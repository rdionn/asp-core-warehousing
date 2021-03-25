using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Warehouse.Models;

namespace Warehouse.Controllers {
    public class LoginController : Controller {
        private readonly SignInManager<User> _signInManager;

        public LoginController(SignInManager<User> signInManager) {
            _signInManager = signInManager;
        }


        [Route("login")]
        [HttpGet]
        public IActionResult Index(String ReturnUrl = null) {
            return View("Login");    
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(
            [FromForm(Name = "Username")] String? username,
            [FromForm(Name = "Password")] String? password,
            String ReturnUrl = null
        ) {
            if (username != null && password != null) {
                var signInResult = await _signInManager.PasswordSignInAsync(username, password, true, false);

                if (signInResult.Succeeded) {
                    return LocalRedirect(ReturnUrl);
                }
            }

            return View("Login");
        }
    }
}