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

        //Comment out most of the time just uncomment when you want to seed new data in the database that you added to InitDB()
        // InitDB().Wait();
        // InitDB2().Wait();
        // InitDB3().Wait();
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

    // [Authorize(Roles = Roles.Beheerder)]
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

    private async Task InitDB()
    {
        // Todo clear DB of all data 


        // Create roles
        if (!_roleManager.RoleExistsAsync(Roles.Beheerder).Result)
        {
            var role = new IdentityRole();
            role.Name = Roles.Beheerder;
            _roleManager.CreateAsync(role).Wait();
        }

        if (!_roleManager.RoleExistsAsync(Roles.Ervaringsdeskundige).Result)
        {
            var role = new IdentityRole();
            role.Name = Roles.Ervaringsdeskundige;
            _roleManager.CreateAsync(role).Wait();
        }

        if (!_roleManager.RoleExistsAsync(Roles.Bedrijf).Result)
        {
            var role = new IdentityRole();
            role.Name = Roles.Bedrijf;
            _roleManager.CreateAsync(role).Wait();
        }

        foreach (var e in setDataErvaringsdeskundiges())
        {
            Console.WriteLine("Start Deskundige");
            var userExists = await _userManager.FindByNameAsync(e.Gebruikersnaam);
            if (userExists == null)
            {
                var deskundige = new Ervaringsdeskundige
                {
                    Email = e.Email,
                    Voornaam = e.Voornaam,
                    Achternaam = e.Acternaam,
                    Postcode = e.Postcode,
                    UserName = e.Gebruikersnaam,
                    PhoneNumber = e.Telefoonnummer,
                    AccountType = Roles.Ervaringsdeskundige
                    //Nog meer data over beperkingen en dergelijke, volgt later als dit allemaal werk
                };

                _userManager.CreateAsync(deskundige, e.Wachtwoord).Wait();
                _userManager.AddToRoleAsync(deskundige, Roles.Ervaringsdeskundige).Wait();
                Console.WriteLine("End Deskundige");
            }
        }
    }


    private async Task InitDB2()
    {
        foreach (var b in setDataBeheerder())
        {
            Console.WriteLine(
                $"Start Beheerder\n{b.Voornaam}, {b.Acternaam}, {b.Gebruikersnaam}, {b.Email}, {b.Telefoonnummer}, \n");
            var userExists = await _userManager.FindByNameAsync(b.Gebruikersnaam);
            if (userExists == null)
            {
                var beheerder = new Beheerder
                {
                    Email = b.Email,
                    Voornaam = b.Voornaam,
                    Achternaam = b.Acternaam,
                    UserName = b.Gebruikersnaam,
                    PhoneNumber = b.Telefoonnummer,
                    AccountType = Roles.Beheerder
                };
                Console.WriteLine(
                    $"Beheerder registreer, gewoon\n{b.Voornaam}, {b.Acternaam}, {b.Gebruikersnaam}, {b.Email}, {b.Telefoonnummer}\n{beheerder.Voornaam},{beheerder.Achternaam}, {beheerder.UserName},{beheerder.Email},{beheerder.PhoneNumber},\n\n");

                _userManager.CreateAsync(beheerder, b.Wachtwoord).Wait();
                await _userManager.AddToRoleAsync(beheerder, Roles.Beheerder);
                Console.WriteLine("End Beheerder");
            }
        }
    }

    private async Task InitDB3()
        {
        foreach (var b in setDataBedrijf())
        {
            Console.WriteLine("Start Bedrijf");
            var userExists = await _userManager.FindByNameAsync(b.Gebruikersnaam);
            if (userExists == null)
            {
                var bedrijf = new Bedrijf
                {
                    Email = b.Email,
                    Kvk = b.Kvk,
                    Locatie = b.Locatie,
                    Naam = b.Bedrijfsnaam,
                    Website = b.Website,
                    UserName = b.Gebruikersnaam,
                    PhoneNumber = b.Telefoonnummer,
                    AccountType = Roles.Bedrijf,
                    
                };
                _userManager.CreateAsync(bedrijf, b.Wachtwoord).Wait();
                _userManager.AddToRoleAsync(bedrijf, Roles.Bedrijf).Wait();

                Console.WriteLine("End Bedrijf");
            }
        }
    }

    private List<DTORegistreerBeheerder> setDataBeheerder()
    {
        var rnd = new Random();
        var telefoonnummer = 123456789;
        var ww = "Wachtwoord1!";
        var emailNum = 47;
        return new List<DTORegistreerBeheerder>
        {
            new()
            {
                Voornaam = "Jack",
                Acternaam = "Black",
                Gebruikersnaam = "Fenix",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"beheer{emailNum++}@mail.com"
            },
            new()
            {
                Voornaam = "Ed",
                Acternaam = "Max",
                Gebruikersnaam = "Admin",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"beheer{emailNum++}@mail.com"
            },
            new()
            {
                Voornaam = "Maria",
                Acternaam = "Franz",
                Gebruikersnaam = "Heilung",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"beheer{emailNum++}@mail.com"
            }
        };
    }

    private List<DTORegistreerBedrijf> setDataBedrijf()
    {
        var rnd = new Random();
        var telefoonnummer = 123456789;
        var ww = "Wachtwoord1!";
        var emailNum = 47;
        return new List<DTORegistreerBedrijf>
        {
            new DTORegistreerBedrijf()
            {
                Bedrijfsnaam = "Continental",
                Gebruikersnaam = "JWick",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"bedrijf{emailNum++}@mail.com",
                Kvk = rnd.NextInt64(10000000, 99999999),
                Website = "Continental.com",
                Locatie =
                    $"{rnd.NextInt64(-60, 60)}.{rnd.NextInt64(99999)}, {rnd.NextInt64(-60, 60)}.{rnd.NextInt64(99999)}"
            },
            new DTORegistreerBedrijf()
            {
                Bedrijfsnaam = "Yagami",
                Gebruikersnaam = "Light",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"bedrijf{emailNum++}@mail.com",
                Kvk = rnd.NextInt64(10000000, 99999999),
                Website = "Kira.com",
                Locatie =
                    $"{rnd.NextInt64(-60, 60)}.{rnd.NextInt64(99999)}, {rnd.NextInt64(-60, 60)}.{rnd.NextInt64(99999)}"
            },
            new DTORegistreerBedrijf()
            {
                Bedrijfsnaam = "Halloween Town",
                Gebruikersnaam = "Jack",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"bedrijf{emailNum++}@mail.com",
                Kvk = rnd.NextInt64(10000000, 99999999),
                Website = "the_nightmare_before.com",
                Locatie =
                    $"{rnd.NextInt64(-60, 60)}.{rnd.NextInt64(99999)}, {rnd.NextInt64(-60, 60)}.{rnd.NextInt64(99999)}"
            },
            new DTORegistreerBedrijf()
            {
                Bedrijfsnaam = "Feel Good Inc",
                Gebruikersnaam = "Gorillaz",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"bedrijf{emailNum++}@mail.com",
                Kvk = rnd.NextInt64(10000000, 99999999),
                Website = "Cracker_Island.com",
                Locatie =
                    $"{rnd.NextInt64(-60, 60)}.{rnd.NextInt64(99999)}, {rnd.NextInt64(-60, 60)}.{rnd.NextInt64(99999)}"
            }
        };
    }

    private List<DTORegistreerErvaringsdeskundige> setDataErvaringsdeskundiges()
    {
        var rnd = new Random();
        var telefoonnummer = 123456789;
        var ww = "Wachtwoord1!";
        var emailNum = 47;
        var deskundigenLijst = new List<DTORegistreerErvaringsdeskundige>
        {
            new()
            {
                Voornaam = "Steven",
                Acternaam = "Wilson",
                Gebruikersnaam = "Voyage34",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"mail_ed{emailNum}@mail.com",
                Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
            },
            new()
            {
                Voornaam = "Jack",
                Acternaam = "White",
                Gebruikersnaam = "Raconteurs",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"mail_ed{++emailNum}@mail.com",
                Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
            },
            new()
            {
                Voornaam = "Maynard",
                Acternaam = "Keenan",
                Gebruikersnaam = "A_Perfect_Circle",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"mail_ed{++emailNum}@mail.com",
                Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
            },
            new()
            {
                Voornaam = "John",
                Acternaam = "Entwistle",
                Gebruikersnaam = "ThunderFingers",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"mail_ed{++emailNum}@mail.com",
                Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
            },
            new()
            {
                Voornaam = "Cliff",
                Acternaam = "Burton",
                Gebruikersnaam = "RIP",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"mail_ed{++emailNum}@mail.com",
                Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
            },
            new()
            {
                Voornaam = "Jeff",
                Acternaam = "Wayne",
                Gebruikersnaam = "War_of_the_worlds",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"mail_ed{++emailNum}@mail.com",
                Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
            },
            new()
            {
                Voornaam = "Till",
                Acternaam = "Lindemann",
                Gebruikersnaam = "Rammstein",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"mail_ed{++emailNum}@mail.com",
                Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
            },
            new()
            {
                Voornaam = "Franck",
                Acternaam = "Hueso",
                Gebruikersnaam = "Carpenter Brut",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"mail_ed{++emailNum}@mail.com",
                Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
            }
        };
        return deskundigenLijst;
    }
}