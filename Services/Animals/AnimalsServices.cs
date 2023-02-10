using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectoFinal_LR.DatabaseContext;
using ProjectoFinal_LR.Models;
using System.Diagnostics;

namespace ProjectoFinal_LR.Services.Animals
{
    public class AnimalsServices : IAnimalsServices
    {
        private readonly MainDbContext _dbContext;
        public AnimalsServices(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Animal> GetList()
        {
            List<Animal> list;
            try
            {
                list =  _dbContext.Animals.Include(c => c.Client).Include(s => s.Specie)
                    .OrderBy(o => o.Name).ToList();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

            return list;
        }

        public async Task<Animal> GetOne(int idAnimal)
        {
            Animal animal = null;

            try
            {
                animal = await _dbContext.Animals
                    .Where(a => a.IdAnimal == idAnimal)
                    .Include(c => c.Client)
                    .Include(s => s.Specie)
                    .Include(b => b.Breed)
                    .FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

            return animal;
        }

        public async Task<bool> Add(Animal animal)
        {
            if (animal == null)
            {
                return false;
            }

            try
            {
                await _dbContext.Animals.AddAsync(animal);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public async Task<bool> Update(Animal animalUpdated)
        {
            try
            {
                _dbContext.Animals.Update(animalUpdated);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public async Task<bool> Remove(int idAnimal)
        {
            var animal = await GetOne(idAnimal);

            if (animal == null)
            {
                return false;
            }

            try
            {
                _dbContext.Animals.Remove(animal);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public async Task<List<Animal>> Filter(string name)
        {
            List<Animal> animals = null;

            try
            {
                if(name == null)
                {
                    animals = await _dbContext.Animals
						.Include(c => c.Client)
                        .Include(s => s.Specie)
						.ToListAsync<Animal>();
                }
                else
                {
                    animals = await _dbContext.Animals
                        .Where(a => a.Name.ToLower().Contains(name.ToLower()))
						.Include(c => c.Client)
                        .Include(s => s.Specie)
						.ToListAsync<Animal>();
                }

            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

            return animals;
        }

        public async Task<List<Breed>> GetBreedsBySpecie(int idSpecie)
        {
            List<Breed> breeds = null;

            try
            {
                breeds = await _dbContext.Breeds.Where(b => b.IdSpecie == idSpecie).ToListAsync<Breed>();

            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

            return breeds;
        }

        // Função que permite "popular" o select(drop-down) que se encontra na view do create/edit
        public List<SelectListItem> SelectAnimals()
        {
            // cria um elemento vazio
            var animals = new List<SelectListItem>
            {
                new SelectListItem { Text = "", Value = 0.ToString() }
            };

            foreach (var item in GetList())
            {
                animals.Add(new SelectListItem { Text = item.Name, Value = item.IdAnimal.ToString() });
            }

            return animals;
        }
    }
}
