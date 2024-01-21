using System.Security.Claims;
using api.DataTemplate;
using api.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SpecFlow.Internal.Json;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class OnderzoekController : ControllerBase
{
    private readonly StichtingContext _context;
    private readonly UserManager<Gebruiker> _userManager;

    public OnderzoekController(StichtingContext context, UserManager<Gebruiker> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [Authorize(Roles = $"{Roles.Beheerder}, {Roles.Bedrijf}")]
    [HttpPost("CreateOnderzoek")]
    public async Task<ActionResult> CreateOnderzoek([FromBody] DTOCreateOnderzoek dto)
    {
        try
        {
            string userName;
            userName = User.FindFirstValue(ClaimTypes.Name);
            Console.WriteLine($"Username: {userName}");
            var uitvoerder = await _userManager.FindByNameAsync(userName);
            if (uitvoerder == null)
                return BadRequest(
                    "De gebruiker die het onderzoek aanmaakt bestaat helaas niet... zou nooit moeten gebeuren want anders faalt hij al bij de authorize");

            List<OnderzoeksType> onderzoeksTypes = new List<OnderzoeksType>();
            foreach (var ot in dto.OnderzoeksTypes)
            {
                onderzoeksTypes.Add(await _context.OnderzoeksTypes.FirstAsync(o => o.Id == ot));
            }

            Onderzoek onderzoek = new Onderzoek
            {
                Uitvoerder = uitvoerder,
                OnderzoeksType = onderzoeksTypes,
                Beloning = dto.Beloning,
                Beschrijving = dto.Beschrijving,
                Locatie = dto.Locatie,
                Titel = dto.Titel,
                OnderzoeksData = dto.OnderzoeksData
            };

            await _context.Onderzoeken.AddAsync(onderzoek);
            await _context.SaveChangesAsync();

            return Ok("Onderzoek is opgeslagen.");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: er gaat iets mis in OnderzoekController/CreateOnderzoek\n{e}");
        }
    }

    // [Authorize(Roles = $"{Roles.Bedrijf}, {Roles.Beheerder}, {Roles.Ervaringsdeskundige}")]
    [HttpGet("GetOnderzoek/{id}")]
    public async Task<ActionResult<DTOGetOnderzoek>> GetOnderzoek(int id)
    {
        try
        {
            DTOGetOnderzoek response;
            string uitvoerderNaam = "";
            var onderzoek = await _context.Onderzoeken.Include(o => o.Uitvoerder).Include(o => o.OnderzoeksType).FirstOrDefaultAsync(o => o.Id == id);
            if (onderzoek == null)
                return BadRequest($"Onderzoek met id: {id} bestaat niet.");
            
            // _context.OnderzoeksTypes.Where(ot => )_context.Onderzoeken.

            var onderzoeksTypes = new List<int>();
            foreach (var ot in onderzoek.OnderzoeksType)
            {
                onderzoeksTypes.Add(ot.Id);
                // onderzoeksType.Add(await _context.OnderzoeksTypes.FirstOrDefaultAsync(o => o.Id == ot.Id));
            }

            // Console.WriteLine(onderzoek.ToJson());
            Console.WriteLine("Uitvoerder: " + onderzoek.Uitvoerder);
            Console.WriteLine("Id: " + onderzoek.Uitvoerder.Id);

            // var uitvoerder = await _userManager.FindByIdAsync(onderzoek.Uitvoerder.Id);
            if (onderzoek.Uitvoerder.AccountType == Roles.Bedrijf)
            {
                uitvoerderNaam = ((Bedrijf)onderzoek.Uitvoerder).Naam;
            }
            else
            {
                uitvoerderNaam = ((Persoon)onderzoek.Uitvoerder).Voornaam + ((Persoon)onderzoek.Uitvoerder).Achternaam;
            }

            Console.WriteLine("Naam: " + uitvoerderNaam);
            response = new DTOGetOnderzoek
            {
                OnderzoekId = onderzoek.Id,
                UitvoerderId = onderzoek.Uitvoerder.Id,
                Titel = onderzoek.Titel,
                Beloning = onderzoek.Beloning,
                Beschrijving = onderzoek.Beschrijving,
                Locatie = onderzoek.Locatie,
                OnderzoeksType = onderzoeksTypes,
                OnderzoeksData = onderzoek.OnderzoeksData,
                UitvoerderNaam = uitvoerderNaam
            };
            
            return Ok(response);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: er gaat iets mis in OnderzoekController/GetOnderzoek\n{e}");
        }
    }
}