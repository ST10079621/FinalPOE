using System.Reflection;
using System;

namespace TimeManagementLibrary
{
    public class ModuleService
    {
        private readonly AppDbContext context;

        public ModuleService(AppDbContext dbContext)
        {
            context = dbContext;
        }

        public void AddModule(int userId, string code, string name, int credits, int classHours)
        {
            var user = context.Users.Include(u => u.Modules).FirstOrDefault(u => u.UserId == userId);

            if (user != null)
            {
                var module = new Module
                {
                    UserId = userId,
                    Code = code,
                    Name = name,
                    Credits = credits,
                    ClassHoursPerWeek = classHours,
                    SelfStudyHoursPerWeek = (credits * 10) - classHours
                };

                user.Modules.Add(module);
                context.SaveChanges();
            }
        }
        public void UpdateModule(int moduleId, string newCode, string newName, int newCredits, int newClassHours)
        {
            var module = context.Modules.Find(moduleId);

            if (module != null)
            {
                module.Code = newCode;
                module.Name = newName;
                module.Credits = newCredits;
                module.ClassHoursPerWeek = newClassHours;
                module.SelfStudyHoursPerWeek = newCredits * 10 - newClassHours;

                context.SaveChanges();
            }
        }

        public void DeleteModule(int moduleId)
        {
            var module = context.Modules.Find(moduleId);

            if (module != null)
            {
                context.Modules.Remove(module);
                context.SaveChanges();
            }
        }

        private void LoadModules(int userId)
        {
            ModulesListBox.Items.Clear();
            var modules = moduleService.GetModulesForUser(userId);

            foreach (var module in modules)
            {
                string moduleInfo = $"{module.Code} - {module.Name}: {module.SelfStudyHoursPerWeek} hours/week";
                ModulesListBox.Items.Add(moduleInfo);
            }
        }
    }
}