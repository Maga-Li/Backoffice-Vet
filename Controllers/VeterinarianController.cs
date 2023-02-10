using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using ProjectoFinal_LR.Models;
using ProjectoFinal_LR.Services.Veterinarians;

namespace ProjectoFinal_LR.Controllers
{
	public class VeterinarianController : Controller
	{
		private readonly IVeterinariansServices _veterinariansServices;
		private readonly INotyfService _notyfService;

		public VeterinarianController(IVeterinariansServices veterinariansServices, INotyfService notyfService)
		{
			_veterinariansServices = veterinariansServices;
			_notyfService = notyfService;
		}

		[HttpGet]
		public IActionResult List()
		{
			var list = _veterinariansServices.GetList();

			if(list == null)
			{
				return NotFound();
			}

			return View(list);
		}

		[HttpGet]
		public IActionResult Create()
		{
			var veterinarian = new Veterinarian();

			return View(veterinarian);
		}

		[HttpPost]
		public async Task<IActionResult> Create(Veterinarian veterinarian)
		{
			if(await _veterinariansServices.Add(veterinarian))
			{
				_notyfService.Success("Veterinário criado com sucesso!");
				return RedirectToAction(nameof(List));
			}
			else
			{
				_notyfService.Error("Erro ao criar o veterinário!");
			}

			return View();
		}

        [HttpGet]
        public async Task<IActionResult> Edit(int idVeterinarian)
        {
            if (idVeterinarian == 0)
            {
                return NotFound();
            }

            var veterinarian = await _veterinariansServices.GetOne(idVeterinarian);

            if (veterinarian == null)
            {
                return NotFound();
            }

            return View(veterinarian);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Veterinarian veterinarian)
        {
            if (await _veterinariansServices.Update(veterinarian))
            {
                _notyfService.Success("Veterinário editado com sucesso!");
                return RedirectToAction(nameof(List));
            }
            else
            {
                _notyfService.Error("Erro ao editar o Veterinário!");
            }

            return View();
        }

        [HttpGet]
		public async Task<IActionResult> Delete(int idVeterinarian)
		{
			if(await _veterinariansServices.Remove(idVeterinarian))
			{
				return RedirectToAction(nameof(List));
			}

			return Ok();
		}

        [HttpPost]
        public async Task<IActionResult> Search(string name)
        {
            var veterinarian = await _veterinariansServices.Filter(name);

            if (veterinarian == null)
            {
                return NotFound();
            }

            return View("List", veterinarian);
        }
    }
}
