using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Controllers
{
    public class LesseesController : Controller
    {
        private readonly DataContext _context;

        public LesseesController(DataContext context)
        {
            _context = context;
        }

        // GET: Lessees
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Lessee.Include(l => l.user);
            return View(await dataContext.ToListAsync());
        }

        // GET: Lessees/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessee = await _context.Lessee
                .Include(l => l.user)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lessee == null)
            {
                return NotFound();
            }

            return View(lessee);
        }

        // GET: Lessees/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Lessees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Document,FirstName,LastName,FixedPhone,CellPhone,Address,ImageUrl,UserId")] Lessee lessee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lessee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", lessee.UserId);
            return View(lessee);
        }

        // GET: Lessees/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessee = await _context.Lessee.FindAsync(id);
            if (lessee == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", lessee.UserId);
            return View(lessee);
        }

        // POST: Lessees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Document,FirstName,LastName,FixedPhone,CellPhone,Address,ImageUrl,UserId")] Lessee lessee)
        {
            if (id != lessee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lessee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LesseeExists(lessee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", lessee.UserId);
            return View(lessee);
        }

        // GET: Lessees/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessee = await _context.Lessee
                .Include(l => l.user)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lessee == null)
            {
                return NotFound();
            }

            return View(lessee);
        }

        // POST: Lessees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var lessee = await _context.Lessee.FindAsync(id);
            _context.Lessee.Remove(lessee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LesseeExists(string id)
        {
            return _context.Lessee.Any(e => e.Id == id);
        }
    }
}
