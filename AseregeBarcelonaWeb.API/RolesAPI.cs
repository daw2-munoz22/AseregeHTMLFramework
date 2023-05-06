using AseregeBarcelonaWeb.Manager;
using Microsoft.AspNetCore.Mvc;
using AseregeBarcelonaWeb.Model.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AseregeBarcelonaWeb.API
{
    [ApiController]
    [Route("api/roles")] public class RolesAPI : ControllerBase
    {
        [HttpGet] public async Task<IActionResult> Get()
        {
            MySQLManager manager = new MySQLManager();
            List<Role> roleList = await manager.SelectRolesAsync();

            if (roleList == null || roleList.Count < 1) 
            {
                await Task.CompletedTask; 
                return NoContent();                
            }

            return Ok(roleList);
        }

        [HttpPost] public async Task<IActionResult> Post([FromBody] Role model)
        {
            MySQLManager manager = new MySQLManager();
            string result = await manager.InsertRoleAsync(model);
            await manager.DisposeAsync();
            await Task.CompletedTask;
            return Ok();
        }

        [HttpPut] public async Task<IActionResult> Put([FromBody] Role role)
        {            
            using (MySQLManager manager = new MySQLManager())
            {

                //await manager.UpdateRoleAsync(model.Nombre, model.Apellido);
            }
            await Task.CompletedTask;
            return NoContent();
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