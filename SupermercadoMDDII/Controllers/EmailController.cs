using Microsoft.AspNetCore.Mvc;
using SupermercadoMDDII.Implementacion;
using SupermercadoMDDII.Services;

namespace SupermercadoMDDII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult SendEmail(EmailDTO request) {
            _emailService.SendEmail(request);
            return Ok();

        }
    }
}
