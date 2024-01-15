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

    [Authorize(Roles = Roles.Beheerder)]
    [HttpPost("AddOnderzoeksType")]
    public async Task<ActionResult> AddOnderzoeksType([FromBody] DTOHelper dto)
    {
        try
        {
            var onderzoeksType = new OnderzoeksType();
            await _context.OnderzoeksTypes.AddAsync(new OnderzoeksType() { Type = dto.Naam });
            await _context.SaveChangesAsync();
            return StatusCode(201, onderzoeksType); // Created
        }
        catch (Exception ב)
        {//Bet
            return StatusCode(500, "Internal server error: er gaat iets mis in HelperController/AddOnderzoeksType");
        }
    }

    [Authorize(Roles = Roles.Beheerder)]
    [HttpPost("AddAandoening")]
    public async Task<ActionResult> AddAandoening([FromBody] DTOHelper dto)
    {
        try
        {
            var aandoening = new Aandoening();
            await _context.Aandoeningen.AddAsync(new Aandoening() { Naam = dto.Naam });
            await _context.SaveChangesAsync();
            return StatusCode(201, aandoening); // Created
        }
        catch (Exception ג)
        {//Gimel
            return StatusCode(500, "Internal server error: er gaat iets mis in HelperController/AddAandoening");
        }
    }

    [Authorize(Roles = Roles.Beheerder)]
    [HttpPost("AddBenadering")]
    public async Task<ActionResult> AddBenadering([FromBody] DTOHelper dto)
    {
        try
        {
            var benadering = new Benadering();
            await _context.Benaderingen.AddAsync(new Benadering() { Soort = dto.Naam });
            await _context.SaveChangesAsync();
            return StatusCode(201, benadering); // Created
        }
        catch (Exception ד)
        {//Dalet
            return StatusCode(500, "Internal server error: er gaat iets mis in HelperController/AddBenadering");
        }
    }

    [Authorize(Roles = Roles.Beheerder)]
    [HttpPost("AddBeperking")]
    public async Task<ActionResult> AddBeperking([FromBody] DTOHelper dto)
    {
        try
        {
            var beperking = new Beperking();
            await _context.Beperkingen.AddAsync(new Beperking() { Naam = dto.Naam });
            await _context.SaveChangesAsync();
            return StatusCode(201, beperking);// Created
        }
        catch (Exception ה)
        {//Hee
            return StatusCode(500, "Internal server error: er gaat iets mis in HelperController/AddBeperking");
        }
    }

    [Authorize(Roles = Roles.Beheerder)]
    [HttpPost("AddHulpmiddel")]
    public async Task<ActionResult> AddHulpmiddel([FromBody] DTOHelper dto)
    {
        try
        {
            var hulpmiddel = new Hulpmiddel();
            await _context.Hulpmiddelen.AddAsync(new Hulpmiddel() { Naam = dto.Naam });
            await _context.SaveChangesAsync();
            return StatusCode(201, hulpmiddel); // Created
        }
        catch (Exception ו)
        {//Waw
            return StatusCode(500, "Internal server error: er gaat iets mis in HelperController/AddBeperking");
        }
    }

    /*
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

            await _context.OnderzoeksTypes.AddAsync(new OnderzoeksType() { BitFlag = 1, Type = dto.Naam });
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
            await _context.Aandoeningen.AddAsync(new Aandoening() { BitFlag = 1, Naam = dto.Naam });
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
            await _context.Benaderingen.AddAsync(new Benadering() { BitFlag = 1, Soort = dto.Naam });
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
            await _context.Beperkingen.AddAsync(new Beperking() { BitFlag = 1, Naam = dto.Naam });
            await _context.SaveChangesAsync();
            return StatusCode(201, beperking); // Created
        }
        #endregion
    */
    #region Get Enums

    [HttpGet("GetOnderzoeksTypes")]
    public async Task<ActionResult<IEnumerable<OnderzoeksType>>> GetOnderzoeksTypes()
    {
        try
        {
            return await _context.OnderzoeksTypes.ToListAsync();
        }
        catch (Exception ז)
        {//Zajien
            return StatusCode(500, "Internal server error: er gaat iets mis in HelperController/AddBeperking");
        }
    }

    [HttpGet("GetAandoeningen")]
    public async Task<ActionResult<IEnumerable<Aandoening>>> GetAandoeningen()
    {
        try
        {
            return await _context.Aandoeningen.ToListAsync();
        }
        catch (Exception ח)
        {//Chet
            return StatusCode(500, "Internal server error: er gaat iets mis in HelperController/AddBeperking");
        }
    }

    [HttpGet("GetBenaderingen")]
    public async Task<ActionResult<IEnumerable<Benadering>>> GetBenaderingen()
    {
        try
        {
            return await _context.Benaderingen.ToListAsync();
        }
        catch (Exception ט)
        {//Tet
            return StatusCode(500, "Internal server error: er gaat iets mis in HelperController/AddBeperking");
        }
    }

    [HttpGet("GetBeperkingen")]
    public async Task<ActionResult<IEnumerable<Beperking>>> GetBeperkingen()
    {
        try
        {
            return await _context.Beperkingen.ToListAsync();
        }
        catch (Exception י)
        {//Jod
            return StatusCode(500, "Internal server error: er gaat iets mis in HelperController/AddBeperking");
        }
    }

    [HttpGet("GetHulpmiddelen")]
    public async Task<ActionResult<IEnumerable<Hulpmiddel>>> GetHulpmiddelen()
    {
        try
        {
            return await _context.Hulpmiddelen.ToListAsync();
        }
        catch (Exception כ)
        {//Kaf
            return StatusCode(500, "Internal server error: er gaat iets mis in HelperController/AddBeperking");
        }
    }

    #endregion

}