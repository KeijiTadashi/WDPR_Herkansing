using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.DataTemplate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace api;

/*
 * Test ervaringsdeskundige: Gebruikersnaam = "TestDeskundige"; Wachtwoord = "MijnWachtwoord1!"
 */
public class AuthService : ControllerBase
{
    private readonly UserManager<Gebruiker> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<Gebruiker> userManager, RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        //Create roles if they don't exist, can be removed after running once and maybe put into some kind of start the service or just remove
        if (!_roleManager.RoleExistsAsync(Roles.Beheerder).Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = Roles.Beheerder;
            roleManager.CreateAsync(role).Wait();
        }
        if (!roleManager.RoleExistsAsync(Roles.Ervaringsdeskundige).Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = Roles.Ervaringsdeskundige;
            roleManager.CreateAsync(role).Wait();
        }
        if (!roleManager.RoleExistsAsync(Roles.Bedrijf).Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = Roles.Bedrijf;
            roleManager.CreateAsync(role).Wait();
        }
    }

    [HttpPost("RegistreerErvaringsdeskundige")]
    public async Task<ActionResult> RegistreerErvaringsdeskundige([FromBody] DTORegistreerErvaringsdeskundige dto)
    {
        var userExists = await _userManager.FindByNameAsync(dto.Gebruikersnaam);
        if (userExists != null)
            return Conflict("De gebruikersnaam bestaat al.");
        // return (new ConflictObjectResult("Gebruiksnaam bestaat al."));
        Ervaringsdeskundige deskundige = new Ervaringsdeskundige()
        {
            Email = dto.Email,
            Voornaam = dto.Voornaam,
            Achternaam = dto.Acternaam,
            Postcode = dto.Postcode,
            UserName = dto.Gebruikersnaam,
            PhoneNumber = dto.Telefoonnummer,
            AccountType = Roles.Ervaringsdeskundige
            //Nog meer data over beperkingen en dergelijke, volgt later als dit allemaal werk
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

    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] DTOLogin dto)
    {
        var gebruiker = await _userManager.FindByNameAsync(dto.Gebruikersnaam);

        if (gebruiker == null)
            return NotFound("De gebruikersnaam bestaat niet.");

        if (!await _userManager.CheckPasswordAsync(gebruiker, dto.Wachtwoord))
            return BadRequest("Het wachtwoord is incorrect.");
        
        var userRoles = await _userManager.GetRolesAsync(gebruiker);
        var authClaims = new List<Claim> {
            new Claim(ClaimTypes.Name, gebruiker.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        (string token, DateTime validTo) token = GenerateToken(authClaims, _configuration["Jwt:ErvaringsdeskundigeExpirationInHours"]);
        
        return Ok(new
        {
            api_key = token.token,
            expiration = token.validTo,
            user = gebruiker.UserName,
            Role = userRoles,
            status = "Gebruiker login successful"
        });
    }

    private (string, DateTime) GenerateToken(IEnumerable<Claim> claims, string expirationInHours)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddHours(Convert.ToInt64(expirationInHours)), 
            claims: claims, 
            signingCredentials: new SigningCredentials(
                authSigningKey,
                SecurityAlgorithms.HmacSha256
                )
            );
        return (new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);


    }

    [Authorize(Roles = $"{Roles.Ervaringsdeskundige}, {Roles.Beheerder}")]
    [HttpPut("UpdateErvaringsdeskundige")]
    public async Task<ActionResult> UpdateErvaringsdeskundife([FromBody] DTOErvaringsdeskundige dto)
    {
        //Add some logic if the user has role beheerder, because they can edit someone else

        var userId = User.FindFirstValue(ClaimTypes.Name);
        Console.WriteLine($"UserId: {userId}");
        Ervaringsdeskundige deskundige = (Ervaringsdeskundige) await _userManager.FindByNameAsync(userId);
        if (deskundige == null)
            return BadRequest("Ervaringsdeskundige is niet gevonden");

        deskundige.Voornaam = dto.Voornaam != null ? deskundige.Voornaam = dto.Voornaam : deskundige.Voornaam;
        deskundige.Achternaam = dto.Achternaam != null ? deskundige.Achternaam = dto.Achternaam : deskundige.Achternaam;
        deskundige.PhoneNumber = dto.TelefoonNummer != null ? deskundige.PhoneNumber = dto.TelefoonNummer : deskundige.PhoneNumber;
        deskundige.Postcode = dto.Postcode != null ? deskundige.Postcode = dto.Postcode : deskundige.Postcode;
        deskundige.Email = dto.Email != null ? deskundige.Email = dto.Email : deskundige.Email;
        if (dto.NieuwWachtwoord != null)
        {
            var result = await _userManager.ChangePasswordAsync(deskundige, dto.HuidigWachtwoord, dto.NieuwWachtwoord);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
        }

        await _userManager.UpdateAsync(deskundige);
        return Ok("De gebuikers informatie is geupdate.");

    }

}