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
    public async Task<ActionResult<IEnumerable<DTOVragenOphalen>>> GetVragen(int id)
    {
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
}