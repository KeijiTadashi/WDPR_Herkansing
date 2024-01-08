using api.DataTemplate;
using api.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class HelperController : ControllerBase
{
    private readonly StichtingContext _context;

    public HelperController(StichtingContext context)
    {
        _context = context;
    }

    private async Task<ActionResult> AddItem(string table, string item)
    {
        return Ok();
    }

    // [Authorize(Roles = Roles.Beheerder)]
    [HttpPost("AddOnderzoeksType")]
    public async Task<ActionResult> AddOnderzoeksType([FromBody] DTOHelper dto)
    {
        if (await _context.OnderzoeksTypes.CountAsync() == 0)
        {
            
        }
        
        var max = await _context.OnderzoeksTypes.MaxAsync(o => o.BitFlag);
        if (max == null)
            return Ok("There is nothing in the database");
        return AddItem(BitflagTypes.OnderzoeksType, dto.Naam).Result;
    }

}