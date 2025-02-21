using Microsoft.AspNetCore.Mvc;
using GymManagementApp.Models;

namespace GymManagementApp.Controllers
{
    public class RegistrationController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string email, bool consent, int age)
        {
            if (age < 18)
                ViewBag.Message = "Musisz mieć co najmniej 18 lat, aby się zarejestrować!";
            else if (!consent)
                ViewBag.Message = "Musisz wyrazić zgodę na regulamin!";
            else
                ViewBag.Message = $"Użytkownik {email} został pomyślnie zarejestrowany!";

            return View("Index");
        }
    }
}
