using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PostCardAppV2.Data;
using PostCardAppV2.Models;

namespace PostCardAppV2.Controllers
{
    public class SheetsController : Controller
    {
        private readonly PostCardAppContext _context;

        public SheetsController(PostCardAppContext context)
        {
            _context = context;
        }

        // GET: Sheets
        public async Task<IActionResult> Index()
        {
            return View(_context.Sheets.ToListAsync().Result.OrderByDescending(x => x.size));
        }



        // GET: Sheets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sheets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] Sheets sheets)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sheets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sheets);
        }

        // GET: Sheets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sheets = await _context.Sheets.FindAsync(id);
            if (sheets == null)
            {
                return NotFound();
            }
            return View(sheets);
        }

        // POST: Sheets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] Sheets sheets)
        {
            if (id != sheets.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sheets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SheetsExists(sheets.ID))
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
            return View(sheets);
        }

        // GET: Sheets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sheets = await _context.Sheets
                .FirstOrDefaultAsync(m => m.ID == id);
            if (sheets == null)
            {
                return NotFound();
            }

            return View(sheets);
        }

        // POST: Sheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sheets = await _context.Sheets.FindAsync(id);
            _context.Sheets.Remove(sheets);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SheetsExists(int id)
        {
            return _context.Sheets.Any(e => e.ID == id);
        }
    }
}
