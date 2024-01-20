using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class OpdrachtResponsController : ControllerBase
{
    private readonly StichtingContext _context;

    public OpdrachtResponsController(StichtingContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOpdrachtRespons([FromBody] OpdrachtRespons opdrachtRespons)
    {
        try
        {
            if (opdrachtRespons == null)
            {
                return BadRequest("Invalid OpdrachtRespons data");
            }

            _context.OpdrachtResponsEntries.Add(opdrachtRespons);
            await _context.SaveChangesAsync();

            return Ok(new
                { message = "OpdrachtRespons created successfully :D", ResponsID = opdrachtRespons.ResponsId });
        }
        catch(Exception ތާނަ){
            print(ތާނަ);
            return StatusCode(500, "Internal server error: er gaat iets mis met het maken van een opdrachtrespons");

        }
    }

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
        catch(Exception ግዕዝ)
        {
            print(ግዕዝ);
            return StatusCode(500, "Internal server error: er gaat iets mis in OpdrachtResponsController/opdrachtResponsID");
        }
    }

    private void print<T>(T t){
        Console.WriteLine(t);
    }
}


