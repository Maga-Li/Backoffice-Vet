using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectoFinal_LR.Models;

namespace ProjectoFinal_LR.Services.Clients
{
    public interface IClientsServices
    {
        public List<Client> GetList();
        public Task<Client> GetOne(int idClient);
        public Task<bool> Add(Client client);
        public Task<bool> Update(Client clientUpdated);
        public Task<bool> Remove(int idClient);
        public Task<List<Client>> Filter(string name);
        public List<SelectListItem> SelectClients();
    }
}
