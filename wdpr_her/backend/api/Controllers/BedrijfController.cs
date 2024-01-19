using api.DataTemplate;
using Microsoft.AspNetCore.Identity;
 using api.Helper;
 using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

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

    
    // Commented out for easier testing
    // [Authorize(Roles = Roles.Beheerder)]

    [HttpGet("GetAllBedrijven")]
    public async Task<ActionResult<IEnumerable<DTOGetBedrijf>>> GetAllBedrijven()
    {
        String ErrorMessage = "";
        try
        {
            ErrorMessage="Er gaat was mis met het ophalen van alle bedrijven";
            var bedrijven = await _context.Bedrijven.Select(b => new DTOGetBedrijf()
            {
                Bedrijfsnaam = b.Naam,
                Email = b.Email,
                Kvk = b.Kvk,
                Telefoonnummer = b.PhoneNumber,
                Locatie = b.Locatie, 
                Website = b.Website
            }).ToListAsync();
            return Ok(bedrijven);
        }catch(Exception ex){
            Console.Write(ex);
            Console.Write(ErrorMessage);
            return StatusCode(500, "Internal server error: er gaat iets mis in BedrijfController/GetAllBedrijven");
        }

    }
}