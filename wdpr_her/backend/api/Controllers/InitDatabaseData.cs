using api.DataTemplate;
using api.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpecFlow.Internal.Json;

namespace api.Controllers;

// TODO, doesn't work correctly yet
public class InitDatabaseData : ControllerBase
{
    private readonly StichtingContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<Gebruiker> _userManager;

    public InitDatabaseData(StichtingContext context, UserManager<Gebruiker> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpPost("InitData")]
    public async Task<ActionResult> InitData()
    {
        try
        {
            #region Roles

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

            #endregion

            #region Enums

            HelperController helperController = new HelperController(_context);

            foreach (var v in setAandoeningen())
            {
                var exists = await _context.Aandoeningen.FirstOrDefaultAsync(a => a.Naam == v.Naam);
                if (exists != null)
                    continue;
                await helperController.AddAandoening(v);
                await helperController
                    .AddBeperking(v); // Want ik (Brian) weet niet wat het verschil eigenlijk is of dat het dubbel is
            }

            foreach (var v in setBenadering())
            {
                var exists = await _context.Benaderingen.FirstOrDefaultAsync(a => a.Soort == v.Naam);
                if (exists != null)
                    continue;
                await helperController.AddBenadering(v);
            }

            foreach (var v in setOnderzoeksTypes())
            {
                var exists = await _context.OnderzoeksTypes.FirstOrDefaultAsync(a => a.Type == v.Naam);
                if (exists != null)
                    continue;
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
            }

            #endregion

            #region Bedrijven

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
                        AccountType = Roles.Bedrijf
                    };
                    _userManager.CreateAsync(bedrijf, b.Wachtwoord).Wait();
                    _userManager.AddToRoleAsync(bedrijf, Roles.Bedrijf).Wait();

                    Console.WriteLine("End Bedrijf");
                }
            }

            #endregion

            #region Onderzoeken

            OnderzoekController onderzoekController = new OnderzoekController(_context, _userManager);
            foreach (var dto in setOnderzoeken())
            {
                var exists = await _context.Onderzoeken.FirstOrDefaultAsync(a => a.Titel == dto.Titel);
                if (exists != null)
                    continue;
                var result = await onderzoekController.CreateOnderzoek(dto);
                Console.WriteLine(result.ToJson());
            }

            #endregion

            return Ok();
        }
        catch (Exception अ)
        {
            //Kanthya
            Console.Write(अ);
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

    // Will probably fail, because user is not set, might work if logged in and then run InitData
    private List<DTOCreateOnderzoek> setOnderzoeken()
    {
        return new List<DTOCreateOnderzoek>
        {
            new DTOCreateOnderzoek
            {
                Locatie = "Moon",
                Titel = "Fly me to the ...",
                OnderzoeksTypes = new List<int> { 2, 3, 4 }, // (1)Enquête, (2)Interview, (3)Op Locatie, (4)Engelstalig
                Beloning = "A stardust diamond",
                Beschrijving = @"Look up here, I'm in Heaven
I've got scars that can't be seen
I've got drama, can't be stolen
Everybody knows me now
Look up here, man, I'm in danger
I've got nothing left to lose
I'm so high, it makes my brain whirl
Dropped my cellphone down below
Ain't that just like me?
By the time I got to New York
I was living like a king
There I used up all my money
I was looking for your ass
This way or no way
You know, I'll be free
Just like that bluebird
Now, ain't that just like me?
Oh, I'll be free
Just like that bluebird
Oh, I'll be free
Ain't that just like me?",
                OnderzoeksData = @"{
Sectie: {
    Vraag:  
}"
            }
        };
    }

    #endregion
}