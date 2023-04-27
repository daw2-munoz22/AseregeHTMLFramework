using AseregeBarcelonaWeb.Manager;
using AseregeBarcelonaWeb.Model.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AseregeBarcelonaWeb.API
{
    [ApiController]   
    [Route("api/images")] public class ImageAPI : ControllerBase
    {

        [HttpGet] public async Task<IActionResult> Get()
        {
            MySQLManager result = new MySQLManager();
            long count = await result.SelectCountImages();            
            await result.DisposeAsync();
            return Ok(count);
        }      
    }
}