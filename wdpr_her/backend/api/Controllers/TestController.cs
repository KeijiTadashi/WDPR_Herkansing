using api.DataTemplate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api;

/*
 * Stappen voor het maken van een nieuw api ingang.
 * (Optioneel) Als je nog geen {Nieuwe}Controller.cs hebt, maak er een
 * -    Copy basically alles wat je hieronder ziet vanaf "[ApiController] ... End Create Step"
 *      Met je {Nieuwe}Controller inplaats van TestController
 * 
 * Maak een nieuwe [HttpPost/Put/Get/Delete] zie examples beneden (2x Post / 2x Get / 1x Delete / 1x Put).
 * Als je niet alle gegevens wil kan je gebruik maken van [FromBody] (zie CreateTest en react AddTest) maak in dat geval een nieuwe DTO (DataTemplateObject)
 * met de gegevens die je wil hebben (of als los argument, maar [FromBody] heeft eigenlijk altijd de voorkeur
 * zie put (UpdateIsTest()) en de axios put (UpdateTestBool) in react als voorbeeld van losse argumenten)
 *      Het verschil is dat losse argumenten in de url komen te staan en bij een FromBody in de Request
 *      Functie(var a) => {hele url}/?a=mijnvalue
 *      Functie([FromBody] DTOA) => {hele url}
 * Doe stuff in je functie
 * Return een statuscode (met of zonder data) of de data (which should give code 200 OK)
 *
 * REACT EXAMPLES: see Components/ApiExample (url/test)
 */


/*
 * De route van de api die nodig is om deze endpoints te bereiken.
 * [controller] = de naam van de controllerclass zonder controller => TestController -> Test
 *      {api url}/Test
 */
[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly StichtingContext _context;

    public TestController(StichtingContext context)
    {
        _context = context;
    }
    // End Create Step

    // Route = {api url}/Test/CreateTest
    [HttpPost("CreateTest")]
    public async Task<ActionResult> CreateTest([FromBody] DTOCreateTest ct)
    {
        try
        {
            Test t = new Test() { Name = ct.Name, IsTest = ct.DitIsEenTestBool };


            await _context.Tests.AddAsync(t); // Add this to the list of changes to make during SaveChanges(Async)
            var saved = await _context.SaveChangesAsync(); // 


            // return StatusCode(201); // 201 = Created
            return Created("TestDB", $"Saved {saved} tests");
        }
        catch (Exception इ)
        {
            //talavya
            return StatusCode(500, "Internal server error: er gaat iets mis in TestControler/CreateTest");
        }
    }

    // Optional extra route -> {api url}/Test/CreateTestDefault
    [HttpPost("CreateTestDefault")]
    public async Task<ActionResult> CreateTest2()
    {
        try
        {
            await _context.Tests.AddAsync(new Test() { Name = "Default", IsTest = true });
            await _context.SaveChangesAsync();
            return Ok(); // Return Ok (200) without data
        }
        catch (Exception उ)
        {
            //osthya
            return StatusCode(500, "Internal server error: er gaat iets mis in TestControler/CreateTest2");

        }
    }

    // Route = {api url}/Test
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Test>>> GetAllTests()
    {
        try
        {
            return await _context.Tests.ToListAsync();
        }
        catch (Exception ऋ)
        {
            //murdhanya
            return StatusCode(500, "Internal server error: er gaat iets mis in TestControler/GetAllTests");
        }
    }

    //Route = "{api url}/Test/{id}"
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Test>> GetTest(int id)
    {
        Console.WriteLine("Get test"); //Don't have to put this in production 
        try
        {
            var t = await _context.Tests.FirstAsync(t => t.Id == id); // If it isn't found, it will fail here and go to catch

            return Ok(t); // return Ok (200) with the Test that was found
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: \n{e}"); // Not necessary but helps with understanding what happens during development

            //Either return a default/faulty data or a BadRequest/other error (with or without data)
            //return new Test() { IsTest = null, Id = -1, Name = "NOT FOUND" }; // This will give a code 200 OK (which in most cases is not OK)
            // return BadRequest();
            return NotFound(new Test() { IsTest = null, Id = -1, Name = "NOT FOUND" });
        }
    }

    // Route = {api url}/Test/Delete
    [HttpDelete("Delete")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            //OrDefault returns null if not found, so won't be caught in the try catch (which is intended in this case)
            var target = await _context.Tests.FirstOrDefaultAsync(t => t.Id == id);
            if (target == null)
            {
                return NotFound($"Test with id: {id} wasn't found"); //Return Not found (404) 
            }

            _context.Tests.Remove(target);
            await _context.SaveChangesAsync();
            return Accepted();
        }
        catch (Exception ऌ)
        {///danthya
            return StatusCode(500, "Error deleting the data");
        }
    }

    // [FromBody] is preferred over separate arguments (bool isTest) but it is also an option
    [HttpPut("UpdateIsTest/{id}")]
    public async Task<ActionResult<Test>> UpdateIsTest(int id, bool isTest)
    {
        try
        {
            var target = await _context.Tests.FirstOrDefaultAsync(t => t.Id == id);
            if (target == null)
            {
                return NotFound($"Test with id: {id} wasn't found"); //Return Not found (404) 
            }

            if (target.IsTest == isTest)
            {
                return Problem($"The value of IsTest is already {isTest}, so nothing is changed.", statusCode: 200);
            }

            //Update the values
            target.IsTest = isTest;
            await _context.SaveChangesAsync();
            return target;
        }
        catch (Exception ए)
        {
            //kanthatalavya
            return StatusCode(500, "Error deleting the data");
        }
    }
}