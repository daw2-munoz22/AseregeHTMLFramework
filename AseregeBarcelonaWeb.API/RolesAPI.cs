using AseregeBarcelonaWeb.Manager;
using Microsoft.AspNetCore.Mvc;
using AseregeBarcelonaWeb.Model.Data;

namespace AseregeBarcelonaWeb.API
{
    [ApiController]
    [Route("api/roles")] public class RolesAPI : ControllerBase
    {
        [HttpGet] public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost] public IActionResult Post([FromBody] Role model)
        {
            MySQLManager result = new MySQLManager();
            return Ok(result.InsertRole(model));
        }

      /*  [HttpPut("{id}")] public IActionResult Put(int id, [FromBody] Role model)
        {            
            return Ok();
        }

        [HttpDelete("{id}")] public IActionResult Delete(int id)
        {        
            return Ok();
        }*/
    }
}