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
    
    [HttpPost("RegistreerErvaringsdeskundige")]
    public async Task<ActionResult> RegistreerErvaringsdeskundige([FromBody] DTORegistreerErvaringsdeskundige dto)
    {
        var userExists = await _userManager.FindByNameAsync(dto.Gebruikersnaam);
        if (userExists != null)
            return Conflict("De gebruikersnaam bestaat al.");
        
        var deskundige = new Ervaringsdeskundige
        {
            Email = dto.Email,
            Voornaam = dto.Voornaam,
            Achternaam = dto.Acternaam,
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

    [Authorize(Roles = Roles.Beheerder)]
    [HttpPost("RegistreerBeheerder")]
    public async Task<ActionResult> RegistreerBeheerder([FromBody] DTORegistreerBeheerder dto)
    {
        var userExists = await _userManager.FindByNameAsync(dto.Gebruikersnaam);
        if (userExists != null)
            return Conflict("De gebruikersnaam bestaat al.");
        var beheerder = new Beheerder
        {
            Email = dto.Email,
            Voornaam = dto.Voornaam,
            Achternaam = dto.Acternaam,
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

    [HttpPost("RegistreerBedrijf")]
    public async Task<ActionResult> RegistreerBeheerder([FromBody] DTORegistreerBedrijf dto)
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

        // Todo For now don't give them a role, only after een beheerder het bedrijf heeft goedgekeured
        // result = await _userManager.AddToRoleAsync(bedrijf, Roles.Bedrijf);
        if (result.Succeeded)
            return Created("", "Registratie gelukt.");

        return BadRequest(result.Errors);
    }
}