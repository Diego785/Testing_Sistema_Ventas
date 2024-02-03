using Microsoft.AspNetCore.Mvc;

namespace TestingDBVenta.Controllers
{
    public class NegocioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
