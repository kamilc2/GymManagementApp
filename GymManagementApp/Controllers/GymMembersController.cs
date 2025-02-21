using Microsoft.AspNetCore.Mvc;
using GymManagementApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace GymManagementApp.Controllers
{
    public class GymMembersController : Controller
    {
        static List<GymMemberViewModel> members = new List<GymMemberViewModel>();

        [HttpGet]
        public IActionResult Index(string searchQuery)
        {
            var filteredMembers = string.IsNullOrEmpty(searchQuery)
                ? members
                : members.Where(m => m.Name.Contains(searchQuery)).ToList();

            return View(filteredMembers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(GymMemberViewModel member)
        {
            if (ModelState.IsValid)
            {
                member.Id = (members.Count > 0) ? members.Max(x => x.Id) + 1 : 1;
                members.Add(member);
                return RedirectToAction("Index");
            }
            else
            {
                return View(member);
            }
        }
    }
}
