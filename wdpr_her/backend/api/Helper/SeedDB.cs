// using api.DataTemplate;
// using api;
// using Microsoft.AspNetCore.Identity;
//
// namespace api.Helper;
//
// public class SeedDB
// {
//     private readonly UserManager<Gebruiker> _userManager;
//     private static readonly RoleManager<IdentityRole> _roleManager;
//     private readonly IConfiguration _configuration;
//     private List<DTORegistreerErvaringsdeskundige> _deskundigenLijst;
//
//     public SeedDB(UserManager<Gebruiker> userManager, RoleManager<IdentityRole> roleManager,
//         IConfiguration configuration)
//     {
//         _userManager = userManager;
//         _roleManager = roleManager;
//         _configuration = configuration;
//         SetData();
//         Seed();
//     }
//
//     private static void Seed()
//     {
//         //Create roles if they don't exist, can be removed after running once and maybe put into some kind of start the service or just remove
//         if (!_roleManager.RoleExistsAsync(Roles.Beheerder).Result)
//         {
//             IdentityRole role = new IdentityRole();
//             role.Name = Roles.Beheerder;
//             _roleManager.CreateAsync(role).Wait();
//         }
//
//         if (!_roleManager.RoleExistsAsync(Roles.Ervaringsdeskundige).Result)
//         {
//             IdentityRole role = new IdentityRole();
//             role.Name = Roles.Ervaringsdeskundige;
//             _roleManager.CreateAsync(role).Wait();
//         }
//
//         if (!_roleManager.RoleExistsAsync(Roles.Bedrijf).Result)
//         {
//             IdentityRole role = new IdentityRole();
//             role.Name = Roles.Bedrijf;
//             _roleManager.CreateAsync(role).Wait();
//         }
//
//         foreach (DTORegistreerErvaringsdeskundige e in _deskundigenLijst)
//         {
//             var userExists = _userManager.FindByNameAsync(e.Gebruikersnaam);
//             if (userExists == null)
//             {
//                 Ervaringsdeskundige deskundige = new Ervaringsdeskundige()
//                 {
//                     Email = e.Email,
//                     Voornaam = e.Voornaam,
//                     Achternaam = e.Acternaam,
//                     Postcode = e.Postcode,
//                     UserName = e.Gebruikersnaam,
//                     PhoneNumber = e.Telefoonnummer,
//                     AccountType = Roles.Ervaringsdeskundige
//                     //Nog meer data over beperkingen en dergelijke, volgt later als dit allemaal werk
//                 };
//                 
//                 var result = await _userManager.CreateAsync(deskundige, e.Wachtwoord);
//                 result = await _userManager.AddToRoleAsync(deskundige, Roles.Ervaringsdeskundige);
//                 Console.WriteLine(result.Succeeded);
//             }
//         }
//     }
//
//     private void SetData()
//     {
//         Random rnd = new Random();
//         int telefoonnummer = 123456789;
//         string ww = "Wachtwoord1!";
//         int emailNum = 47;
//         _deskundigenLijst = new List<DTORegistreerErvaringsdeskundige>
//         {
//             new DTORegistreerErvaringsdeskundige
//             {
//                 Voornaam = "Steven", 
//                 Acternaam = "Wilson", 
//                 Gebruikersnaam = "Voyage34", 
//                 Wachtwoord = ww,
//                 Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
//                 Email = $"mail_ed{emailNum}@mail.com", 
//                 Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
//             },
//             new DTORegistreerErvaringsdeskundige
//             {
//                 Voornaam = "Jack", 
//                 Acternaam = "White", 
//                 Gebruikersnaam = "Raconteurs", 
//                 Wachtwoord = ww,
//                 Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
//                 Email = $"mail_ed{++emailNum}@mail.com", 
//                 Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
//             },
//             new DTORegistreerErvaringsdeskundige
//             {
//                 Voornaam = "Maynard", 
//                 Acternaam = "Keenan", 
//                 Gebruikersnaam = "A_Perfect_Circle", 
//                 Wachtwoord = ww,
//                 Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
//                 Email = $"mail_ed{++emailNum}@mail.com", 
//                 Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
//             },
//             new DTORegistreerErvaringsdeskundige
//             {
//                 Voornaam = "John", 
//                 Acternaam = "Entwistle", 
//                 Gebruikersnaam = "ThunderFingers", 
//                 Wachtwoord = ww,
//                 Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
//                 Email = $"mail_ed{++emailNum}@mail.com", 
//                 Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
//             },
//             new DTORegistreerErvaringsdeskundige
//             {
//                 Voornaam = "Cliff", 
//                 Acternaam = "Burton", 
//                 Gebruikersnaam = "RIP", 
//                 Wachtwoord = ww,
//                 Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
//                 Email = $"mail_ed{++emailNum}@mail.com", 
//                 Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
//             },
//             new DTORegistreerErvaringsdeskundige
//             {
//                 Voornaam = "Jeff", 
//                 Acternaam = "Wayne", 
//                 Gebruikersnaam = "War_of_the_worlds", 
//                 Wachtwoord = ww,
//                 Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
//                 Email = $"mail_ed{++emailNum}@mail.com", 
//                 Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
//             },
//             new DTORegistreerErvaringsdeskundige
//             {
//                 Voornaam = "Till", 
//                 Acternaam = "Lindemann", 
//                 Gebruikersnaam = "Rammstein", 
//                 Wachtwoord = ww,
//                 Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
//                 Email = $"mail_ed{++emailNum}@mail.com", 
//                 Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
//             },
//             new DTORegistreerErvaringsdeskundige
//             {
//                 Voornaam = "Franck", 
//                 Acternaam = "Hueso", 
//                 Gebruikersnaam = "Carpenter Brut", 
//                 Wachtwoord = ww,
//                 Telefoonnummer = $"+{rnd.NextInt64(99)}-{telefoonnummer + rnd.NextInt64(999999999)}",
//                 Email = $"mail_ed{++emailNum}@mail.com", 
//                 Postcode = $"{rnd.NextInt64(1000, 9999)} AB"
//             },
//         };
//     }
// }
