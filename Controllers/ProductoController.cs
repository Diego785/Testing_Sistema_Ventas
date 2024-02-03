using Microsoft.AspNetCore.Mvc;

namespace TestingDBVenta.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
