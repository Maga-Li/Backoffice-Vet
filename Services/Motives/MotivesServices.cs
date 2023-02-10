using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectoFinal_LR.DatabaseContext;
using ProjectoFinal_LR.Models;
using System.Diagnostics;

namespace ProjectoFinal_LR.Services.Motives
{
    public class MotivesServices : IMotivesServices
    {
        private readonly MainDbContext _dbContext;

        public MotivesServices(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Motive> GetList()
        {
            List<Motive> list;

            try
            {
                list = _dbContext.Motives.ToList();

            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

            return list;
        }

        public List<SelectListItem> SelectMotives()
        {
            var motives = new List<SelectListItem>();

            motives.Add(new SelectListItem { Text = "", Value = 0.ToString() });
            foreach (var item in GetList())
            {
                motives.Add(new SelectListItem { Text = item.Description, Value = item.IdMotive.ToString() });
            }

            return motives;
        }
    }
}
