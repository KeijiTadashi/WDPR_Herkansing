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
        string em = "";
        try
        {
            em = "Locatie 1: await _userManager geeft een error";
            var userExists = await _userManager.FindByNameAsync(dto.Gebruikersnaam);
            if (userExists != null)
                return Conflict("De gebruikersnaam bestaat al.");

            em = "Locatie 2: Error in het aanmaken van een nieuwe Ervaringsdeskundige";
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

            em = "Locatie 3: await van create userManager geeft error";
            var result = await _userManager.CreateAsync(deskundige, dto.Wachtwoord);
            if (!result.Succeeded)
                return BadRequest("Het aanmaken van een gebruiker is mislukt.");

            // Todo verander rol naar account die wacht op goedkeuring van beheerder met andere bevoegdheden, of misschien nog geen rol en update de rol in de gebruikergoedkuren functie (die nog gemaakt moet worden
            em = "Locatie 4: await van  userManager geeft error";
            result = await _userManager.AddToRoleAsync(deskundige, Roles.Ervaringsdeskundige);
            if (result.Succeeded)
            {
                return Created("", "Registratie gelukt.");
            }
            return BadRequest(result.Errors);
        }
        catch (Exception å)
        {
            print(å);
            print(em);
            return StatusCode(500, "Internal server error: er gaat iets mis in AccountController/Registreer. Error Message:" + em);
        }
    }

    [Authorize(Roles = Roles.Beheerder)]
    [HttpPost("RegistreerBeheerder")]
    public async Task<ActionResult> RegistreerBeheerder([FromBody] DTORegistreerBeheerder dto)
    {
        string em = "";
        try
        {

            em = "Fout bij await _userManager FindByNameAsync";
            var userExists = await _userManager.FindByNameAsync(dto.Gebruikersnaam);
            if (userExists != null)
            {
                return Conflict("De gebruikersnaam bestaat al.");
            }

            em = "Fout bij aanmaken nieuw Beheerder Object";
            var beheerder = new Beheerder
            {
                Email = dto.Email,
                Voornaam = dto.Voornaam,
                Achternaam = dto.Achternaam,
                UserName = dto.Gebruikersnaam,
                PhoneNumber = dto.Telefoonnummer,
                AccountType = Roles.Beheerder
            };

            em = "Fout bij await _userManager CreateAsync";
            var result = await _userManager.CreateAsync(beheerder, dto.Wachtwoord);
            if (!result.Succeeded)
            {
                return BadRequest("Het aanmaken van een gebruiker is mislukt.");
            }

            em = "Fout bij _userManager AddToRoleAsync";
            result = await _userManager.AddToRoleAsync(beheerder, Roles.Beheerder);
            if (result.Succeeded)
            {
                return Created("", "Registratie gelukt.");
            }
            return BadRequest(result.Errors);
        }
        catch (Exception æ)
        {
            print(æ);
            print(em);

            return StatusCode(500, "Internal server error: er gaat iets mis in AccountController/RegistreerBeheerder. Error:"+em);
        }
    }

    [HttpPost("RegistreerBedrijf")]
    public async Task<ActionResult> RegistreerBeheerder([FromBody] DTORegistreerBedrijf dto)
    {
        string em = "";
        try
        {
            em = "Fout bij await _userManager FindByNameAsync";
            var userExists = await _userManager.FindByNameAsync(dto.Gebruikersnaam);
            if (userExists != null)
            {
                return Conflict("De gebruikersnaam bestaat al.");
            }

            em = "Error bij aanmaken Bedrijf object";
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

            em = "Error bij await _userManager CreateAsync";
            var result = await _userManager.CreateAsync(bedrijf, dto.Wachtwoord);
            if (!result.Succeeded)
            {
                return BadRequest($"Het aanmaken van een gebruiker is mislukt.\n{result}");
            }

            // Todo Don't give them a role, only after een beheerder het bedrijf heeft goedgekeured
            em = "Error bij await _userManager AddToRoleAsync";
            result = await _userManager.AddToRoleAsync(bedrijf, Roles.Bedrijf);
            if (result.Succeeded)
            {
                return Created("", "Registratie gelukt.");
            }

            return BadRequest(result.Errors);
        }
        catch (Exception þ)
        {
            print(þ);
            print(em);
            return StatusCode(500, "Internal server error: er gaat iets mis in AccountController/RegistreerBedrijf. Error:"+em);
        }
    }

    #endregion

    #region Update

    [Authorize(Roles = $"{Roles.Ervaringsdeskundige}, {Roles.Beheerder}")]
    [HttpPut("UpdateErvaringsdeskundige")]
    public async Task<ActionResult> UpdateErvaringsdeskundige([FromBody] DTOUpdateErvaringsdeskundige dtoUpdate)
    {
        string em = "";
        try
        {
            string userName;

            em = "Fout bij User.FindFirstValue";
            if (User.FindFirstValue(ClaimTypes.Role) == Roles.Beheerder)
            {
                if (dtoUpdate.UserName == null)
                {
                    return BadRequest("Er is niet bekend welke ervaringsdeskundige moet worden aamgepast.");
                }
                userName = dtoUpdate.UserName;
            }
            else
            {
                userName = User.FindFirstValue(ClaimTypes.Name);
            }


            em = "Fout bij await _userManager FindByNameAsync";

            var deskundige = (Ervaringsdeskundige)await _userManager.FindByNameAsync(userName);
            if (deskundige == null)
            {
                return BadRequest("Ervaringsdeskundige is niet gevonden");
            }
            deskundige.Voornaam = dtoUpdate.Voornaam ?? deskundige.Voornaam;
            deskundige.Achternaam = dtoUpdate.Achternaam ?? deskundige.Achternaam;
            deskundige.PhoneNumber = dtoUpdate.TelefoonNummer ?? deskundige.PhoneNumber;
            deskundige.Postcode = dtoUpdate.Postcode ?? deskundige.Postcode;
            deskundige.Email = dtoUpdate.Email ?? deskundige.Email;


            // Change password
            em = "Fout bij change password";
            if (dtoUpdate.NieuwWachtwoord != null)
            {
                var result = await _userManager.ChangePasswordAsync(deskundige, dtoUpdate.HuidigWachtwoord,
                    dtoUpdate.NieuwWachtwoord);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
            }

            // Change username
            em = "Fout bij ChangeUsername";
            if (dtoUpdate.NewUserName != null)
            {
                var userExists = await _userManager.FindByNameAsync(dtoUpdate.NewUserName);
                if (userExists != null)
                    return Conflict("De gebruikersnaam bestaat al.");
                deskundige.UserName = dtoUpdate.NewUserName;
                await _userManager.UpdateNormalizedUserNameAsync(deskundige);
            }

            em = "Fout bij _userManager UpdateAsync";
            await _userManager.UpdateAsync(deskundige);
            return Ok("De gebuikers informatie is geupdate.");
        }
        catch (Exception βρεκεκεκέξ)
        {
            print(βρεκεκεκέξ);
            print(em);
            return StatusCode(500, "Internal server error: er gaat iets mis in AccountController/UpdateErvaringsDeskundige. Error:"+em);

        }
    }


    [Authorize(Roles = $"{Roles.Bedrijf}, {Roles.Beheerder}")]
    [HttpPut("UpdateBedrijf")]
    public async Task<ActionResult> UpdateBedrijf([FromBody] DTOUpdateBedrijf dtoUpdate)
    {
        string em = "";
        try
        {
            em = "Error bij het vinden van het bedrijf";

            string userName;
            if (User.FindFirstValue(ClaimTypes.Role) == Roles.Beheerder)
            {
                if (dtoUpdate.UserName == null)
                {
                    return BadRequest("Er is niet bekend welk bedrijf moet worden aamgepast.");
                }
                userName = dtoUpdate.UserName;
            }
            else
            {
                userName = User.FindFirstValue(ClaimTypes.Name);
            }



            em="Error bij het casten naar bedrijf";
            var bedrijf = (Bedrijf)await _userManager.FindByNameAsync(userName);
            if (bedrijf == null)
            {
                return BadRequest("Ervaringsdeskundige is niet gevonden");
            }


            em="Error bij updaten attributen van bedrijf";

            bedrijf.Kvk = dtoUpdate.Kvk ?? bedrijf.Kvk;
            bedrijf.Email = dtoUpdate.Email ?? bedrijf.Email;
            bedrijf.Naam = dtoUpdate.Naam ?? bedrijf.Naam;
            bedrijf.Locatie = dtoUpdate.Locatie ?? bedrijf.Locatie;
            bedrijf.PhoneNumber = dtoUpdate.TelefoonNummer ?? bedrijf.PhoneNumber;
            bedrijf.Website = dtoUpdate.Website ?? bedrijf.Website;


            em="Error bij aanpassen wachtwoord";

            // Change password
            if (dtoUpdate.NieuwWachtwoord != null)
            {
                var result = await _userManager.ChangePasswordAsync(bedrijf, dtoUpdate.HuidigWachtwoord,
                    dtoUpdate.NieuwWachtwoord);
                if (!result.Succeeded)
                    return BadRequest(result.Errors);
            }

            em="Error bij aanpassen username";

            // Change username
            if (dtoUpdate.NewUserName != null)
            {
                var userExists = await _userManager.FindByNameAsync(dtoUpdate.NewUserName);
                if (userExists != null)
                    return Conflict("De gebruikersnaam bestaat al.");
                bedrijf.UserName = dtoUpdate.NewUserName;
                await _userManager.UpdateNormalizedUserNameAsync(bedrijf);
            }


            em="Error bij updaten bedrijf";

            await _userManager.UpdateAsync(bedrijf);
            return Ok("De gebuikers informatie is geupdate.");
        }
        catch (Exception κοάξ)
        {
            print(κοάξ);
            print(em);
            return StatusCode(500, "Internal server error: er gaat iets mis in AccountController/UpdateBedrijf. Error:"+em);
        }
    }

    [Authorize(Roles = Roles.Beheerder)]
    [HttpPut("UpdateBeheerder")]
    public async Task<ActionResult> UpdateBeheerder([FromBody] DTOUpdateBeheerder dtoUpdate)
    {
        string em="";
        try
        {

            em="Fout bij ophalen beheerder";

            string userName;
            userName = User.FindFirstValue(ClaimTypes.Name);


            em="Fout bij await _userManager.FindByNameAsync";

            var beheerder = (Beheerder)await _userManager.FindByNameAsync(userName);
            if (beheerder == null)
                return BadRequest("Beheerder is niet gevonden");


            em="Fout bij updaten attribuyten van beheerder";

            beheerder.Achternaam = dtoUpdate.Achternaam ?? beheerder.Achternaam;
            beheerder.Voornaam = dtoUpdate.Voornaam ?? beheerder.Voornaam;
            beheerder.Email = dtoUpdate.Email ?? beheerder.Email;
            beheerder.PhoneNumber = dtoUpdate.TelefoonNummer ?? beheerder.PhoneNumber;

           
             // Change password
            em="Fout bij updaten wachtwoord";

            if (dtoUpdate.NieuwWachtwoord != null)
            {
                var result = await _userManager.ChangePasswordAsync(beheerder, dtoUpdate.HuidigWachtwoord,
                    dtoUpdate.NieuwWachtwoord);
                if (!result.Succeeded)
                    return BadRequest(result.Errors);
            }


            // Change username
            em="Fout bij aanpassen username";
            
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
        catch(Exception µ)
        {
            print(µ);
            print(em);
            return StatusCode(500, "Internal server error: er gaat iets mis in AccountController/UpdateBeheerder. Error:"+em);
        }
    }

    #endregion

    private void print<T>(T t)
    {
        Console.WriteLine(t);
    }
}