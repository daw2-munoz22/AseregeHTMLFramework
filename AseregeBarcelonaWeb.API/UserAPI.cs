using AseregeBarcelonaWeb.Manager;
using AseregeBarcelonaWeb.Manager.Enums;
using AseregeBarcelonaWeb.Model.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
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
    public class JavascriptUser
    {
        public Authorize authorize { get; set; }
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Passwordseguro { get; set; }
        public int Roles_idroles { get; set; }        
    }
}