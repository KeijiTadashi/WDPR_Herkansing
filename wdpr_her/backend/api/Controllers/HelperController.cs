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

    #region Add/Post Enums
    [Authorize(Roles = Roles.Beheerder)]
    [HttpPost("AddOnderzoeksType")]
    public async Task<ActionResult> AddOnderzoeksType([FromBody] DTOHelper dto)
    {
        var onderzoeksType = new OnderzoeksType();
        if (await _context.OnderzoeksTypes.AnyAsync())
        {
            var max = await _context.OnderzoeksTypes.MaxAsync(o => o.BitFlag);
            await _context.OnderzoeksTypes.AddAsync(new OnderzoeksType() { BitFlag = max * 2, Type = dto.Naam });
            await _context.SaveChangesAsync();
            return StatusCode(201, onderzoeksType); // Created
        }
        
        onderzoeksType = new OnderzoeksType() { BitFlag = 1, Type = dto.Naam };
        await _context.SaveChangesAsync();
        return StatusCode(201, onderzoeksType); // Created
    }
    [Authorize(Roles = Roles.Beheerder)]
    [HttpPost("AddAandoening")]
    public async Task<ActionResult> AddAandoening([FromBody] DTOHelper dto)
    {
        var aandoening = new Aandoening();
        if (await _context.Aandoeningen.AnyAsync())
        {
            var max = await _context.Aandoeningen.MaxAsync(o => o.BitFlag);
            await _context.Aandoeningen.AddAsync(new Aandoening() { BitFlag = max * 2, Naam = dto.Naam });
            await _context.SaveChangesAsync();
            return StatusCode(201, aandoening); // Created
        }
        
        aandoening = new Aandoening() { BitFlag = 1, Naam = dto.Naam };
        await _context.SaveChangesAsync();
        return StatusCode(201, aandoening); // Created
    }
    [Authorize(Roles = Roles.Beheerder)]
    [HttpPost("AddBenadering")]
    public async Task<ActionResult> AddBenadering([FromBody] DTOHelper dto)
    {
        var benadering = new Benadering();
        if (await _context.Benaderingen.AnyAsync())
        {
            var max = await _context.Benaderingen.MaxAsync(o => o.BitFlag);
            await _context.Benaderingen.AddAsync(new Benadering() { BitFlag = max * 2, Soort = dto.Naam });
            await _context.SaveChangesAsync();
            return StatusCode(201, benadering); // Created
        }
        
        benadering = new Benadering() { BitFlag = 1, Soort = dto.Naam };
        await _context.SaveChangesAsync();
        return StatusCode(201, benadering); // Created
    }
    
    [Authorize(Roles = Roles.Beheerder)]
    [HttpPost("AddBeperking")]
    public async Task<ActionResult> AddBeperking([FromBody] DTOHelper dto)
    {
        var beperking = new Beperking();
        if (await _context.Beperkingen.AnyAsync())
        {
            var max = await _context.Beperkingen.MaxAsync(o => o.BitFlag);
            await _context.Beperkingen.AddAsync(new Beperking() { BitFlag = max * 2, Naam = dto.Naam });
            await _context.SaveChangesAsync();
            return StatusCode(201, beperking); // Created
        }
        
        beperking = new Beperking() { BitFlag = 1, Naam = dto.Naam };
        await _context.SaveChangesAsync();
        return StatusCode(201, beperking); // Created
    }
    #endregion

    #region Get Enums

    [HttpGet("GetOnderzoeksTypes")]
    public async Task<ActionResult<IEnumerable<OnderzoeksType>>> GetOnderzoeksTypes()
    {
        return await _context.OnderzoeksTypes.ToListAsync();
    }
    [HttpGet("GetAandoeningen")]
    public async Task<ActionResult<IEnumerable<Aandoening>>> GetAandoeningen()
    {
        return await _context.Aandoeningen.ToListAsync();
    }
    [HttpGet("GetBenaderingen")]
    public async Task<ActionResult<IEnumerable<Benadering>>> GetBenaderingen()
    {
        return await _context.Benaderingen.ToListAsync();
    }
    [HttpGet("GetBeperkingen")]
    public async Task<ActionResult<IEnumerable<Beperking>>> GetBeperkingen()
    {
        return await _context.Beperkingen.ToListAsync();
    }

    #endregion
    
}