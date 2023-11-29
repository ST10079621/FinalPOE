using Microsoft.AspNetCore.Mvc;
using FinalPOE.Data;
using FinalPOE.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FinalPOE.Controllers
{
    public class ModuleScheduleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ModuleScheduleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Action to display the user's module schedule
        public IActionResult Index()
        {
            var userId = User.Identity.Name;
            var schedule = _context.ModuleSchedules
                .Where(s => s.UserId == userId && s.PlannedDate.Date == DateTime.Today.Date)
                .ToList();

            return View(schedule);
        }

        // Action to set the day for a module
        [HttpPost]
        public IActionResult SetModuleDay(ModuleSchedule moduleSchedule)
        {
            if (ModelState.IsValid)
            {
                moduleSchedule.UserId = User.Identity.Name;
                _context.ModuleSchedules.Add(moduleSchedule);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(moduleSchedule);
        }


        [HttpPost]
        public IActionResult AddModule(Module model)
        {
            if (ModelState.IsValid)
            {
                var module = new Module
                {
                    UserId = model.UserId,
                    Name = model.Name,
                    Credits = model.Credits,
                    ClassHoursPerWeek = model.ClassHoursPerWeek,
                    ScheduledDay = model.ScheduledDay // Set the selected day of the week
                };

                // Save module to the database
                _context.Add(module);

                return RedirectToAction("Index", "Module");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var moduleSchedule = await _context.ModuleSchedules.FindAsync(id);
            if (moduleSchedule == null)
            {
                return NotFound();
            }

            _context.ModuleSchedules.Remove(moduleSchedule);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ModuleSchedule moduleSchedule)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moduleSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleScheduleExists(moduleSchedule.Id))
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
            return View(moduleSchedule);
        }

        private bool ModuleScheduleExists(int id)
        {
            return _context.ModuleSchedules.Any(e => e.Id == id);
        }


    }
}
