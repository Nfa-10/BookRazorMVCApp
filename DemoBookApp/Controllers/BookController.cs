﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoBookApp.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoBookApp.Data;
using DemoBookApp.Models;
using Microsoft.AspNetCore.Authentication;

namespace DemoBookApp.Controllers
{
    public class BookController : Controller
    {
        private readonly BookDbContext _context;

        public BookController(BookDbContext context)
        {
            _context = context;
        }
        // GET: Book
        public async Task<IActionResult> Index(Guid? id, string searchString)
        {
            List<AuthorModel> authorList = _context.Author.ToList();
         
            ViewBag.Authors = new SelectList(authorList, "Id", "Name");

            if (id != null || !String.IsNullOrEmpty(searchString))
            {
                var books = _context.Books.Include(b => b.Author).Where(b=>b.Author.Id == id || b.Title!.Contains(searchString));
               
           
                if (!books.Any())
                {
                    ViewBag.Message = Message.NO_BOOK;
                    return View(await books.ToListAsync());
                }
                return View(await books.ToListAsync());
            }
            ViewBag.SelectedAuthorId = id;
            return View(await _context.Books.ToListAsync());
        }


        // GET: Book/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var bookModel = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (bookModel == null)
            {
                return NotFound();
            }
            return View(bookModel);
        }
        // GET: Book/Create
        public IActionResult Create()
        {
            ViewData["AuthorID"] = new SelectList(_context.Author, "Id", "Name");
            return View();
        }

        // POST: Book/Create
       
        [HttpPost]
        public async Task<IActionResult> Create([Bind("BookId,Title,Edition,Price,dateOfPublishing,AuthorID")] BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                bookModel.BookId = Guid.NewGuid();
                _context.Add(bookModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(_context.Author, "Id", "Name", bookModel.AuthorID);
            return View(bookModel);
        }

        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var bookModel = await _context.Books.FindAsync(id);
            if (bookModel == null)
            {
                return NotFound();
            }
            ViewData["AuthorID"] = new SelectList(_context.Author, "Id", "Name", bookModel.AuthorID);
            return View(bookModel);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("BookId,Title,Edition,Price,dateOfPublishing,AuthorID")] BookModel bookModel)
        {
            if (id != bookModel.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookModelExists(bookModel.BookId))
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
            ViewData["AuthorID"] = new SelectList(_context.Author, "Id", "Name", bookModel.AuthorID);
            return View(bookModel);
        }

        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var bookModel = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (bookModel == null)
            {
                return NotFound();
            }

            return View(bookModel);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Books == null)
            {
                return Problem(Message.NO_BOOK_CONTEXT);
            }
            var bookModel = await _context.Books.FindAsync(id);
            if (bookModel != null)
            {
                _context.Books.Remove(bookModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookModelExists(Guid id)
        {
            return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
