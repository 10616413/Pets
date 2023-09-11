using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pets.Data;
using Pets.Models;

namespace Pets.Controllers
{
    public class PetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PetController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pet
        public async Task<IActionResult> Index()
        {
              return _context.PetModels != null ? 
                          View(await _context.PetModels.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.PetModels'  is null.");
        }

        // GET: Pet/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.PetModels == null)
            {
                return NotFound();
            }

            var petModel = await _context.PetModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petModel == null)
            {
                return NotFound();
            }

            return View(petModel);
        }

        // GET: Pet/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,petName,ownerName,Breed,Age,Address")] PetModel petModel)
        {
            if (ModelState.IsValid)
            {
                petModel.Id = Guid.NewGuid();
                _context.Add(petModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(petModel);
        }

        // GET: Pet/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.PetModels == null)
            {
                return NotFound();
            }

            var petModel = await _context.PetModels.FindAsync(id);
            if (petModel == null)
            {
                return NotFound();
            }
            return View(petModel);
        }

        // POST: Pet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,petName,ownerName,Breed,Age,Address")] PetModel petModel)
        {
            if (id != petModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(petModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetModelExists(petModel.Id))
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
            return View(petModel);
        }

        // GET: Pet/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.PetModels == null)
            {
                return NotFound();
            }

            var petModel = await _context.PetModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petModel == null)
            {
                return NotFound();
            }

            return View(petModel);
        }

        // POST: Pet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.PetModels == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PetModels'  is null.");
            }
            var petModel = await _context.PetModels.FindAsync(id);
            if (petModel != null)
            {
                _context.PetModels.Remove(petModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetModelExists(Guid id)
        {
          return (_context.PetModels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
