using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectoFinal_LR.Models;

namespace ProjectoFinal_LR.Services.Breeds
{
    public interface IBreedsServices
    {
        public List<Breed> GetList();
        public List<SelectListItem> SelectBreeds();
    }
}
