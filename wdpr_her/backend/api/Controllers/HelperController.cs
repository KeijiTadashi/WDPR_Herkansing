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
        string em = "";
        try
        {;
            var onderzoeksType = new OnderzoeksType();

            
            em="Er gaat wat mis met het toevoegen van een onderzoekstype in de database";
            await _context.OnderzoeksTypes.AddAsync(new OnderzoeksType() { Type = dto.Naam });

            em="Er gaat wat mis met het opslaan van een onderzoekstype in de databasse";
            await _context.SaveChangesAsync();
            return StatusCode(201, onderzoeksType); // Created
        }
        catch (Exception ב)
        {//Bet
            print(ב);
            print(em);
            return StatusCode(500, "Internal server error: er gaat iets mis in HelperController/AddOnderzoeksType. Error:"+em);

        }
    }

    [Authorize(Roles = Roles.Beheerder)]
    [HttpPost("AddAandoening")]
    public async Task<ActionResult> AddAandoening([FromBody] DTOHelper dto)
    {
        string em = "";
        try
        {
            var aandoening = new Aandoening();

            em="Er gaat wat mis met het toevoegen van een aandoening";
            await _context.Aandoeningen.AddAsync(new Aandoening() { Naam = dto.Naam });

            em="Er gaat wat mis met het opslaan van een aandoening in de database";
            await _context.SaveChangesAsync();
            return StatusCode(201, aandoening); // Created
        }
        catch (Exception ג)
        {//Gimel
            print(ג);
            return StatusCode(500, "Internal server error: er gaat iets mis in HelperController/AddAandoening. Error:"+em);

        }
    }

    [Authorize(Roles = Roles.Beheerder)]
    [HttpPost("AddBenadering")]
    public async Task<ActionResult> AddBenadering([FromBody] DTOHelper dto)
    {
        string em="";
        try
        {
            var benadering = new Benadering();

            em="Er gaat wat mis met het toevoegen van een benadering";
            await _context.Benaderingen.AddAsync(new Benadering() { Soort = dto.Naam });
            await _context.SaveChangesAsync();
            return StatusCode(201, benadering); // Created
        }
        catch (Exception ד)
        {//Dalet
            print( ד);
            print(em);
            return StatusCode(500, "Internal server error: er gaat iets mis in HelperController/AddBenadering. Error:"+em);

        }
    }

    [Authorize(Roles = Roles.Beheerder)]
    [HttpPost("AddBeperking")]
    public async Task<ActionResult> AddBeperking([FromBody] DTOHelper dto)
    {
        string em = "";
        try
        {
            var beperking = new Beperking();

            em="Er gaat wat mis met het toevoegen van een beperking";
            await _context.Beperkingen.AddAsync(new Beperking() { Naam = dto.Naam });
            await _context.SaveChangesAsync();
            return StatusCode(201, beperking); // Created
        }
        catch (Exception ה)

        {//Hee
            print(ה);
            print(em);
            return StatusCode(500, "Internal server error: er gaat iets mis in HelperController/AddBeperking. Error:"+em);

        }
    }

    [Authorize(Roles = Roles.Beheerder)]
    [HttpPost("AddHulpmiddel")]
    public async Task<ActionResult> AddHulpmiddel([FromBody] DTOHelper dto)
    {
        string em = "";
        try
        {
            var hulpmiddel = new Hulpmiddel();

            em="Er gaat wat mis met het toevoegen van een hulpmiddel";
            await _context.Hulpmiddelen.AddAsync(new Hulpmiddel() { Naam = dto.Naam });
            await _context.SaveChangesAsync();
            return StatusCode(201, hulpmiddel); // Created
        }
        catch (Exception ו)
        {//Waw'
            print(ו);
            print(em);
            return StatusCode(500, "Internal server error: er gaat iets mis in HelperController/AddHulpmiddel. Error:"+em);

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
        string em = "Er gaat wat mis in OnderzoeksTypes";
        try
        {
            return await _context.OnderzoeksTypes.ToListAsync();
        }
        catch (Exception ז)
        {//Zajien
            print(ז);
            print(em);
            return StatusCode(500, "Internal server error: er gaat iets mis in HelperController/GetOnderzoeksTypes. Error:"+em);

    }}

    [HttpGet("GetAandoeningen")]
    public async Task<ActionResult<IEnumerable<Aandoening>>> GetAandoeningen()
    {
        string em="Er gaat wat mis met het ophalen van de aandoeningen";
        try
        {
            return await _context.Aandoeningen.ToListAsync();
        }
        catch (Exception ח)
        {//Chet
            print(ח);
            print(em);
            return StatusCode(500, "Internal server error: er gaat iets mis in HelperController/GetAandoeningen. Error:"+em);

        }
    }

    [HttpGet("GetBenaderingen")]
    public async Task<ActionResult<IEnumerable<Benadering>>> GetBenaderingen()
    {
        string em = "Er gaat wat mis met het ophalen van de benaderingen";
        try
        {
            return await _context.Benaderingen.ToListAsync();
        }
        catch (Exception ט)
        {//Tet
            print(ט);
            print(em);
            return StatusCode(500, "Internal server error: er gaat iets mis in HelperController/GetBenaderingen. Error:"+em);

        }
    }

    [HttpGet("GetBeperkingen")]
    public async Task<ActionResult<IEnumerable<Beperking>>> GetBeperkingen()
    {
        string em = "Er gaat wawt mis met het ophalen van de beperkingen";
        try
        {
            return await _context.Beperkingen.ToListAsync();
        }
        catch (Exception י)
        {//Jod
            print(י);
            print(em);
            return StatusCode(500, "Internal server error: er gaat iets mis in HelperController/GetBeperkingen. Error:"+em);

        }
    }

    [HttpGet("GetHulpmiddelen")]
    public async Task<ActionResult<IEnumerable<Hulpmiddel>>> GetHulpmiddelen()
    {
        string em = "Er gaat wat mis met het ophalen van de Hulpmiddelen";
        try
        {
            return await _context.Hulpmiddelen.ToListAsync();
        }
        catch (Exception כ)
        {//Kaf
            print(כ);
            print(em);
            return StatusCode(500, "Internal server error: er gaat iets mis in HelperController/GetHulpmiddelen. Error:"+em);

        }
    }

    #endregion
    private void print<T>(T t){
    //I love python
        Console.WriteLine(t);
    }

}