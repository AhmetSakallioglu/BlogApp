using BlogApp.Entities;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BlogApp.Controllers
{
    public class TagController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public TagController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IActionResult Index()
        {
            var tags = _databaseContext.Tag
                .Select(t => new TagViewModel { Id = t.Id, Name = t.Name })
                .ToList();

            return View(tags);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TagViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                if (await _databaseContext.Tag.AnyAsync(x => x.Name.ToLower() == model.Name.ToLower(), cancellationToken))
                {
                    ModelState.AddModelError("", "Bu isimde bir etiket zaten mevcut.");
                    return View(model);
                }
            }

            Tag tag = new Tag()
            {
                Name = model.Name,
            };

            await _databaseContext.Tag.AddAsync(tag);
            int affectedRowCount = await _databaseContext.SaveChangesAsync(cancellationToken);

            if (affectedRowCount == 0)
            {
                ModelState.AddModelError("", "Tag Eklenemedi.");
                return View(model);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            Tag tag = await _databaseContext.Tag.FindAsync(id);

            if (tag != null)
            {
                _databaseContext.Tag.Remove(tag);
                await _databaseContext.SaveChangesAsync(cancellationToken);
            }
            return RedirectToAction(nameof(Index));
        }


    }
}
