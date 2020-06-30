using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PostCardAppV2.Data;
using PostCardAppV2.Models;
using PostCardAppV2.Models.dtos;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore.Internal;
using static PostCardAppV2.Models.Paper;

#nullable enable

namespace PostCardAppV2.Controllers
{
    public class PapersController : Controller
    {
        private readonly PostCardAppContext _context;

        public PapersController(PostCardAppContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> UploadCsv(IFormFile postedFile)
        {
            //var result = new StringBuilder();
            using (var reader = new StreamReader(postedFile.OpenReadStream()))
            {
                string[] result;
                String[] Headers = reader.ReadLine().Replace(" ", "").ToLower().Split(",");
                Dictionary<string, int> head = new Dictionary<string, int>
                {
                    {"sheetsize", Array.IndexOf(Headers, "sheetsize")},
                    {"coating", Array.IndexOf(Headers, "coating")},
                    {"weight", Array.IndexOf(Headers, "weight")},
                    {"stocktype", Array.IndexOf(Headers, "stocktype")},
                    {"price", Array.IndexOf(Headers, "price")},
                    {"color", Array.IndexOf(Headers, "color")},
                };
                HashSet<Paper> papers = new HashSet<Paper>();
                HashSet<Sheets> sheetSizes = new HashSet<Sheets>();
                var line = reader.ReadLine();
                while (line != null)
                {
                    result = line.ToLower().Replace(" ", "").Split(",");
                    var sheet = new Sheets() { Name = result[head["sheetsize"]] };
                    var paper = new Paper()
                    {
                        PaperCoating = (Coating)Enum.Parse(typeof(Coating), result[head["coating"]], true),
                        PaperStockType = (StockType)Enum.Parse(typeof(StockType), result[head["stocktype"]], true),
                        PaperColor = result[head["color"]],
                        Weight = result[head["weight"]],
                    };
                    if (!sheetSizes.Add(sheet))
                    {
                        sheet = sheetSizes.First(x => x.Name == sheet.Name);
                    }
                    if (papers.Add(paper))
                    {
                        paper.CostAssignments = new List<PaperSheetAssignments>();
                        paper.CostAssignments.Add(new PaperSheetAssignments() { Cost = double.Parse(result[head["price"]]), Paper = paper, Sheet = sheet });
                    } else
                    {
                        paper = papers.First(x => x.Name == paper.Name);
                        paper.CostAssignments.Add(new PaperSheetAssignments() { Cost = double.Parse(result[head["price"]]), Paper = paper, Sheet = sheet });
                    }
                    line = reader.ReadLine();
                }
                _context.RemoveRange(_context.Sheets);
                _context.RemoveRange(_context.Papers);
                _context.AddRange(sheetSizes);
                await _context.SaveChangesAsync();
                _context.AddRange(papers);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));

        }



        // GET: Papers
        public async Task<IActionResult> Index()
        {
            var papers = _context.Papers
                .Include(x => x.CostAssignments)
                .ThenInclude(x => x.Sheet)
                .ToList();
            papers.ForEach(p => p.UpdateSizes(_context));
            return View(papers.OrderBy(x => x.Name));
        }



        // GET: Papers/Create
        public IActionResult Create(string? errormsg)
        {
            var model = new PaperCreateModel();
            model.SheetPricing = _context.Sheets.ToDictionary(x => x.Name, x => "0");
            if (errormsg != null)
            {
                ViewData["ErrorMessage"] = errormsg;
            }
            return View(model);
        }

        // POST: Papers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PaperCoating,PaperColor, PaperStockType, Weight, SheetPricing")] PaperCreateModel paper)
        {
            if (ModelState.IsValid)
            {
                var newpaper = new Paper { 
                    PaperCoating = (Coating)paper.PaperCoating,
                    PaperColor = paper.PaperColor,
                    PaperStockType = paper.PaperStockType,
                    Weight = paper.Weight,
                };
                newpaper.CostAssignments = paper.SheetPricing.Select(x => new PaperSheetAssignments
                {
                    Cost = double.Parse(x.Value),
                    SheetId = _context.Sheets.FirstOrDefault(y => y.Name == x.Key).ID,
                    PaperId = newpaper.ID,
                }).Where(x => x.Cost != 0).ToList();

                _context.Add(newpaper);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(paper);
        }

        // GET: Papers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paper = await _context.Papers.Include(x => x.CostAssignments).ThenInclude(x => x.Sheet).FirstOrDefaultAsync(x => x.ID == id);

            var input = new PaperEditModel { ID = paper.ID,
                PaperStockType = (StockType)paper.PaperStockType,
                PaperColor = paper.PaperColor,
                PaperCoating = paper.PaperCoating,
                Weight = paper.Weight,
            };
            input.EditAssignments = paper.CostAssignments.Select(x => new PaperEditAssignments { Name = x.Sheet.Name, Price = x.Cost }).ToList();
            var sheets = _context.Sheets.ToList();
            sheets.RemoveAll(x=>input.EditAssignments.Any(y=>y.Name==x.Name));
            input.EditAssignments.AddRange(sheets.Select(x=> new PaperEditAssignments { Name = x.Name, Price = 0.0 }).ToList());
            if (paper == null)
            {
                return NotFound();
            }
            return View(input);
        }

        // POST: Papers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PaperCoating,PaperColor, PaperStockType, " +
            "Weight, EditAssignments, EditAssignments.Name, EditAssignments.Price")] PaperEditModel paper)
        {
            if (id != paper.ID)
            {
                return NotFound();
            }
            var update = _context.Papers.Include(x => x.CostAssignments).ThenInclude(x => x.Sheet).First(x => x.ID == id);
            update.PaperCoating = paper.PaperCoating;
            update.PaperColor = paper.PaperColor;
            update.PaperStockType = paper.PaperStockType;
            update.Weight = paper.Weight;
            update.CostAssignments = paper.EditAssignments.Select(x => new PaperSheetAssignments() { PaperId = id, SheetId = _context.Sheets.First(y => y.Name == x.Name).ID, Cost = x.Price }).ToList();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(update);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaperExists(paper.ID))
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
            return View(update);
        }

        // GET: Papers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paper = await _context.Papers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (paper == null)
            {
                return NotFound();
            }

            return View(paper);
        }

        // POST: Papers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paper = await _context.Papers.FindAsync(id);
            _context.Papers.Remove(paper);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaperExists(int id)
        {
            return _context.Papers.Any(e => e.ID == id);
        }
    }
}
