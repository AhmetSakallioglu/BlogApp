using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
	public class PostController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

	}
}
