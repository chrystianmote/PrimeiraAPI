using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Application.Services;

namespace PrimeiraAPI.Controllers
{
    public class AuthController : Controller
    {
        [HttpPost]
        [Route("api/v1/auth")]
        public IActionResult Auth(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                if (username == "Chrystian" && password == "123456")
                {
                    var token = TokenService.GenerateToken(new Domain.Model.Employee("",0,""));
                    return Ok(token);
                }
            }
            return BadRequest("Usuário ou Senha Inválidos");
        }
    }
}
