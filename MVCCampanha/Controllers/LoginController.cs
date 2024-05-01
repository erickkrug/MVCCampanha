using Microsoft.AspNetCore.Mvc;

namespace MVCCampanha.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Login(string usuario, string senha)
        {
            if(usuario == "adm" && senha == "Master1234")
            {
                return Ok();
            }
            else
            {
                return Forbid();
            }
        }
    }
}
