using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.DataTemplate;
using Microsoft.AspNetCore.Identity;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class OpdrachtResponsController : ControllerBase
{
    private readonly StichtingContext _context;
    private readonly UserManager<Gebruiker> _userManager;

    public OpdrachtResponsController(StichtingContext context, UserManager<Gebruiker> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [Authorize]
    [HttpPost("CreateOpdrachtRespons")]
    public async Task<IActionResult> CreateOpdrachtRespons([FromBody] DTOCreateOpdrachtRespons dto)
    {
        try
        {
            if (dto == null)
            {
                return BadRequest("Geen OpdrachtRespons data");
            }
            
            string userName;
            userName = User.FindFirstValue(ClaimTypes.Name);

            Gebruiker gebruiker = await _userManager.FindByNameAsync(userName);
            if (gebruiker == null)
                return BadRequest("Should never get here cause authorize should fail, but no user found...");

            OpdrachtRespons opdrachtRespons = new OpdrachtRespons
            {
                OnderzoekId = dto.OnderzoekId,
                Gebruiker = gebruiker,
                VraagMetAntwoordenJSON = dto.VraagMetAntwoordenJSON
            };
            
            await _context.OpdrachtResponsEntries.AddAsync(opdrachtRespons);
            await _context.SaveChangesAsync();

            return Created("OpdrachtRespons", opdrachtRespons);
            // return Ok(new
            //     { message = "OpdrachtRespons created successfully :D", ResponsID = opdrachtRespons.ResponsId });
        }
        catch
        {
            return StatusCode(500, "Internal server error: er gaat iets mis in CreateOpdrachtRespons");
        }
    }

    [Authorize]
    [HttpGet("{opdrachtResponsId}")]
    public IActionResult GetOpdrachtRespons(int opdrachtResponsId)
    {
        try
        {
            var opdrachtRespons = _context.OpdrachtResponsEntries
                .Include(o => o.Gebruiker)
                .Include(o => _context.Onderzoeken.Where(onderzoek => o.OnderzoekId == onderzoek.Id))
                .FirstOrDefault(o => o.ResponsId == opdrachtResponsId);

            if (opdrachtRespons == null)
            {
                return NotFound("OpdrachtRespons not found");
            }

            return Ok(opdrachtRespons);
        }
        catch
        {
            return StatusCode(500,
                "Internal server error: er gaat iets mis in OpdrachtResponsController/opdrachtResponsID");
        }
    }
}