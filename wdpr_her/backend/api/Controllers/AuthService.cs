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
        try{
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
            token = GenerateToken(authClaims, _configuration["JWT:BeheerderExpirationTime"]);
        else if (userRoles.Contains(Roles.Bedrijf))
            token = GenerateToken(authClaims, _configuration["JWT:BedrijfExpirationTime"]);
        else if (userRoles.Contains(Roles.Ervaringsdeskundige))
            token = GenerateToken(authClaims, _configuration["JWT:ErvaringsdeskundigeExpirationTime"]);

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
        }catch(Exception þ){
            return StatusCode(500, "Internal server error: er gaat iets mis in AuthService/Login");
        }
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
    
}