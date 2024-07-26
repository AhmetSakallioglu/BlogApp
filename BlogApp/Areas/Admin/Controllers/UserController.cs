using AutoMapper;
using BlogApp.Areas.Admin.Models;
using BlogApp.Entities;
using BlogApp.Entities;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers
{
    [Authorize(Roles = "admin")]
	[Area("Admin")]
	public class UserController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;

        public UserController(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            List<UserModel> users = _databaseContext.User.ToList().Select(x => _mapper.Map<UserModel>(x)).ToList();

            //_databaseContext.Users.Select(x => new UserModel {Id = x.Id, FullName = x.FullName}).ToList();

            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                if (await _databaseContext.User.AnyAsync(x => x.Username.ToLower() == model.Username.ToLower(), cancellationToken))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username is already exists.");
                    return View(model);
                }

                User user = _mapper.Map<User>(model);
                _databaseContext.User.Add(user);
                await _databaseContext.SaveChangesAsync(cancellationToken);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult Edit(Guid id)
        {
            User user = _databaseContext.User.Find(id);
            EditUserModel model = _mapper.Map<EditUserModel>(user);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, EditUserModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                if (await _databaseContext.User.AnyAsync(x => x.Username.ToLower() == model.Username.ToLower() && x.Id != id))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username is already exists.");
                    return View(model);
                }

                User user = await _databaseContext.User.FindAsync(id);

                _mapper.Map(model, user);
                await _databaseContext.SaveChangesAsync(cancellationToken);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationtoken)
        {
            User user = await _databaseContext.User.FindAsync(id);

            if (user != null)
            {
                _databaseContext.User.Remove(user);
                await _databaseContext.SaveChangesAsync(cancellationtoken);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
