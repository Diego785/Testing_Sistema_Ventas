using Microsoft.AspNetCore.Mvc;

namespace TestingDBVenta.Controllers
{
    public class ReporteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
