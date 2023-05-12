using AseregeBarcelonaWeb.Manager;
using AseregeBarcelonaWeb.Manager.Enums;
using AseregeBarcelonaWeb.Model.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AseregeBarcelonaWeb.API
{
    [ApiController]
    [Route("api/images")]
    public class ImageAPI : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            MySQLManager result = new MySQLManager();
            long count = await result.SelectCountImages();
            await result.DisposeAsync();
            return Ok(count);
        }

        [HttpPut] public async Task<IActionResult> Put([FromBody] Picture model)
        {

            model.Authorize.Password = CryptographyManager.GeneratePasswordHash(model.Authorize.Password);
            MySQLManager manager = new MySQLManager();
            User user = manager.GetUser(model.Authorize);
            if (user != null)
            {
                if (user.Roles_idroles == (int)UserRole.Administrator || user.Roles_idroles == (int)UserRole.User)
                {
                    await manager.InsertFile(model);
                }
                await manager.DisposeAsync();
                await Task.CompletedTask;
                return Ok();
            }
            else
            {
                await manager.DisposeAsync();
                await Task.CompletedTask;
                return Unauthorized();
            }
        }
    }
}