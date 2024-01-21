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
            string userName;
            userName = User.FindFirstValue(ClaimTypes.Name);
            
            Ervaringsdeskundige ervaringsdeskundige = (Ervaringsdeskundige)await _userManager
                .FindByNameAsync(userName);
            
            
            // int usersCount = await _userManager.Users
            // .Where(u => u.AccountType == "Ervaringsdeskundige")
            // .CountAsync();
            //
            // var random = new Random();
            // int skip = random.Next(usersCount);

            // Ervaringsdeskundige deskundige = (Ervaringsdeskundige)await _userManager.Users
            //     .Where(u => u.AccountType == "Ervaringsdeskundige")
            //     .Skip(skip)
            //     .FirstOrDefaultAsync();

            // var users = await _userManager.Users.ToListAsync();


            DTOGetErvaringsdeskundige dto = new DTOGetErvaringsdeskundige
            {
                UserName = ervaringsdeskundige.UserName,
                Voornaam = ervaringsdeskundige.Voornaam,
                Achternaam = ervaringsdeskundige.Achternaam,
                Postcode = ervaringsdeskundige.Postcode,
                TelefoonNummer = ervaringsdeskundige.PhoneNumber,
                Email = ervaringsdeskundige.Email,
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
