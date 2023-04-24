using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AseregeBarcelonaWeb.Model.Data;
using AseregeBarcelonaWeb.Manager;

namespace AseregeBarcelonaWeb.API
{
    //Api para el Login
    [ApiController] //propiedad de inyector de dependencias
    //definir ruta del login 
	[Route("api/passwordrestore")] public class PasswordRestoreAPI : ControllerBase //la clase hereda del codigo interno de ASP NET
    {
        [HttpGet] public IActionResult Get([FromBody] Authorize model) //la función get, obtiene del body un JSON del tipo Autorize       
        {
            return Ok();
        }

        [HttpPost] public IActionResult Post([FromBody] MailRestore model)
        {
            MySQLManager result = new MySQLManager();
            List<User> users = result.SelectUsers();
            User usuarioFiltrado = users.FirstOrDefault(u => u.Email == model.Email && u.Telefono == model.Telefono);
            if (usuarioFiltrado != null)
            {
                return Ok(SendMail(usuarioFiltrado.Email));
            }

            return NotFound();

        }

        [HttpPut("{id}")] public IActionResult Put(int id, [FromBody] Authorize model)
        {
            // TODO: Handle PUT request
            return Ok();
        }

        [HttpDelete("{id}")] public IActionResult Delete(int id)
        {            
            return Ok();
        }

        //api para el reenvio del correo
        private async Task SendMail(string emailAddress)
        {


            /*using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("edgarmunozmanjon@gmail.com", "060242343");

                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress("edgarmunozmanjon@gmail.com");
                    mailMessage.To.Add(emailAddress);
                    mailMessage.SubjectEncoding = Encoding.UTF8;
                    mailMessage.Subject = "BarcelonaWeb Password Recovery";
                    mailMessage.Body = "<h1>Password Recovery Email</h1><br />";
                    mailMessage.IsBodyHtml = true;

                    await smtpClient.SendMailAsync(mailMessage);
                }
            }*/
        }

    }
}
/*public string SendMail(string to)
{
    string fromAddress = "edgarmunozmanjon@gmail.com";
    string toAddress = to;
    string subject = "Test Email";
    string body = "This is a test email sent from C#.";

    var smtpClient = new SmtpClient("smtp.gmail.com")
    {
        Port = 587,
        Credentials = new NetworkCredential(fromAddress, "060242343"),                
        EnableSsl = true
    };

    MailMessage message = new MailMessage(fromAddress, toAddress)
    {
        Subject = subject,
        Body = body
    };                
    smtpClient.Send(message);
    message.Dispose();



    return "Mensaje enviado";
}*/
