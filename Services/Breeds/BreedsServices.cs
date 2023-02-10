using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectoFinal_LR.DatabaseContext;
using ProjectoFinal_LR.Models;
using System.Diagnostics;

namespace ProjectoFinal_LR.Services.Breeds
{
    public class BreedsServices : IBreedsServices
    {
        private readonly MainDbContext _dbContext;

        public BreedsServices(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Breed> GetList()
        {
            List<Breed> list;
            try
            {
                list =  _dbContext.Breeds.ToList();

            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

            return list;
        }
        public List<SelectListItem> SelectBreeds()
        {
            var breeds = new List<SelectListItem>();

			breeds.Add(new SelectListItem { Text = "", Value = 0.ToString() });
			foreach (var item in GetList())
            {
                breeds.Add(new SelectListItem { Text = item.Name, Value = item.IdBreed.ToString()});
            }

            return breeds;
        }
    }
}
