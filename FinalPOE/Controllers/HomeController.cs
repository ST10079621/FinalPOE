using FinalPOE.Data;
using FinalPOE.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinalPOE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _moduleRepository;

        public HomeController(ApplicationDbContext moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }

        public IActionResult Index()
        {
            // Get the current day of the week
            var currentDay = DateTime.Today.DayOfWeek;

            // Retrieve the module planned for the current day
            var moduleForToday = _moduleRepository.GetModuleByScheduledDay(currentDay);

            if (moduleForToday != null)
            {
                ViewBag.ReminderMessage = $"Today's planned module: {moduleForToday.Name}";
            }
            else
            {
                ViewBag.ReminderMessage = "No module planned for today";
            }

            return View();
        }

    }
}