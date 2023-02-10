using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectoFinal_LR.Models;

namespace ProjectoFinal_LR.Services.Species
{
    public interface ISpeciesServices
    {
        public List<Specie> GetList();
        public List<SelectListItem> SelectSpecies();
    }
}
