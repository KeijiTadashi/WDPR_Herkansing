using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using api.DataTemplate;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class ErvaringsdeskundigeController : ControllerBase
{
    private readonly StichtingContext _context;
    private readonly UserManager<Gebruiker> _userManager;

    public ErvaringsdeskundigeController(StichtingContext context, UserManager<Gebruiker> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // getalle-Ervaringdeskundige // hiervan ook dto aanmaken 

    [HttpGet("GetErvaringsdeskundige")]
    public async Task<ActionResult<DTOGetErvaringsdeskundige>> GetErvaringdeskundige()
    {
        try
        {
            int usersCount = await _userManager.Users
            .Where(u => u.AccountType == "Ervaringsdeskundige")
            .CountAsync();

            var random = new Random();
            int skip = random.Next(usersCount);

            Ervaringsdeskundige deskundige = (Ervaringsdeskundige)await _userManager.Users
                .Where(u => u.AccountType == "Ervaringsdeskundige")
                .Skip(skip)
                .FirstOrDefaultAsync();

            // var users = await _userManager.Users.ToListAsync();


            DTOGetErvaringsdeskundige dto = new DTOGetErvaringsdeskundige
            {
                UserName = deskundige.UserName,
                Voornaam = deskundige.Voornaam,
                Achternaam = deskundige.Achternaam,
                Postcode = deskundige.Postcode,
                TelefoonNummer = deskundige.PhoneNumber,
                Email = deskundige.Email,
            };
            return Ok(dto);
        }
        catch (Exception ex)
        {
            Console.Write(ex);
            return StatusCode(500, "Internal server error: er gaat iets mis in ErvaringsdeskundigeController/GetErvaringsdeskundige");
        }
    }
}
