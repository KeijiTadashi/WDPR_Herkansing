using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class OpdrachtResponsController : ControllerBase{
    private readonly StichtingContext _context;

    public OpdrachtResponsController(StichtingContext context){
        _context = context;
    }

    [Authorize(Roles = $"Roles.Ervaringsdeskundige")]
    [HttpPost]
    public async Task<IActionResult> CreateOpdrachtRespons([FromBody] OpdrachtRespons opdrachtRespons){
        try{
            if(opdrachtRespons == null){
                return BadRequest("Invalid OpdrachtRespons data");
            }

            _context.OpdrachtResponsEntries.Add(opdrachtRespons);
            await _context.SaveChangesAsync();

            return Ok(new{message = "OpdrachtRespons created successfully :D", opdrachtRespons.ResponsID});
        }
        catch(Exception e){
            return StatusCode(500, "Internal server error: er gaat iets mis in CreateOpdrachtRespons");
        }
    }

    [HttpGet("{opdrachtResponsId}")]
    public IActionResult GetOpdrachtRespons(int opdrachtResponsId)
    {
        try
        {
            var opdrachtRespons = _context.OpdrachtResponsEntries
                                    .Include(o => o.UserID)
                                    .Include(o => o.OnderzoekID)
                                    .FirstOrDefault(o => o.ResponsID == opdrachtResponsId);

            if (opdrachtRespons == null)
            {
                return NotFound("OpdrachtRespons not found");
            }

            return Ok(opdrachtRespons);
        }
        catch (Exception uitzondering)
        {
            // Log the exception for debugging purposes
            // Log.Error(ex, $"Error retrieving OpdrachtRespons with ID {opdrachtResponsId}");
            return StatusCode(500, "Internal server error");
        }
    }
}

