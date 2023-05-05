using AseregeBarcelonaWeb.Manager;
using AseregeBarcelonaWeb.Manager.Enums;
using AseregeBarcelonaWeb.Model.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

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

        [HttpPost] public async Task<IActionResult> Post([FromBody] User model)
        {
            MySQLManager result = new MySQLManager();
            string user = await result.InsertUserAsync(model);
            await result.DisposeAsync();
            return Ok(user);
        }

        [HttpPut] public async Task<IActionResult> Put([FromBody] User model)
        {
            using (Manager.MySQLManager manager = new Manager.MySQLManager())
            {
                await manager.UpdateUserAsync(model.Nombre, model.Apellido, model.Edad, model.Sexo, model.Email,
                model.Telefono, model.Passwordseguro, model.Roles_idroles, model.ID);
            }
            return NoContent();
        }

    /*    [HttpDelete("{id}")] public async Task<IActionResult> Delete([FromBody] User model, Authorize role)
        {
            Manager.MySQLManager manager = new Manager.MySQLManager();
            
            User user = manager.GetUser(role);

            if (user.Roles_idroles == (int)UserRole.Administrator)
            {
                await manager.DeleteUserAsync(user.ID);
                await manager.DisposeAsync();
                return Ok("OK");
            }
            else 
            {                
                await manager.DisposeAsync();
                return Unauthorized();
            }            
        }
    */
    }

    public class MyModel
    {
    
    }
}