using BlogApp.Entities;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Controllers
{
	public class PostController : Controller
	{
		private readonly DatabaseContext _databaseContext;
		private readonly IConfiguration _configuration;
		public PostController(DatabaseContext databaseContext, IConfiguration configuration)
		{
			_databaseContext = databaseContext;
			_configuration = configuration;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Create()
		{
			ViewBag.Tags = _databaseContext.Tag.ToList();
			return View();
		}

	}
}


