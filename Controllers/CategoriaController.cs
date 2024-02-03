using Microsoft.AspNetCore.Mvc;

namespace TestingDBVenta.Controllers
{
    public class CategoriaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
