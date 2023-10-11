using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Application.Services;
using PrimeiraAPI.Domain.Model.EmployeeAggregate;

namespace PrimeiraAPI.Controllers
{


    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller
    {
        [HttpPost]
        public IActionResult Auth(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                if (username == "Chrystian" && password == "123456")
                {
                    var token = TokenService.GenerateToken(new Employee("",0,""));
                    return Ok(token);
                }
            }
            return BadRequest("Usuário ou Senha Inválidos");
        }
    }
}
