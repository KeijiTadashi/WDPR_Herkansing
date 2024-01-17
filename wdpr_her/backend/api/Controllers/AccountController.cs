using System.Security.Claims;
using api.DataTemplate;
using api.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class AccountController : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<Gebruiker> _userManager;

    public AccountController(UserManager<Gebruiker> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    #region Registreer
    [HttpPost("RegistreerErvaringsdeskundige")]
    public async Task<ActionResult> RegistreerErvaringsdeskundige([FromBody] DTORegistreerErvaringsdeskundige dto)
    {
        try
        {
            var userExists = await _userManager.FindByNameAsync(dto.Gebruikersnaam);
            if (userExists != null)
                return Conflict("De gebruikersnaam bestaat al.");

            var deskundige = new Ervaringsdeskundige
            {
                Email = dto.Email,
                Voornaam = dto.Voornaam,
                Achternaam = dto.Achternaam,
                Postcode = dto.Postcode,
                UserName = dto.Gebruikersnaam,
                PhoneNumber = dto.Telefoonnummer,
                AccountType = Roles.Ervaringsdeskundige
                //Nog meer data over beperkingen en dergelijke, volgt later als dit allemaal werk TODO
            };

            var result = await _userManager.CreateAsync(deskundige, dto.Wachtwoord);
            if (!result.Succeeded)
                return BadRequest("Het aanmaken van een gebruiker is mislukt.");

            // Todo verander rol naar account die wacht op goedkeuring van beheerder met andere bevoegdheden, of misschien nog geen rol en update de rol in de gebruikergoedkuren functie (die nog gemaakt moet worden
            result = await _userManager.AddToRoleAsync(deskundige, Roles.Ervaringsdeskundige);
            if (result.Succeeded)
                return Created("", "Registratie gelukt.");

            return BadRequest(result.Errors);
        }
        catch
        {
            return StatusCode(500, "Internal server error: er gaat iets mis in AccountController/Registreer");
        }
    }

    [Authorize(Roles = Roles.Beheerder)]
    [HttpPost("RegistreerBeheerder")]
    public async Task<ActionResult> RegistreerBeheerder([FromBody] DTORegistreerBeheerder dto)
    {
        try
        {
            var userExists = await _userManager.FindByNameAsync(dto.Gebruikersnaam);
            if (userExists != null)
                return Conflict("De gebruikersnaam bestaat al.");
            var beheerder = new Beheerder
            {
                Email = dto.Email,
                Voornaam = dto.Voornaam,
                Achternaam = dto.Achternaam,
                UserName = dto.Gebruikersnaam,
                PhoneNumber = dto.Telefoonnummer,
                AccountType = Roles.Beheerder
            };

            var result = await _userManager.CreateAsync(beheerder, dto.Wachtwoord);
            if (!result.Succeeded)
                return BadRequest("Het aanmaken van een gebruiker is mislukt.");

            result = await _userManager.AddToRoleAsync(beheerder, Roles.Beheerder);
            if (result.Succeeded)
                return Created("", "Registratie gelukt.");

            return BadRequest(result.Errors);
        }
        catch
        {
            return StatusCode(500, "Internal server error: er gaat iets mis in AccountController/RegistreerBeheerder");
        }
    }

    [HttpPost("RegistreerBedrijf")]
    public async Task<ActionResult> RegistreerBeheerder([FromBody] DTORegistreerBedrijf dto)
    {
        try
        {
            var userExists = await _userManager.FindByNameAsync(dto.Gebruikersnaam);
            if (userExists != null)
                return Conflict("De gebruikersnaam bestaat al.");
            var bedrijf = new Bedrijf
            {
                Email = dto.Email,
                Kvk = dto.Kvk,
                Locatie = dto.Locatie,
                Naam = dto.Bedrijfsnaam,
                Website = dto.Website,
                UserName = dto.Gebruikersnaam,
                PhoneNumber = dto.Telefoonnummer,
                AccountType = Roles.Bedrijf
            };

            var result = await _userManager.CreateAsync(bedrijf, dto.Wachtwoord);
            if (!result.Succeeded)
                return BadRequest("Het aanmaken van een gebruiker is mislukt.");

            // Todo Don't give them a role, only after een beheerder het bedrijf heeft goedgekeured
            result = await _userManager.AddToRoleAsync(bedrijf, Roles.Bedrijf);
            if (result.Succeeded)
                return Created("", "Registratie gelukt.");

            return BadRequest(result.Errors);
        }
        catch
        {
            return StatusCode(500, "Internal server error: er gaat iets mis in AccountController/RegistreerBedrijf");
        }
    }
    #endregion

    #region Update
    [Authorize(Roles = $"{Roles.Ervaringsdeskundige}, {Roles.Beheerder}")]
    [HttpPut("UpdateErvaringsdeskundige")]
    public async Task<ActionResult> UpdateErvaringsdeskundige([FromBody] DTOUpdateErvaringsdeskundige dtoUpdate)
    {
        try
        {
            string userName;
            if (User.FindFirstValue(ClaimTypes.Role) == Roles.Beheerder)
            {
                if (dtoUpdate.UserName == null)
                    return BadRequest("Er is niet bekend welke ervaringsdeskundige moet worden aamgepast.");
                userName = dtoUpdate.UserName;
            }
            else
            {
                userName = User.FindFirstValue(ClaimTypes.Name);
            }
            var deskundige = (Ervaringsdeskundige)await _userManager.FindByNameAsync(userName);
            if (deskundige == null)
                return BadRequest("Ervaringsdeskundige is niet gevonden");

            deskundige.Voornaam = dtoUpdate.Voornaam ?? deskundige.Voornaam;
            deskundige.Achternaam = dtoUpdate.Achternaam ?? deskundige.Achternaam;
            deskundige.PhoneNumber = dtoUpdate.TelefoonNummer ?? deskundige.PhoneNumber;
            deskundige.Postcode = dtoUpdate.Postcode ?? deskundige.Postcode;
            deskundige.Email = dtoUpdate.Email ?? deskundige.Email;


            // Change password
            if (dtoUpdate.NieuwWachtwoord != null)
            {
                var result = await _userManager.ChangePasswordAsync(deskundige, dtoUpdate.HuidigWachtwoord, dtoUpdate.NieuwWachtwoord);
                if (!result.Succeeded)
                    return BadRequest(result.Errors);
            }

            // Change username
            if (dtoUpdate.NewUserName != null)
            {
                var userExists = await _userManager.FindByNameAsync(dtoUpdate.NewUserName);
                if (userExists != null)
                    return Conflict("De gebruikersnaam bestaat al.");
                deskundige.UserName = dtoUpdate.NewUserName;
                await _userManager.UpdateNormalizedUserNameAsync(deskundige);
            }

            await _userManager.UpdateAsync(deskundige);
            return Ok("De gebuikers informatie is geupdate.");
        }
        catch
        {
            return StatusCode(500, "Internal server error: er gaat iets mis in AccountController/UpdateErvaringsDeskundige");
        }
    }


    [Authorize(Roles = $"{Roles.Bedrijf}, {Roles.Beheerder}")]
    [HttpPut("UpdateBedrijf")]
    public async Task<ActionResult> UpdateBedrijf([FromBody] DTOUpdateBedrijf dtoUpdate)
    {
        try{
            string userName;
            if (User.FindFirstValue(ClaimTypes.Role) == Roles.Beheerder)
            {
                if (dtoUpdate.UserName == null)
                    return BadRequest("Er is niet bekend welk bedrijf moet worden aamgepast.");
                userName = dtoUpdate.UserName;
            }
            else
            {
                userName = User.FindFirstValue(ClaimTypes.Name);
            }
            var bedrijf = (Bedrijf)await _userManager.FindByNameAsync(userName);
            if (bedrijf == null)
                return BadRequest("Ervaringsdeskundige is niet gevonden");

            bedrijf.Kvk = dtoUpdate.Kvk ?? bedrijf.Kvk;
            bedrijf.Email = dtoUpdate.Email ?? bedrijf.Email;
            bedrijf.Naam = dtoUpdate.Naam ?? bedrijf.Naam;
            bedrijf.Locatie = dtoUpdate.Locatie ?? bedrijf.Locatie;
            bedrijf.PhoneNumber = dtoUpdate.TelefoonNummer ?? bedrijf.PhoneNumber;
            bedrijf.Website = dtoUpdate.Website ?? bedrijf.Website;

            // Change password
            if (dtoUpdate.NieuwWachtwoord != null)
            {
                var result = await _userManager.ChangePasswordAsync(bedrijf, dtoUpdate.HuidigWachtwoord, dtoUpdate.NieuwWachtwoord);
                if (!result.Succeeded)
                    return BadRequest(result.Errors);
            }

            // Change username
            if (dtoUpdate.NewUserName != null)
            {
                var userExists = await _userManager.FindByNameAsync(dtoUpdate.NewUserName);
                if (userExists != null)
                    return Conflict("De gebruikersnaam bestaat al.");
                bedrijf.UserName = dtoUpdate.NewUserName;
                await _userManager.UpdateNormalizedUserNameAsync(bedrijf);
            }

            await _userManager.UpdateAsync(bedrijf);
            return Ok("De gebuikers informatie is geupdate.");
        }
        catch
        {
            return StatusCode(500, "Internal server error: er gaat iets mis in AccountController/UpdateBedrijf");
        }
    }

    [Authorize(Roles = Roles.Beheerder)]
    [HttpPut("UpdateBeheerder")]
    public async Task<ActionResult> UpdateBeheerder([FromBody] DTOUpdateBeheerder dtoUpdate)
    {
        try{
                
            string userName;
            userName = User.FindFirstValue(ClaimTypes.Name);

            var beheerder = (Beheerder)await _userManager.FindByNameAsync(userName);
            if (beheerder == null)
                return BadRequest("Beheerder is niet gevonden");

            beheerder.Achternaam = dtoUpdate.Achternaam ?? beheerder.Achternaam;
            beheerder.Voornaam = dtoUpdate.Voornaam ?? beheerder.Voornaam;
            beheerder.Email = dtoUpdate.Email ?? beheerder.Email;
            beheerder.PhoneNumber = dtoUpdate.TelefoonNummer ?? beheerder.PhoneNumber;

            // Change password
            if (dtoUpdate.NieuwWachtwoord != null)
            {
                var result = await _userManager.ChangePasswordAsync(beheerder, dtoUpdate.HuidigWachtwoord, dtoUpdate.NieuwWachtwoord);
                if (!result.Succeeded)
                    return BadRequest(result.Errors);
            }

            // Change username
            if (dtoUpdate.NewUserName != null)
            {
                var userExists = await _userManager.FindByNameAsync(dtoUpdate.NewUserName);
                if (userExists != null)
                    return Conflict("De gebruikersnaam bestaat al.");
                beheerder.UserName = dtoUpdate.NewUserName;
                await _userManager.UpdateNormalizedUserNameAsync(beheerder);
            }

            await _userManager.UpdateAsync(beheerder);
            return Ok("De gebuikers informatie is geupdate.");
        }
        catch
        {
            return StatusCode(500, "Internal server error: er gaat iets mis in AccountController/UpdateBeheerder");
        }
    }


    #endregion
}