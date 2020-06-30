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
    public class CardColorsController : Controller
    {
        private readonly PostCardAppContext _context;

        public CardColorsController(PostCardAppContext context)
        {
            _context = context;
        }

        // GET: CardColors
        public async Task<IActionResult> Index()
        {
            return View(await _context.CardColor.ToListAsync());
        }

        // GET: CardColors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardColor = await _context.CardColor
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cardColor == null)
            {
                return NotFound();
            }

            return View(cardColor);
        }

        // GET: CardColors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CardColors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Multiplier")] CardColor cardColor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cardColor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cardColor);
        }

        // GET: CardColors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardColor = await _context.CardColor.FindAsync(id);
            if (cardColor == null)
            {
                return NotFound();
            }
            return View(cardColor);
        }

        // POST: CardColors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Multiplier")] CardColor cardColor)
        {
            if (id != cardColor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cardColor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardColorExists(cardColor.ID))
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
            return View(cardColor);
        }

        // GET: CardColors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardColor = await _context.CardColor
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cardColor == null)
            {
                return NotFound();
            }

            return View(cardColor);
        }

        // POST: CardColors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cardColor = await _context.CardColor.FindAsync(id);
            _context.CardColor.Remove(cardColor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardColorExists(int id)
        {
            return _context.CardColor.Any(e => e.ID == id);
        }
    }
}
