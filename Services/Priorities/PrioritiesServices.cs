using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging.Abstractions;
using ProjectoFinal_LR.DatabaseContext;
using ProjectoFinal_LR.Models;
using System.Diagnostics;

namespace ProjectoFinal_LR.Services.Priorities
{
    public class PrioritiesServices : IPrioritiesServices
    {
        private readonly MainDbContext _dbContext;

        public PrioritiesServices(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Priority> GetList()
        {
            List<Priority> list;
            try
            {
                list = _dbContext.Priorities.ToList();

            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

            return list;
        }

        public List<SelectListItem> SelectPriorities()
        {
            var priorities = new List<SelectListItem>();

            priorities.Add(new SelectListItem { Text = "", Value = 0.ToString() });
            foreach (var item in GetList())
            {
                priorities.Add(new SelectListItem { Text = item.Type, Value = item.IdPriority.ToString() });
            }

            return priorities;
        }
    }
}
