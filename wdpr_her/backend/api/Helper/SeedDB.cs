using api.DataTemplate;
using Microsoft.AspNetCore.Identity;

namespace api.Helper;

public class SeedDB
{
    private readonly UserManager<Gebruiker> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private List<DTORegistreerErvaringsdeskundige> _deskundige;

    public SeedDB(UserManager<Gebruiker> userManager, RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        SetData();
        Seed();
    }

    private void Seed()
    {
        //Create roles if they don't exist, can be removed after running once and maybe put into some kind of start the service or just remove
        if (!_roleManager.RoleExistsAsync(Roles.Beheerder).Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = Roles.Beheerder;
            _roleManager.CreateAsync(role).Wait();
        }

        if (!_roleManager.RoleExistsAsync(Roles.Ervaringsdeskundige).Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = Roles.Ervaringsdeskundige;
            _roleManager.CreateAsync(role).Wait();
        }

        if (!_roleManager.RoleExistsAsync(Roles.Bedrijf).Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = Roles.Bedrijf;
            _roleManager.CreateAsync(role).Wait();
        }

        foreach (DTORegistreerErvaringsdeskundige e in _deskundige)
        {
            // Autogenerate: Email, postcode, telefoonnummer, wachtwoord
        }
    }

    private void SetData()
    {
        Random rnd = new Random();
        int telefoonnummer = 123456789;
        string ww = "Wachtwoord1!";
        int emailNum = 47;
        _deskundige = new List<DTORegistreerErvaringsdeskundige>
        {
            new DTORegistreerErvaringsdeskundige
            {
                Voornaam = "Steven", 
                Acternaam = "Wilson", 
                Gebruikersnaam = "Voyage34", 
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"mail_ed{emailNum}@mail.com", 
                Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
            },
            new DTORegistreerErvaringsdeskundige
            {
                Voornaam = "Jack", 
                Acternaam = "White", 
                Gebruikersnaam = "", 
                Wachtwoord = ww,
                Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
                Email = $"mail_ed{emailNum}@mail.com", 
                Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
            },
        };
    }
}
