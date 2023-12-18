using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

[ApiController]
[Route("api/Onderzoekcontroller")]
public class ValuesController : ControllerBase
{
    [HttpPost]
    public ActionResult<string> Post([FromBody] JObject data)
    {
        if (data == null)
        {
            return BadRequest("Invalid JSON data");
        }

        // Process the JSON data as needed
        // For example, you can access properties like this:
        // var value1 = data["property1"].ToString();
        // var value2 = data["property2"].ToString();

        return $"Received JSON data: {data}";
    }

    [HttpGet]
    public ActionResult<string> Get()
    {
        //Vul hier de get methode in
        return "no"
    }
}
