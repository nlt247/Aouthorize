using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Aouthorize.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Secret() {
            return View();
        }

        public IActionResult Authenticate() {
            var grandmaclaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Bob"),
                new Claim(ClaimTypes.Email,"Bob@fmail.com"),
                new Claim("Grandma.Says","Very nice boi.")
            };
            var licenseClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Bob K Foo"),
                new Claim("DrivingLicense","A+")
            };
            var grandmaIdentity = new ClaimsIdentity(grandmaclaims, "Grandma Identity");
            var licenseIdentity = new ClaimsIdentity(licenseClaims, "Government");
            var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity,licenseIdentity });
            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Index");
        }
    }
}
