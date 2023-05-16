using AseregeBarcelonaWeb.Manager;
using Microsoft.AspNetCore.Mvc;
using AseregeBarcelonaWeb.Model.Data;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AseregeBarcelonaWeb.API
{
    //Api para el Login
    [ApiController] //propiedad de inyector de dependencias
    //definir ruta del login
	[Route("api/login")] public class LoginAPI : ControllerBase //la clase hereda del codigo interno de ASP NET
    {
        //COMPLETADO
        //esta API permite iniciar la sesión del usuario
        [HttpPost] public async Task<IActionResult> Post(Authorize model) 
        {
            model.Password = CryptographyManager.GeneratePasswordHash(model.Password);
            User user = new MySQLManager().GetUser(model);           
            await Task.CompletedTask;
            return Ok(user); //devuelve una nueva instancia a la base de datos
        }

        [HttpPut] public async Task<IActionResult> Put([FromBody] Authorize model)
        {
            MySQLManager result = new MySQLManager();
            model.Password = CryptographyManager.GeneratePasswordHash(model.Password);
            User user = result.GetUser(model);
  
            bool validated = result.Login(model);
            result.Dispose();
            await Task.CompletedTask;
             
            return validated ? Ok("Inicio de sesión exitoso") : Unauthorized("Nombre de usuario o contraseña incorrectos");                        
        }    
    }
}