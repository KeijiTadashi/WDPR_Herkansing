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

    [HttpGet("GetAllBedrijven")]
    public async Task<ActionResult<IEnumerable<Bedrijf>>> GetAllBedrijven()
    {
        return await _context.Bedrijven.ToListAsync();
    }
}
