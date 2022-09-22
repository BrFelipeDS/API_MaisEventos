using APIMaisEventos.Interfaces;
using APIMaisEventos.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIMaisEventos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ILoginRepository repo;

        public LoginController(ILoginRepository _repo)
        {
            repo = _repo;
        }


        [HttpPost]
        public IActionResult Logar(string email, string senha)
        {
            var logar = repo.Logar(email, senha);
            if (logar == null)
                return Unauthorized();

            return Ok(new { token = logar });
        }
    }
}
