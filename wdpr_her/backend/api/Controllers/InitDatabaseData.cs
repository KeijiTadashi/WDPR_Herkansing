using api.DataTemplate;
using api.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

// TODO, doesn't work correctly yet
public class InitDatabaseData : ControllerBase
{
    private readonly StichtingContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<Gebruiker> _userManager;

    public InitDatabaseData(StichtingContext context, UserManager<Gebruiker> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpPost("InitData")]
    public async Task<ActionResult> InitData()
    {
        String em = "";
        try
        {
            #region Roles


            if (!_roleManager.RoleExistsAsync(Roles.Beheerder).Result)
            {
                em="Er gaat wat mis in de check of rol Beheerder bestaat";
                var role = new IdentityRole();
                role.Name = Roles.Beheerder;
                _roleManager.CreateAsync(role).Wait();
            }

            if (!_roleManager.RoleExistsAsync(Roles.Ervaringsdeskundige).Result)
            {
                em="er gaat wat mis in de check of de rol Ervaringsdeskundige bestaat";
                var role = new IdentityRole();
                role.Name = Roles.Ervaringsdeskundige;
                _roleManager.CreateAsync(role).Wait();
            }

            if (!_roleManager.RoleExistsAsync(Roles.Bedrijf).Result)
            {
                em="er gaat wat mis in de check of de rol Bedrijf bestaat";
                var role = new IdentityRole();
                role.Name = Roles.Bedrijf;
                _roleManager.CreateAsync(role).Wait();
            }

            #endregion

            #region Enums

            HelperController helperController = new HelperController(_context);

            foreach (var v in setAandoeningen())
            {
                await helperController.AddAandoening(v);
                await helperController.AddBeperking(v); // Want ik (Brian) weet niet wat het verschil eigenlijk is of dat het dubbel is
            }
            foreach (var v in setBenadering())
            {
                await helperController.AddBenadering(v);
            }
            foreach (var v in setOnderzoeksTypes())
            {
                await helperController.AddOnderzoeksType(v);
            }

            // TODO Add Hulpmiddelen

            #endregion

            AccountController accountController = new AccountController(_userManager, _roleManager);
            #region Ervaringsdeskundigen

            foreach (var e in setDataErvaringsdeskundiges())
            {
                await accountController.RegistreerErvaringsdeskundige(e);
            }
            #endregion

            #region Beheerders

            foreach (var b in setDataBeheerder())
            {
                await accountController.RegistreerBeheerder(b);
                // print(
                //     $"Start Beheerder\n{b.Voornaam}, {b.Achternaam}, {b.Gebruikersnaam}, {b.Email}, {b.Telefoonnummer}, \n");
                // var userExists = await _userManager.FindByNameAsync(b.Gebruikersnaam);
                // if (userExists == null)
                // {
                //     var beheerder = new Beheerder
                //     {
                //         Email = b.Email,
                //         Voornaam = b.Voornaam,
                //         Achternaam = b.Achternaam,
                //         UserName = b.Gebruikersnaam,
                //         PhoneNumber = b.Telefoonnummer,
                //         AccountType = Roles.Beheerder
                //     };
                //     print(
                //         $"Beheerder registreer, gewoon\n{b.Voornaam}, {b.Achternaam}, {b.Gebruikersnaam}, {b.Email}, {b.Telefoonnummer}\n{beheerder.Voornaam},{beheerder.Achternaam}, {beheerder.UserName},{beheerder.Email},{beheerder.PhoneNumber},\n\n");
                //
                //     _userManager.CreateAsync(beheerder, b.Wachtwoord).Wait();
                //     _userManager.AddToRoleAsync(beheerder, Roles.Beheerder).Wait();
                //     print("End Beheerder");
                // }
            }

            #endregion

            #region Bedrijven

            foreach (var b in setDataBedrijf())
            {
                print("Start Bedrijf");
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
                        AccountType = Roles.Bedrijf
                    };
                    _userManager.CreateAsync(bedrijf, b.Wachtwoord).Wait();
                    _userManager.AddToRoleAsync(bedrijf, Roles.Bedrijf).Wait();

                    print("End Bedrijf");
                }
            }

            #endregion

            return Ok();
        }
        catch (Exception अ)
        {
            //Kanthya
            print(अ);
            return StatusCode(500, "Internal server error: er gaat iets mis in InitDatabaseData/InitData");
        }
    }

    #region Data

    private List<DTORegistreerBeheerder> setDataBeheerder()
    {
        var rnd = new Random();
        var telefoonnummer = 123456789;
        var ww = "Wachtwoord1!";
        var emailNum = 47 + 100;
        return new List<DTORegistreerBeheerder>
        {
            new()
            {
                Voornaam = "Jack",
                Achternaam = "Black",
                Gebruikersnaam = "Fenix",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"beheer{emailNum++}@mail.com"
            },
            new()
            {
                Voornaam = "Ed",
                Achternaam = "Max",
                Gebruikersnaam = "Admin",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"beheer{emailNum++}@mail.com"
            },
            new()
            {
                Voornaam = "Maria",
                Achternaam = "Franz",
                Gebruikersnaam = "Heilung",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"beheer{emailNum++}@mail.com"
            },
            new()
            {
                Voornaam = "Sherlock",
                Achternaam = "Holmes",
                Gebruikersnaam = "Mycroft",
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
                Achternaam = "Wilson",
                Gebruikersnaam = "Voyage34",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"mail_ed{emailNum}@mail.com",
                Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
            },
            new()
            {
                Voornaam = "Jack",
                Achternaam = "White",
                Gebruikersnaam = "Raconteurs",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"mail_ed{++emailNum}@mail.com",
                Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
            },
            new()
            {
                Voornaam = "Maynard",
                Achternaam = "Keenan",
                Gebruikersnaam = "A_Perfect_Circle",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"mail_ed{++emailNum}@mail.com",
                Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
            },
            new()
            {
                Voornaam = "John",
                Achternaam = "Entwistle",
                Gebruikersnaam = "ThunderFingers",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"mail_ed{++emailNum}@mail.com",
                Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
            },
            new()
            {
                Voornaam = "Cliff",
                Achternaam = "Burton",
                Gebruikersnaam = "RIP",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"mail_ed{++emailNum}@mail.com",
                Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
            },
            new()
            {
                Voornaam = "Jeff",
                Achternaam = "Wayne",
                Gebruikersnaam = "War_of_the_worlds",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"mail_ed{++emailNum}@mail.com",
                Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
            },
            new()
            {
                Voornaam = "Till",
                Achternaam = "Lindemann",
                Gebruikersnaam = "Rammstein",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"mail_ed{++emailNum}@mail.com",
                Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
            },
            new()
            {
                Voornaam = "Franck",
                Achternaam = "Hueso",
                Gebruikersnaam = "Carpenter_Brut",
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"mail_ed{++emailNum}@mail.com",
                Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
            }
        };
        return deskundigenLijst;
    }

    private List<DTOHelper> setAandoeningen()
    {
        return new List<DTOHelper>()
        {
            new DTOHelper() { Naam = "Slechtziend" },
            new DTOHelper() { Naam = "Blind" },
            new DTOHelper() { Naam = "Dyslectisch" },
            new DTOHelper() { Naam = "Slechthorend" },
            new DTOHelper() { Naam = "Doof" }
        };
    }

    private List<DTOHelper> setBenadering()
    {
        return new List<DTOHelper>()
        {
            new DTOHelper() { Naam = "E-mail" },
            new DTOHelper() { Naam = "Telefonisch" },
            new DTOHelper() { Naam = "Chat" }
        };
    }

    private List<DTOHelper> setOnderzoeksTypes()
    {
        return new List<DTOHelper>()
        {
            new DTOHelper() { Naam = "Enquête" },
            new DTOHelper() { Naam = "Interview" },
            new DTOHelper() { Naam = "Op Locatie" },
            new DTOHelper() { Naam = "Engelstalig" },
        };
    }

    #endregion

    private void print<T>(T t){
        Console.WriteLine(t);
    }
}