using FinalPOE.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalPOE.Data
{

    public class ApplicationDbContext : DbContext
    {

        public DbSet<ModuleSchedule> ModuleSchedules { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<StudyHours> StudyHours { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }

    

   


}
