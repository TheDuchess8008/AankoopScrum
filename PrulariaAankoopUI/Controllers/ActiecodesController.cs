using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;

namespace PrulariaAankoopUI.Controllers
{
    public class ActiecodesController : Controller
    {
        private readonly PrulariaComContext _context;

        public ActiecodesController(PrulariaComContext context)
        {
            _context = context;
        }

        // GET: Actiecodes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Actiecodes.ToListAsync());
        }

        // GET: Actiecodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actiecode = await _context.Actiecodes
                .FirstOrDefaultAsync(m => m.ActiecodeId == id);
            if (actiecode == null)
            {
                return NotFound();
            }

            return View(actiecode);
        }

        // GET: Actiecodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actiecodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActiecodeId,Naam,GeldigVanDatum,GeldigTotDatum,IsEenmalig")] Actiecode actiecode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actiecode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actiecode);
        }

        // GET: Actiecodes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actiecode = await _context.Actiecodes.FindAsync(id);
            if (actiecode == null)
            {
                return NotFound();
            }
            return View(actiecode);
        }

        // POST: Actiecodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActiecodeId,Naam,GeldigVanDatum,GeldigTotDatum,IsEenmalig")] Actiecode actiecode)
        {
            if (id != actiecode.ActiecodeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actiecode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActiecodeExists(actiecode.ActiecodeId))
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
            return View(actiecode);
        }

        // GET: Actiecodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actiecode = await _context.Actiecodes
                .FirstOrDefaultAsync(m => m.ActiecodeId == id);
            if (actiecode == null)
            {
                return NotFound();
            }

            return View(actiecode);
        }

        // POST: Actiecodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actiecode = await _context.Actiecodes.FindAsync(id);
            if (actiecode != null)
            {
                _context.Actiecodes.Remove(actiecode);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActiecodeExists(int id)
        {
            return _context.Actiecodes.Any(e => e.ActiecodeId == id);
        }
    }
}
