using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.DataTemplate;
using api.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace api.Controllers;


public class AuthService : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<Gebruiker> _userManager;

    public AuthService(UserManager<Gebruiker> userManager, RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    

    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] DTOLogin dto)
    {
        var gebruiker = await _userManager.FindByNameAsync(dto.Gebruikersnaam);
        Console.WriteLine($"Gebruiker {gebruiker}");

        if (gebruiker == null)
            return NotFound("De gebruikersnaam bestaat niet.");

        if (!await _userManager.CheckPasswordAsync(gebruiker, dto.Wachtwoord))
            return BadRequest("Het wachtwoord is incorrect.");

        var userRoles = await _userManager.GetRolesAsync(gebruiker);
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, gebruiker.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        foreach (var userRole in userRoles) authClaims.Add(new Claim(ClaimTypes.Role, userRole));

        (string token, DateTime validTo) token = ("", DateTime.Now);
        if (userRoles.Contains(Roles.Beheerder))
            token = GenerateToken(authClaims, _configuration["Jwt:BeheerderExpirationInHours"]);
        else if (userRoles.Contains(Roles.Bedrijf))
            token = GenerateToken(authClaims, _configuration["Jwt:BedrijfExpirationInHours"]);
        else if (userRoles.Contains(Roles.Ervaringsdeskundige))
            token = GenerateToken(authClaims, _configuration["Jwt:ErvaringsdeskundigeExpirationInHours"]);

        if (token.token != "")
            return Ok(new
            {
                api_key = token.token,
                expiration = token.validTo,
                user = gebruiker.UserName,
                Role = userRoles,
                status = "Gebruiker login successful"
            });

        return BadRequest("Couldn't generate jwt token.");
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
        var deskundige = (Ervaringsdeskundige)await _userManager.FindByNameAsync(userId);
        if (deskundige == null)
            return BadRequest("Ervaringsdeskundige is niet gevonden");

        deskundige.Voornaam = dto.Voornaam != null ? deskundige.Voornaam = dto.Voornaam : deskundige.Voornaam;
        deskundige.Achternaam = dto.Achternaam != null ? deskundige.Achternaam = dto.Achternaam : deskundige.Achternaam;
        deskundige.PhoneNumber = dto.TelefoonNummer != null
            ? deskundige.PhoneNumber = dto.TelefoonNummer
            : deskundige.PhoneNumber;
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