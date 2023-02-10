using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectoFinal_LR.Models;

namespace ProjectoFinal_LR.Services.Animals
{
    public interface IAnimalsServices
    {
        public List<Animal> GetList();
        public Task<Animal> GetOne(int idAnimal);
        public Task<bool> Add(Animal animal);
        public Task<bool> Update(Animal animalUpdated);
        public Task<bool> Remove(int idAnimal);
        public Task<List<Animal>> Filter(string name);
        public Task<List<Breed>> GetBreedsBySpecie(int idSpecie);
        public List<SelectListItem> SelectAnimals();


    }
}
