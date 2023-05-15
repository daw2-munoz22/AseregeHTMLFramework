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
        /*COMPLETA*/
        //obtenemos un JSON con TODOS los usuarios registrados
        
        [HttpGet] public IActionResult Get()
        {
            User[] users = new MySQLManager().SelectUsers().ToArray();
            foreach (var user in users) user.Passwordseguro = "";

            string responseString = JsonConvert.SerializeObject(users, Formatting.Indented); //dar formato al json                                             
            return Ok(responseString);
        }
        
        /*COMPLETA*/
        //esta funcion permite distingir los usuarios que tengan el mismo nombre y password. Se diferencian mediante su email
        //MUY IMPORTANTE: si en la petición del rol es 1, dará acceso denegado!!!
        [HttpPost] public async Task<IActionResult> Post([FromBody] JavascriptUser model)
        {
            if (model.Roles_idroles == (int)UserRole.Administrator) return Unauthorized();
            
            MySQLManager result = new MySQLManager();
            
            bool Exists = await result.ExistUser(model.Nombre, model.Passwordseguro);
            if (!Exists) 
            {
                string user = await result.InsertUserAsync(model);
                await result.DisposeAsync();
                return Ok(user);
            }           
            await result.DisposeAsync();  
             
            return BadRequest();                        
        }

        /*COMPLETA*/
        //esta función permite actualizar el los datos del usuario registrado
        [HttpPut] public async Task<IActionResult> Put([FromBody] JavascriptUser model)
        {
            using (MySQLManager manager = new MySQLManager())
            {
                model.authorize.Password = CryptographyManager.GeneratePasswordHash(model.Passwordseguro);
                

                /*INICIAR SESION*/
                User user = manager.GetUser(model.authorize);

                if (model.Roles_idroles == (int)UserRole.Administrator) 
                {
                    return Unauthorized();
                }
                if (user != null && user.Roles_idroles == (int)UserRole.Administrator || user != null)
                {
                    await manager.UpdateUserAsync(model.Nombre, model.Apellido, model.Edad, model.Sexo.ToCharArray()[0], model.Email,
                        model.Telefono, model.Passwordseguro, model.Roles_idroles, user.ID);
                    return Ok(user);
                }
            }
            return NoContent();
        }
        
        /*COMPLETADO*/
        //esta funcion permite borrar al usuario de manera ASINCRONICA
        [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id, [FromBody] Authorize admin)
        {            
            MySQLManager manager = new MySQLManager();
            admin.Password = CryptographyManager.GeneratePasswordHash(admin.Password);
            
            User user = manager.GetUser(admin);
            if (user != null && user.Roles_idroles == (int)UserRole.Administrator || user != null && user.ID == id)
            {
                await manager.DeleteUserAsync(id);
                await manager.DisposeAsync();
                return Ok("OK");
            }
            else 
            {                
                await manager.DisposeAsync();
                return Unauthorized();
            }            
        }    
    }   
}