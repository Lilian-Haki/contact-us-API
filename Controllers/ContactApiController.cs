using contact_us_page.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace contact_us_page.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactApiController : ControllerBase
    {
       // POST api/<ContactApiController>
        [HttpPost]
        public IActionResult Post([FromBody] ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                var mail = new MailMessage();
                mail.To.Add("irungulilianhaki@gmail.com");
                mail.From = new MailAddress(model.Email);
                mail.ReplyToList.Add(new MailAddress(model.Email));
                mail.Subject = new (model.Subject);
                mail.Body = $"Name: {model.Name}\n\nSubject:{model.Subject}\n\nMessage:\n{model.Message}";

                using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("irungulilianhaki@gmail.com", "kpvn qzaz pqex wljg\r\n");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }

                return Ok(new { message = "Message sent successfully!" });
            }

            return BadRequest(new { error = "Invalid input." });
        }
    }       
}
