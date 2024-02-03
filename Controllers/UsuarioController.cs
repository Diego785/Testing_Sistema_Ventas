using Microsoft.AspNetCore.Mvc;

namespace TestingDBVenta.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
