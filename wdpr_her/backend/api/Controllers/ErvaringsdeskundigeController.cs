using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using api.DataTemplate;
using Microsoft.AspNetCore.Identity;

namespace api.Controllers;


[ApiController]
[Route("[controller]")] 
public class ErvaringsdeskundigeController : ControllerBase
{
    private readonly StichtingContext _context;
    
    public ErvaringsdeskundigeController(StichtingContext context)
    {
        _context = context;
    }

    
}