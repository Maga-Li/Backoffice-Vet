using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectoFinal_LR.DatabaseContext;
using ProjectoFinal_LR.Models;
using System.Diagnostics;

namespace ProjectoFinal_LR.Services.Clients
{
    public class ClientsServices : IClientsServices
    {
        private readonly MainDbContext _dbContext;

        public ClientsServices(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Client> GetList()
        {
            List<Client> list;

            try
            {
                list = _dbContext.Clients.OrderBy(o => o.Name).ToList();

            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

            return list;
        }

        public async Task<Client> GetOne(int idClient)
        {
            Client client = null;

            try
            {
                client = await _dbContext.Clients.Where(c => c.IdClient == idClient).FirstOrDefaultAsync();

            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

            return client;
        }

        public async Task<bool> Add(Client client)
        {

            try
            {
                if(await _dbContext.Clients.AnyAsync(x => x.TaxNumber == client.TaxNumber))
                {
                    return false;
                }
                else
                {
                    await _dbContext.Clients.AddAsync(client);
                    await _dbContext.SaveChangesAsync();
                }

            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public async Task<bool> Update(Client clientUpdated)
        {
            try
            {
                if(await _dbContext.Clients
                    .AnyAsync(x => x.TaxNumber == clientUpdated.TaxNumber &&
                        x.IdClient != clientUpdated.IdClient))
                {
                    return false;
                }
                else
                {
                    _dbContext.Clients.Update(clientUpdated);
                    await _dbContext.SaveChangesAsync();
                }

            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public async Task<bool> Remove(int idClient)
        {
            var client = await GetOne(idClient);

            if(client == null)
            {
                return false;
            }

            try
            {
                _dbContext.Clients.Remove(client);
                await _dbContext.SaveChangesAsync();

            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public async Task<List<Client>> Filter(string name)
        {
            List<Client> clients = null;

            try
            {
                if(name == null)
                {
                    clients = await _dbContext.Clients.ToListAsync<Client>();
                }
                else
                {
                    clients = await _dbContext.Clients
                        .Where(c => c.Name.ToLower().Contains(name.ToLower()))
                        .ToListAsync<Client>();
                }

            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

            return clients;

        }

        // Função que permite "popular" o select que se encontra na view
        public List<SelectListItem> SelectClients()
        {
           var clients = new List<SelectListItem>();

            clients.Add(new SelectListItem { Text = "", Value = 0.ToString() });
            foreach (var item in GetList())
            {
                clients.Add(new SelectListItem{Text = item.Name, Value = item.IdClient.ToString()});
            }

            return clients;
        }
    }
}
