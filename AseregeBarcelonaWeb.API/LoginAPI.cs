﻿using AseregeBarcelonaWeb.Manager;
using Microsoft.AspNetCore.Mvc;
using AseregeBarcelonaWeb.Model.Data;

namespace AseregeBarcelonaWeb.API
{
    //Api para el Login
    [ApiController] //propiedad de inyector de dependencias
    //definir ruta del login
	[Route("api/login")] public class LoginAPI : ControllerBase //la clase hereda del codigo interno de ASP NET
    {
        [HttpGet] public IActionResult Get([FromBody] Authorize model) //la función get, obtiene del body un JSON del tipo Autorize
        {
            return Ok(new MySQLManager().GetUser(model)); //devuelve una nueva instancia a la base de datos
        }

        [HttpPost] public IActionResult Post([FromBody] Authorize model)
        {
            MySQLManager result = new MySQLManager();
            User user = result.GetUser(model);
  
            bool validated = result.Login(model);
                         
            if (validated)
            {
                result.Dispose();
                return Ok("Inicio de sesión exitoso");
            }
            else
            {
                result.Dispose();
                return Ok("Nombre de usuario o contraseña incorrectos");
            }                                                                                                  
        }    
    }
}