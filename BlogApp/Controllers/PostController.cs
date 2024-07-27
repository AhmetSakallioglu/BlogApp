using System.ComponentModel.DataAnnotations;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;
using BlogApp.Entities;

namespace BlogApp.Controllers
{
	public class PostController : Controller
	{
		private readonly DatabaseContext _databaseContext;
		private readonly IConfiguration _configuration;
		private readonly IWebHostEnvironment _environment;

		public PostController(DatabaseContext databaseContext, IConfiguration configuration, IWebHostEnvironment environment)
		{
			_databaseContext = databaseContext;
			_configuration = configuration;
			_environment = environment;
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

		[HttpPost]
		public async Task<IActionResult> Create(CreatePostViewModel model)
		{
			if (ModelState.IsValid)
			{
				var post = new Post
				{
					Title = model.Title,
					Content = model.Content,
					Date = DateTime.Now,
					IsActive = model.IsActive,
					PostTag = model.TagId.Select(tagId => new PostTag { TagId = tagId }).ToList(),
					PostImage = new List<PostImage>()
				};

				if (model.Images != null && model.Images.Count > 0)
				{
					foreach (var image in model.Images)
					{
						var fileName = Path.GetFileName(image.FileName);
						var filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);

						using (var stream = new FileStream(filePath, FileMode.Create))
						{
							await image.CopyToAsync(stream);
						}

						post.PostImage.Add(new PostImage
						{
							Image = new Image { ImageUrl = "/uploads/" + fileName }
						});
					}
				}

				await _databaseContext.Post.AddAsync(post);
				await _databaseContext.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}

			ViewBag.Tags = _databaseContext.Tag.ToList();
			return View(model);
		}
	}
}
