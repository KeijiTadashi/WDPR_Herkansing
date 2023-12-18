using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/Logincontroller")]
public class ValuesController : ControllerBase
{
    // GET api/values
    [HttpGet]
    public ActionResult<string> Get()
    {
        return "Hello, this is your API!";
    }
}
