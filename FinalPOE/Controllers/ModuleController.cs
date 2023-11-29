using Microsoft.AspNetCore.Mvc;
using FinalPOE.Data;
using FinalPOE.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FinalPOE.Controllers
{
    public class ModuleController
    {

        private readonly ApplicationDbContext _context;

        public ModuleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Action to display modules
        public IActionResult Index()
        {
            // Get modules for the current user
            var userId = User.Identity.Name;
            var modules = _context.Modules.Where(m => m.UserId == userId).ToList();

            return View(modules);
        }

        // Action to add a module
        [HttpPost]
        public IActionResult AddModule(Module module)
        {
            if (ModelState.IsValid)
            {
                // Calculate self-study hours per week
                module.SelfStudyHoursPerWeek = (module.Credits * 10) / module.ClassHoursPerWeek;

                // Assign module to the current user
                module.UserId = User.Identity.Name;

                // Save the module to the database
                _context.Modules.Add(module);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(module);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var module = await _context.Modules.FindAsync(id);
            if (module == null)
            {
                return NotFound();
            }

            _context.Modules.Remove(module);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Module module)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(module);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleExists(module.ModuleId))
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
            return View(module);
        }

        private bool ModuleExists(int ModuleId)
        {
            return _context.Modules.Any(e => e.ModuleId == ModuleId);
        }

    }
}
