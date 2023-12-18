using api.DataTemplate;
using Microsoft.AspNetCore.Mvc;

namespace api;


[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly StichtingContext _context;
    
    public TestController(StichtingContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task CreateTest([FromBody] TestPost tp)
    {
        await _context.Tests.AddAsync(new Test() {Name = tp.Name, IsTest = tp.Ditiseentestbool});
        await _context.SaveChangesAsync();
    }
}