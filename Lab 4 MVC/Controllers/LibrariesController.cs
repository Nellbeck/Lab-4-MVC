using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab_4_MVC.Data;
using Lab_4_MVC.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Lab_4_MVC.Controllers
{
    public class LibrariesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LibrariesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Libraries
        public async Task<IActionResult> Index(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                var applicationDbContext = _context.Librarys.Include(l => l.Book).Include(x => x.Customer);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = _context.Librarys.Include(l => l.Book).Where(x => x.Customer.CustomerName.Contains(data)).Include(x => x.Customer);
                return View(await applicationDbContext.ToListAsync());
            }
        }

        // GET: Libraries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var library = await _context.Librarys
                .Include(l => l.Book)
                .Include(l => l.Customer)
                .FirstOrDefaultAsync(m => m.LibraryId == id);
            if (library == null)
            {
                return NotFound();
            }

            return View(library);
        }

        // GET: Libraries/Create
        public IActionResult Create()
        {

                ViewData["FKBookId"] = new SelectList(_context.Books, "BookId", "BookName");
                ViewData["FKCustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName");
                return View();

        }

        // POST: Libraries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LibraryId,FKCustomerId,FKBookId,BorrowDateStart,borrowDateEnd,IsAvaible")] Library library)
        {
            var bookBorrowed = _context.Librarys.Any(x => x.FKBookId == library.FKBookId && x.borrowDateEnd > library.BorrowDateStart);
               if (bookBorrowed) {
                ViewBag.message = $"That book is borrowed. Book is available from {_context.Librarys.OrderBy(x => x.borrowDateEnd).Last().borrowDateEnd}";
                ViewData["FKBookId"] = new SelectList(_context.Books, "BookId", "BookName", library.FKBookId);
                ViewData["FKCustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", library.FKCustomerId);
                return View(library);
                }

                _context.Add(library);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Libraries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var library = await _context.Librarys.FindAsync(id);
            if (library == null)
            {
                return NotFound();
            }
            ViewData["FKBookId"] = new SelectList(_context.Books, "BookId", "BookName", library.FKBookId);
            ViewData["FKCustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", library.FKCustomerId);
            return View(library);
        }

        // POST: Libraries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LibraryId,FKCustomerId,FKBookId,BorrowDateStart,borrowDateEnd,IsAvaible")] Library library)
        {
            if (id != library.LibraryId)
            {
                return NotFound();
            }

                    _context.Update(library);
                    await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            
            ViewData["FKBookId"] = new SelectList(_context.Books, "BookId", "BookName", library.FKBookId);
            ViewData["FKCustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", library.FKCustomerId);
            return View(library);
        }

        // GET: Libraries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var library = await _context.Librarys
                .Include(l => l.Book)
                .Include(l => l.Customer)
                .FirstOrDefaultAsync(m => m.LibraryId == id);
            if (library == null)
            {
                return NotFound();
            }

            return View(library);
        }

        // POST: Libraries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var library = await _context.Librarys.FindAsync(id);
            if (library != null)
            {
                _context.Librarys.Remove(library);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibraryExists(int id)
        {
            return _context.Librarys.Any(e => e.LibraryId == id);
        }
    }
}
