using AseregeBarcelonaWeb.Manager;
using AseregeBarcelonaWeb.Model.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AseregeBarcelonaWeb.API
{
    [ApiController]
    [Route("api/users")] public class UserAPI : ControllerBase
    {
        [HttpGet] public IActionResult Get()
        {            
            User[] users = new MySQLManager().SelectUsers().ToArray();
            foreach (var user in users) user.Passwordseguro = "";

            string responseString = JsonConvert.SerializeObject(users, Formatting.Indented); //dar formato al json                                             
            return Ok(responseString);
        }

        [HttpPost] public IActionResult Post([FromBody] User model)
        {
            MySQLManager result = new MySQLManager();
            return Ok(result.InsertUser(model));
        }

        [HttpPut("{id}")] public IActionResult Put(int id, [FromBody] User model)
        {
            // TODO: Handle PUT request
            return Ok();
        }

        [HttpDelete("{id}")] public IActionResult Delete(int id)
        {          
            return Ok();
        }
    }

    public class MyModel
    {
    
    }
}