using Microsoft.AspNetCore.Mvc;
using Rezervace_Ples.Models;
using Rezervace_Ples.Models.Services;
using System.Diagnostics;

namespace Rezervace_Ples.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserService userRepo;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            userRepo = new UserService();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View(new User());
        }

        [HttpPost]
        public IActionResult Login(User prihlasovac)
        {
            if (ModelState.IsValid)
            {
                prihlasovac = userRepo.Verify(prihlasovac);
                if (prihlasovac != null)
                {
                    HttpContext.Session.SetInt32("Admin", Convert.ToInt32(prihlasovac.isAdmin));
                    return RedirectToAction("Prizemi", "Rezervace");
                }
            }
            ViewBag.ErrorMessage = "Špatné uživatelské jméno nebo heslo!";
            return View("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
