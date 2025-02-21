using GymManagementApp.Models;
using GymManagementApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GymManagementApp.Controllers
{
    public class GymUsersController : Controller
    {
        private readonly IGymUserService _gymUserService;

        public GymUsersController(IGymUserService gymUserService)
        {
            _gymUserService = gymUserService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _gymUserService.FindAll(); // ✅ Używamy serwisu, a nie _context
            return View(users);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _gymUserService.FindById(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GymUser user)
        {
            if (ModelState.IsValid)
            {
                await _gymUserService.Add(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _gymUserService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
