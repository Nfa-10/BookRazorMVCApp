using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoBookApp.Data;
using DemoBookApp.Models;

namespace DemoBookApp.Controllers
{
    public class AuthorController : Controller
    {
        private readonly BookDbContext _context;

        public AuthorController(BookDbContext context)
        {
            _context = context;
        }
        // GET: authorController
        public async Task<IActionResult> Index()
        {
            return _context.Author != null ?
                        View(await _context.Author.ToListAsync()) :
                        Problem("Entity set 'BookDbContext.AuthorModel'  is null.");
        }

        // GET: Author/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Gender")] AuthorModel authorModel)
        {
            if (ModelState.IsValid)
            {
                authorModel.Id = Guid.NewGuid();
                _context.Add(authorModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(authorModel);
        }

        // GET: Author/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Author == null)
            {
                return NotFound();
            }

            var authorModel = await _context.Author
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authorModel == null)
            {
                return NotFound();
            }

            return View(authorModel);
        }



        // GET: Author/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Author == null)
            {
                return NotFound();
            }

            var authorModel = await _context.Author.FindAsync(id);
            if (authorModel == null)
            {
                return NotFound();
            }
            return View(authorModel);
        }

        // POST: Author/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Gender")] AuthorModel authorModel)
        {
            if (id != authorModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorModelExists(authorModel.Id))
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
            return View(authorModel);
        }

        // GET: Author/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Author == null)
            {
                return NotFound();
            }

            var authorModel = await _context.Author
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authorModel == null)
            {
                return NotFound();
            }

            return View(authorModel);
        }
        // POST: Author/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Author == null)
            {
                return Problem("There is no model database.");
            }
            var authorModel = await _context.Author.FindAsync(id);
            if (authorModel != null)
            {
                _context.Author.Remove(authorModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool AuthorModelExists(Guid id)
        {
            return (_context.Author?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}


