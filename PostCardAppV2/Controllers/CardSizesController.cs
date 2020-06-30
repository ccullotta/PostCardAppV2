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
    public class CardSizesController : Controller
    {
        private readonly PostCardAppContext _context;

        public CardSizesController(PostCardAppContext context)
        {
            _context = context;
        }

        // GET: CardSizes
        public async Task<IActionResult> Index()
        {
            return View(await _context.CardSize.ToListAsync());
        }


        // GET: CardSizes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CardSizes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,length,width")] CardSize cardSize)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cardSize);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cardSize);
        }

        // GET: CardSizes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardSize = await _context.CardSize.FindAsync(id);
            if (cardSize == null)
            {
                return NotFound();
            }
            return View(cardSize);
        }

        // POST: CardSizes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] CardSize cardSize)
        {
            if (id != cardSize.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cardSize);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardSizeExists(cardSize.ID))
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
            return View(cardSize);
        }

        // GET: CardSizes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardSize = await _context.CardSize
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cardSize == null)
            {
                return NotFound();
            }

            return View(cardSize);
        }

        // POST: CardSizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cardSize = await _context.CardSize.FindAsync(id);
            _context.CardSize.Remove(cardSize);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardSizeExists(int id)
        {
            return _context.CardSize.Any(e => e.ID == id);
        }
    }
}
