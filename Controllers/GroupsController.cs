using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFamilyHome.Data;
using WebFamilyHome.Models;

namespace WebFamilyHome.Controllers
{
    public class GroupsController : Controller
    {
        private readonly AppDbContext _context;

        public GroupsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            var groups = await _context.Groups.Include(g => g.Users).ToListAsync();
            return View(groups);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Group group)
        {
            if (ModelState.IsValid)
            {
                _context.Add(group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(group);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null) return NotFound();
            return View(group);
        }

        // POST: Groups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Group group)
        {
            if (id != group.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(group);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var group = await _context.Groups.Include(g => g.Users)
                                             .FirstOrDefaultAsync(g => g.Id == id);
            if (group == null) return NotFound();
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group != null)
            {
                _context.Groups.Remove(group);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
