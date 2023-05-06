using AseregeBarcelonaWeb.Manager;
using Microsoft.AspNetCore.Mvc;
using AseregeBarcelonaWeb.Model.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using AseregeBarcelonaWeb.Manager.Enums;

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
            MySQLManager manager = new MySQLManager();
            role.authorize.Password = CryptographyManager.GeneratePasswordHash(role.authorize.Password);

            User user = manager.GetUser(role.authorize);
            if (user != null && user.Roles_idroles == (int)UserRole.Administrator)
            {
                string roleResult = await manager.UpdateRoleAsync(role.Nombre, role.Type, role.Idroles);
                await manager.DisposeAsync();
                await Task.CompletedTask;
                return Ok(roleResult);
            }
            else 
            {
                await Task.CompletedTask;
                return Unauthorized();
            }
        }     
        /*[HttpDelete("{id}")] public IActionResult Delete(int id)
        {        
            return Ok();
        }*/
    }
}