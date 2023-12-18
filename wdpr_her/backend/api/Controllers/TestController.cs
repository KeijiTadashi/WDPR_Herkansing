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
        Console.WriteLine($"In CreateTest");
        Test t = new Test() { Name = tp.Name, IsTest = tp.Ditiseentestbool };
        await _context.Tests.AddAsync(t);
        await _context.SaveChangesAsync();
        Console.WriteLine($"Posted, {t.Id} {tp.Name}");
    }

    [HttpGet("{id}")]
    public async Task<Test> GetTest(int id)
    {
        Console.WriteLine("Get test");
        Test t = _context.Tests.First(t => t.Id == id);
        return t;
    }
}