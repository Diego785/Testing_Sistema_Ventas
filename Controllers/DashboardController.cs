using Microsoft.AspNetCore.Mvc;

namespace TestingDBVenta.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
