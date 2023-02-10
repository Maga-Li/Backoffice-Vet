using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectoFinal_LR.DatabaseContext;
using ProjectoFinal_LR.Models;
using System.Diagnostics;

namespace ProjectoFinal_LR.Services.Species
{
    public class SpeciesServices : ISpeciesServices
    {
        private readonly MainDbContext _dbContext;

        public SpeciesServices(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Specie> GetList()
        {
            List<Specie> specie;
            try
            {
                specie = _dbContext.Species.ToList();

            }catch(Exception ex) 
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

            return specie;
        }

        public List<SelectListItem> SelectSpecies()
        {
            var species = new List<SelectListItem>();

			species.Add(new SelectListItem { Text = "", Value = 0.ToString() });
			foreach (var item in GetList())
            {
                species.Add(new SelectListItem { Text = item.Name, Value = item.IdSpecie.ToString() });
            }

            return species;
        }
    }
}
