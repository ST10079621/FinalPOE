using System;
using System.ComponentModel.DataAnnotations;

namespace FinalPOE.Models
{
    public class Module
    {
        [Key]
        public int ModuleId { get; set; }

       
        public string UserId { get; set; }

       
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Credits { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int ClassHoursPerWeek { get; set; }

        public int SelfStudyHoursPerWeek { get; set; } // Calculated field

        public DateTime StartDate { get; set; }

        public int NumberOfWeeks { get; set; }
        public DayOfWeek ScheduledDay { get; set; }

        public ICollection<StudyHours> StudyHours { get; set; }

        public ICollection<ModuleSchedule> Schedules { get; set; }
    }

    public class StudyHours
    {
        [Key]
        public int StudyHoursId { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }
        public DateTime Date { get; set; }
        public int Hours { get; set; }
    }




}
