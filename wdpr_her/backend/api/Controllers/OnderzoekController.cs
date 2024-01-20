﻿using System.Security.Claims;
using api.DataTemplate;
using api.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
}