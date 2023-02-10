using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectoFinal_LR.Models;

namespace ProjectoFinal_LR.Services.Priorities
{
    public interface IPrioritiesServices
    {
        public List<Priority> GetList();
        public List<SelectListItem> SelectPriorities();

    }
}
