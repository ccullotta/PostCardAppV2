using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PostCardAppV2.Backend;
using PostCardAppV2.Data;
using PostCardAppV2.Models;
using PostCardAppV2.Models.dtos;

namespace PostCardAppV2.Controllers
{
    public class QuotesController : Controller
    {
        private readonly PostCardAppContext _context;

        public QuotesController(PostCardAppContext context)
        {
            _context = context;
        }

        // GET: Quotes
        public async Task<IActionResult> Index(int? id, DateTime? date)
        {

            if (id.HasValue)
            {
                return View(_context.Quotes.Include(x => x.Estimates).Where(x => x.ID == id));
            }
            else if (date.HasValue)
            {
                var chicagoTime = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                var createdDate = TimeZoneInfo.ConvertTimeToUtc((DateTime)date, chicagoTime);
                return View(_context.Quotes.Include(x => x.Estimates).Where(x => x.CreatedOn.Date == createdDate.Date).OrderByDescending(x => x.CreatedOn));
            }
            var postCardAppContext = _context.Quotes.Include(x => x.Estimates).OrderByDescending(x => x.CreatedOn);
            return View(await postCardAppContext.ToListAsync());
        }


        public async Task<string> GetCardSizes(int paperId)
        {

            var result = _context.Papers.Include(x => x.CostAssignments).ThenInclude(x => x.Sheet).FirstOrDefault(x => x.ID == paperId);
            var sizes = result.GetCompatibleSizes(_context).Select(x => new { id = x.ID, name = x.Name });
            return JsonConvert.SerializeObject(sizes);
        }
        public async Task<string> GetPapers(int CardSizeId)
        {
            var card = _context.CardSize.Find(CardSizeId);
            var ret = _context.Papers.Include(x => x.CostAssignments).ThenInclude(x => x.Sheet).Where(x => x.CostAssignments.Any(x => x.Sheet.width >= card.width && x.Sheet.length >= card.length)).Select(x => new { id = x.ID, name = x.Name });
            return JsonConvert.SerializeObject(ret);
        }


        // GET: Quotes/Create
        public IActionResult Create()
        {
            ViewData["CardSizeId"] = new SelectList(_context.CardSize, "ID", "Name");
            ViewData["ColorId"] = new SelectList(_context.CardColor, "ID", "Name");
            ViewData["PaperId"] = new SelectList(_context.Papers, "ID", "Name");
            var input = new QuotesCreateDto()
            {
                QuantitiesAndPrices = new List<Estimate>()
                {
                    new Estimate(){Quantity = 0, Price = 0.0}
                }
            };
            return View(input);
        }

        private async Task<Quote> CalculateValidQuote(QuotesCreateDto model)
        {
            var paper = await _context.Papers
                .Include(x => x.CostAssignments)
                .ThenInclude(x => x.Sheet)
                .FirstOrDefaultAsync(x => x.ID == model.PaperId);
            var cardSize = await _context.CardSize.FirstOrDefaultAsync(x => x.ID == model.CardSizeId);
            var color = await _context.CardColor.FirstOrDefaultAsync(x => x.ID == model.ColorId);
            var fitter = new GetCardFit(paper, cardSize, model.WithBleed, _context);
            var onSheet = fitter.GetBestFitCount();
            var sheetCost = _context.PaperSheetAssignments
                .FirstOrDefault(x => x.SheetId == fitter.GetBestSheet().ID && x.PaperId == model.PaperId).Cost;
            var newQuote = new Quote()
            {
                CustomerName = model.CustomerName,
                Paper = paper.Name,
                CardSize = cardSize.Name,
                Color = color.Name,
                WithBleed = model.WithBleed,
                Estimates = new List<Estimate>(),
                CreatedOn = DateTime.UtcNow,
            };
            for (int i = 0; i < model.QuantitiesAndPrices.Count(); i++)
            {
                newQuote.Estimates.Add(new Estimate()
                {
                    QuoteId = newQuote.ID,
                    Quantity = model.QuantitiesAndPrices[i].Quantity,
                    Price = Math.Round(new QuotesCalculator(model.QuantitiesAndPrices[i].Quantity, onSheet, sheetCost, color).getTotalCost(), 2),
                });
                model.QuantitiesAndPrices[i] = new Estimate(newQuote.Estimates[i].Quantity, newQuote.Estimates[i].Price);
            }
            return newQuote;
        }

        private void resetViewData(QuotesCreateDto model)
        {
            ViewData["CardSizeId"] = new SelectList(_context.CardSize, "ID", "Name", model.CardSizeId);
            ViewData["ColorId"] = new SelectList(_context.CardColor, "ID", "Name", model.ColorId);
            ViewData["PaperId"] = new SelectList(_context.Papers, "ID", "Name", model.PaperId);
        }
        // POST: Quotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuotesCreateDto model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var newQuote = await CalculateValidQuote(model);
                    _context.Add(newQuote);
                    resetViewData(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (InvalidOperationException e)
            {
                ViewData["Error"] = e.Message;

            }
            ViewData["Error"] ??= "Invalid Input";
            resetViewData(model);
            await _context.SaveChangesAsync();
            return View(model);
        }
        // POST: Quotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Calculate(QuotesCreateDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newQuote = await CalculateValidQuote(model);
                    model.QuantitiesAndPrices = newQuote.Estimates;
                    resetViewData(model);
                    return View("Create",model);
                }
            }
            catch (InvalidOperationException e)
            {
                ViewData["Error"] = e.Message;

            }
            ViewData["Error"] ??= "Invalid Input";
            resetViewData(model);
            return View("Create", model);
        }


        public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var quote = _context.Quotes.Include(x => x.Estimates).FirstOrDefault(x => x.ID == id);
        if (quote == null)
        {
            return NotFound();
        }
        return View(quote);
    }
    // GET: Quotes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var quote = await _context.Quotes
            .FirstOrDefaultAsync(m => m.ID == id);
        if (quote == null)
        {
            return NotFound();
        }

        return View(quote);
    }

    // POST: Quotes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var quote = await _context.Quotes.FindAsync(id);
        _context.Quotes.Remove(quote);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool QuoteExists(int id)
    {
        return _context.Quotes.Any(e => e.ID == id);
    }
}
}
