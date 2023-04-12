using AseregeBlazor.Manager;
using Microsoft.AspNetCore.Mvc;
using Model.Data;

namespace AseregeBlazor.API
{
    //Api para el Login
    [ApiController] //propiedad de inyector de dependencias
    [Route("api/login")]//definir ruta del login
    public class LoginAPI : ControllerBase //la clase hereda del codigo interno de ASP NET
    {
        [HttpGet]
        public IActionResult Get([FromBody] Authorize model) //la función get, obtiene del body un JSON del tipo Autorize
        {

            
            return Ok(new MySQLManager().GetUser(model)); //devuelve una nueva instancia a la base de datos
        }

        [HttpPost]
        public IActionResult Post([FromBody] Authorize model)
        {
            MySQLManager result = new MySQLManager();            
            return Ok(result.Login(model)); //devuelve si es true (iniciar sesion) or false
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Authorize model)
        {
            // TODO: Handle PUT request
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // TODO: Handle DELETE request
            return Ok();
        }
    }   
}