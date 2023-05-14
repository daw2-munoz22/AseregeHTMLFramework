using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AseregeBarcelonaWeb.Model.Data;
using AseregeBarcelonaWeb.Manager;
using System.Net.Mail;
using System.Net;
using System.Text;
using System;

namespace AseregeBarcelonaWeb.API
{
    //Api para el Login
    [ApiController] //propiedad de inyector de dependencias
    //definir ruta del login 
	[Route("api/passwordrestore")] public class PasswordRestoreAPI : ControllerBase //la clase hereda del codigo interno de ASP NET
    {       
        [HttpPost] public async Task<IActionResult> Post([FromBody] MailRestore model)
        {
            MySQLManager result = new MySQLManager();
            List<User> users = result.SelectUsers();
            User usuarioFiltrado = users.FirstOrDefault(u => u.Email == model.Email && u.Nombre == model.Nombre);

            if (usuarioFiltrado != null)
            {
                string Result = await SendMail("edgarmunozmanjon@outlook.com", "P@ssw0rd543", usuarioFiltrado.Email, "Recuperacion de contraseñas", usuarioFiltrado);
                return Ok(Result);
            }

            return NotFound();

        }

        //api para el reenvio del correo
        private async Task<string> SendMail(string from, string password, string to, string subject, User usuarioFiltrado)
        {
            string server = string.Empty;
            if (from.Contains("live") || from.Contains("hotmail"))
            {
                server = "smtp-mail.outlook.com";
            }
            else if (from.Contains("outlook"))                
            {
                server = "smtp.office365.com";
            }
            else if (from.Contains("gmail"))
            {
                server = "smtp.gmail.com";
            }            

            using SmtpClient smtpClient = new SmtpClient(server, 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(from, password);

            MailMessage mailMessage = new MailMessage(from, to);

            mailMessage.IsBodyHtml = true;
            mailMessage.SubjectEncoding = Encoding.UTF8;
            mailMessage.Subject = subject;
            mailMessage.Body = $"<p>{usuarioFiltrado.Passwordseguro}</p>"; //recibimos el hash para luego, verificar

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                mailMessage.Dispose();
            }
            catch (Exception ex)
            {
                mailMessage.Dispose();
                return ex.Message;                
            }
            await Task.CompletedTask;
            return "OK";
        }
    }
}
