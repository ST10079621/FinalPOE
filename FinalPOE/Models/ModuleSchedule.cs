using System.ComponentModel.DataAnnotations;

namespace FinalPOE.Models
{
    public class ModuleSchedule
    {

        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int ModuleId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PlannedDate { get; set; }

        public virtual Module Module { get; set; }

        
        public DayOfWeek ScheduledDay { get; set; }
    }
}
