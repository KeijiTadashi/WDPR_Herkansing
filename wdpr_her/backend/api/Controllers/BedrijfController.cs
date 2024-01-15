
using api.DataTemplate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;
[ApiController]
[Route("[controller]")]
public class BedrijfController : ControllerBase
{

    private readonly StichtingContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<Gebruiker> _userManager;
    public BedrijfController(UserManager<Gebruiker> userManager, RoleManager<IdentityRole> roleManager, StichtingContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
    }



    // TODO CHANGE TO USEFULL INFO AND NEW DTO
    [HttpGet("GetAllBedrijven")]
    public async Task<ActionResult<IEnumerable<DTOLogin>>> GetAllBedrijven()
    {
        try
        {

            var bedrijven = await _context.Bedrijven.Select(b => new DTOLogin()
            {
                Gebruikersnaam = b.UserName,
                Wachtwoord = b.PasswordHash
                //etc
            }).ToListAsync();
            return bedrijven;
        }catch(Exception Þ){
        //hoofdletter þ (thorn)
        return StatusCode(500, "Internal server error: er gaat iets mis in BedrijfController/GetAllBedrijven");
        }
    }
}
