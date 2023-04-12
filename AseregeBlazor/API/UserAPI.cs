using AseregeBlazor.Manager;
using Microsoft.AspNetCore.Mvc;
using Model.Data;
using Newtonsoft.Json;

namespace AseregeBlazor.API
{
    [ApiController]
    [Route("api/users")]
    public class UserAPI : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {                            
            User[] users = new MySQLManager().SelectUsers().ToArray();                
            foreach (var user in users) user.Passwordseguro = "";
                         
            string responseString = JsonConvert.SerializeObject(users, Formatting.Indented); //dar formato al json                                             
            return Ok(responseString);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User model)
        {
            MySQLManager result = new MySQLManager();            
            return Ok(result.InsertUser(model));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User model)
        {
            // TODO: Handle PUT request
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // TODO: Handle DELETE request
            return Ok();
        }
    }

    public class MyModel
    {
        // TODO: Define model properties
    }
}