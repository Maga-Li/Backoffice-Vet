using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectoFinal_LR.DatabaseContext;
using ProjectoFinal_LR.Models;
using System.Diagnostics;

namespace ProjectoFinal_LR.Services.Veterinarians
{
    public class VeterinariansServices : IVeterinariansServices
    {
        private readonly MainDbContext _dbContext;

        public VeterinariansServices(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Veterinarian> GetList()
        {
            List<Veterinarian> list;

            try
            {
                list = _dbContext.Veterinarians.OrderBy(o => o.Name).ToList();

            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
            return list;
        }

        public async Task<Veterinarian> GetOne(int idVeterinarian)
        {
            Veterinarian veterinarian = null;

            try
            {
                veterinarian = await _dbContext.Veterinarians.Where(v => v.IdVeterinarian == idVeterinarian).FirstOrDefaultAsync();

            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

            return veterinarian;
        }

        public async Task<bool> Add(Veterinarian veterinarian)
        {
            if(veterinarian == null)
            {
                return false;
            }

            try
            {
                await _dbContext.Veterinarians.AddAsync(veterinarian);
                await _dbContext.SaveChangesAsync();

            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public async Task<bool> Update(Veterinarian vetUpdated)
        {
            try
            {

                _dbContext.Veterinarians.Update(vetUpdated);
                await _dbContext.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

            return true;
        }


        public async Task<bool> Remove(int idVeterinarian)
        {
            var veterinarian = await GetOne(idVeterinarian);

            if(veterinarian == null)
            {
                return false;
            }

            try
            {
               _dbContext.Veterinarians.Remove(veterinarian);
                await _dbContext.SaveChangesAsync();

            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public async Task<List<Veterinarian>> Filter(string name)
        {
            List<Veterinarian> veterinarians = null;

            try
            {
                if (name == null)
                {
                    veterinarians = await _dbContext.Veterinarians.ToListAsync<Veterinarian>();
                }
                else
                {
                    veterinarians = await _dbContext.Veterinarians
                        .Where(a => a.Name.ToLower().Contains(name.ToLower()))
                        .ToListAsync<Veterinarian>();
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

            return veterinarians;
        }

        public List<SelectListItem> SelectVeterinarians()
        {
            var veterinarians = new List<SelectListItem>();

            veterinarians.Add(new SelectListItem { Text = "", Value = 0.ToString() });
            foreach (var item in GetList())
            {
                veterinarians.Add(new SelectListItem { Text = item.Name, Value = item.IdVeterinarian.ToString() });
            }

            return veterinarians;
        }
    }
}
