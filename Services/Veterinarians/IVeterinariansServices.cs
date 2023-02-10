using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectoFinal_LR.Models;

namespace ProjectoFinal_LR.Services.Veterinarians
{
    public interface IVeterinariansServices
    {
        public List<Veterinarian> GetList();
        public Task<Veterinarian> GetOne(int idVeterinarian);
        public Task<bool> Add(Veterinarian veterinarian);
        public Task<bool> Update(Veterinarian vetUpdated);
        public Task<bool> Remove(int idVeterinarian);
        public Task<List<Veterinarian>> Filter(string name);
        public List<SelectListItem> SelectVeterinarians();
    }
}
