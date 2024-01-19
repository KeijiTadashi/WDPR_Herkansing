using api.DataTemplate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;


[ApiController]
[Route("[controller]")]
public class OpdrachtController : ControllerBase
{
    private readonly StichtingContext context;

    public OpdrachtController(StichtingContext context)
    {
        this.context = context;
    }

    [HttpGet("GetVragen")]

    public async Task<ActionResult<IEnumerable<DTOVragenOphalen>>> GetVragen(string id)
    {
        string em = "";
        try
        {
            em="Er gaat wat mis met het ophalen van de vragen";
            var vragen = await context.Onderzoeken.Where(v => id == v.Id).Select(v => new DTOVragenOphalen()
            {
                ID = v.Id,
                Titel = v.Titel,
                UitvoerderNaam = v.Uitvoerder.UserName,
                Locatie = v.Locatie,
                Beloning = v.Beloning,
                Beschrijving = v.Beschrijving,
                OnderzoeksData = v.OnderzoeksData
            }).ToListAsync();
            return (vragen);
        }
        catch (Exception ᒥᐢᑕᓇᐢᐠ)
        {
            //"Badger" in Cree
            Console.WriteLine(ᒥᐢᑕᓇᐢᐠ);
            Console.WriteLine(em);
            return StatusCode(500, "Internal server error: er gaat iets mis in OpdrachtController/GetVragen. Error:"+em);
        }
    }
}

