using AseregeBarcelonaWeb.Manager;
using AseregeBarcelonaWeb.Manager.Enums;
using AseregeBarcelonaWeb.Model.Data;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Cms;
using System.Threading.Tasks;

namespace AseregeBarcelonaWeb.API
{
    [ApiController]
    [Route("api/picture/{key:int}")] public class PictureAPI : ControllerBase
    {
        
        //peticion GET asincronica en /api/picture que devuelve una URI con la imagen que has seleccionado
        //mediante su identificador (id)
        //Ejemplo de peticion GET -> {"Id": 2}
        [HttpGet] public async Task<IActionResult> Get(int key)
        {
            MySQLManager result = new MySQLManager();
            Picture uri = await result.SelectPicturesByIDAsync(key);
            await result.DisposeAsync();
            await Task.CompletedTask;
            return Ok(uri);
        }

        //peticion POST asincronica que inserta un fichero (imagen/video) a la base de datos. 
        //MUY IMPORTANTE: tienes que ser administrador o usuario autenticado.
        //En caso contrario, peticion incorrecta


        //{
        //  "Authorize": {"Name": "PACOCINERO", "Password": "COCINA1234#" },
        //  "Id": 2, "Name": "campnou", "Format": "jpeg", "Date": "0001-01-01T00:00:00", "Data": ""
        //}
   
        [HttpPost] public async Task<IActionResult> Post([FromBody] Picture model)
        {
            model.Name = CryptographyManager.GenerateHash(model.Date.ToString(),model.Name);

            string uri = string.Empty;
            MySQLManager result = new MySQLManager();   
            if (model.Authorize != null)
            {
                model.Authorize.Password = CryptographyManager.GeneratePasswordHash(model.Authorize.Password);
                User user = result.GetUser(model.Authorize);
                if (user != null)
                {
                    uri = await result.InsertFileAsync(model);
                    await result.DisposeAsync();
                    await Task.CompletedTask;
                    return Ok(uri);
                }
                else
                {
                    await result.DisposeAsync();
                    await Task.CompletedTask;
                    return Unauthorized("Access Denied!");
                }
            }
            else
            {
                await Task.CompletedTask;
                return BadRequest(model);
            }
                                           
        }    

        [HttpDelete] public async Task<IActionResult> Delete(int key, Authorize model)
        {

            model.Password = CryptographyManager.GeneratePasswordHash(model.Password);
            MySQLManager manager = new MySQLManager();
            User user = manager.GetUser(model);
            if (user != null)
            {
                if (user.Roles_idroles == (int)UserRole.Administrator || user.Roles_idroles == (int)UserRole.User)
                {
                    Picture picture = await manager.SelectPicturesByIDAsync(key);
                    
                    await manager.DeleteImageAsync(key, picture.Name);
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