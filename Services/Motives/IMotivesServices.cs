using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectoFinal_LR.Models;

namespace ProjectoFinal_LR.Services.Motives
{
    public interface IMotivesServices
    {
        public List<Motive> GetList();
        public List<SelectListItem> SelectMotives();
    }
}
