using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebFamilyHome.Data;
using WebFamilyHome.Models;

namespace WebFamilyHome.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.Include(u => u.Group).ToListAsync();
            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewBag.Groups = new SelectList(_context.Groups, "Id", "Name");
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // repopula dropdown se houver erro
            ViewBag.Groups = new SelectList(_context.Groups, "Id", "Name", user.GroupId);
            return View(user);
        }
    }
}
