using Microsoft.AspNetCore.Mvc;

namespace GymManagementApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index([FromQuery] string name = "Goœæ", [FromQuery] int age = 0, [FromQuery] bool isMember = false)
        {
            // Przekazanie danych u¿ytkownika do widoku
            ViewData["User"] = name;
            ViewData["LastVisit"] = Response.HttpContext.Items[GymManagementApp.Middleware.LastVisitMiddleware.CookieName];

            // Przekierowanie do ró¿nych widoków na podstawie wieku i statusu cz³onkostwa
            if (age >= 18 && isMember)
                return View("Member");
            else if (age < 18)
                return View("Underage");
            else
                return View("Guest");
        }
    }
}
