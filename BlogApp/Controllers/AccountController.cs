using BlogApp.Entities;
using BlogApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NETCore.Encrypt.Extensions;
using System.Security.Claims;

namespace BlogApp.Controllers
{
	public class AccountController : Controller
	{
		private readonly DatabaseContext _databaseContext;
		private readonly IConfiguration _configuration;
        public AccountController(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
            _configuration = configuration;
        }

        public IActionResult Index()
		{
			return View();
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model, CancellationToken cancellationToken)
		{
			if(ModelState.IsValid)
			{
				string hashedPassword = DoMD5HashedString(model.Password);

				User user = await _databaseContext.Users.SingleOrDefaultAsync(x => x.Username.ToLower() == model.Username.ToLower() && x.Password == hashedPassword, cancellationToken);

				if(user != null)
				{
					if(user.Locked)
					{
						ModelState.AddModelError(nameof(model.Username), "Yasaklı Kullanıcı!");
						return View(model);
					}

					List<Claim> claims = new List<Claim>();
					claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, user.FullName ?? string.Empty));
                    claims.Add(new Claim(ClaimTypes.Role, user.Role));
					claims.Add(new Claim("Username", user.Username));

					ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

					ClaimsPrincipal principal = new ClaimsPrincipal(identity);

					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

					return RedirectToAction(nameof(Index), "Home");
				}
				else
				{
					ModelState.AddModelError("", "Username is password is incorrect.");
				}
			}
			return View(model);
		}


		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                if (await _databaseContext.Users.AnyAsync(x => x.Username.ToLower() == model.Username.ToLower(), cancellationToken))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username is already exists.");
                    return View(model);
                }

                string hashedPassword = DoMD5HashedString(model.Password);

                User user = new()
                {
                    Username = model.Username,
                    Password = hashedPassword
                };

                _databaseContext.Users.Add(user);
                int affectedRowCount = await _databaseContext.SaveChangesAsync();

                if (affectedRowCount == 0)
                {
                    ModelState.AddModelError("", "User can not be added.");
                }
                else
                {
                    return RedirectToAction(nameof(Login));
                }
            }
            return View(model);
        }

        private string DoMD5HashedString(string s)
        {
            string md5Salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
            string salted = s + md5Salt;
            string hashed = salted.MD5();
            return hashed;
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
    }
}
