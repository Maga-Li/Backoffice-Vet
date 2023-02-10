using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectoFinal_LR.Models;
using ProjectoFinal_LR.Services.Animals;
using ProjectoFinal_LR.Services.Breeds;
using ProjectoFinal_LR.Services.Clients;
using ProjectoFinal_LR.Services.Species;


namespace ProjectoFinal_LR.Controllers
{
	[Authorize]
	public class AnimalController : Controller
	{
		private readonly IAnimalsServices _animalsServices;
		private readonly IClientsServices _clientsServices;
		private readonly ISpeciesServices _speciesServices;
		private readonly IBreedsServices _breedsServices;
		private readonly INotyfService _notyfService;

		public AnimalController(IAnimalsServices animalsServices, IClientsServices clientsServices, ISpeciesServices speciesServices, IBreedsServices breedsServices, INotyfService notyfService)
		{
			_animalsServices = animalsServices;
			_clientsServices = clientsServices;
			_speciesServices = speciesServices;
			_breedsServices = breedsServices;
			_notyfService = notyfService;
		}

		[HttpGet]
		public IActionResult List()
		{

			var list = _animalsServices.GetList();

			if (list == null)
			{
				return NotFound();
			}

			return View(list);
		}

		[HttpGet]
		public async Task<IActionResult> Details(int idAnimal)
		{
			var animal = await _animalsServices.GetOne(idAnimal);

			if (animal == null)
			{
				return NotFound();
			}

			return View(animal);
		}

		[HttpGet]
		public IActionResult Create()
		{
			var animal = new Animal();

			ViewBag.IdClient = _clientsServices.SelectClients();
			ViewBag.IdSpecie = _speciesServices.SelectSpecies();

			return View(animal);
		}

		[HttpPost]
		public async Task<IActionResult> Create(Animal animal)
		{
			if (await _animalsServices.Add(animal))
			{
				_notyfService.Success("Animal criado com sucesso!");
				return RedirectToAction(nameof(List));
			}
			else
			{
				_notyfService.Error("Erro ao criar o animal!");
			}

			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int idAnimal)
		{
			if (idAnimal == 0)
			{
				return NotFound();
			}

			var animal = await _animalsServices.GetOne(idAnimal);

			if (animal == null)
			{
				return NotFound();
			}

			ViewBag.IdClient = _clientsServices.SelectClients();
			ViewBag.IdSpecie = _speciesServices.SelectSpecies();
			ViewBag.IdBreed = _breedsServices.SelectBreeds();

			return View(animal);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Animal animal)
		{
			if (await _animalsServices.Update(animal))
			{
				_notyfService.Success("Animal editado com sucesso!");
				return RedirectToAction(nameof(List));
			}
			else
			{
				_notyfService.Error("Erro ao editar o animal!");
			}

			return View();
		}


		[HttpGet]
		public async Task<IActionResult> Delete(int idAnimal)
		{
			if (await _animalsServices.Remove(idAnimal))
			{
				return RedirectToAction(nameof(List));
			}

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Search(string name)
		{
			var animal = await _animalsServices.Filter(name);

			if (animal == null)
			{
				return NotFound();
			}

			return View("List", animal);
		}

		// Função que retorna um JsonResult para o Javascript
		[HttpGet]
		public async Task<JsonResult> GetBreedsBySpecie(int idSpecie)
		{
			var breeds = await _animalsServices.GetBreedsBySpecie(idSpecie);

			if (breeds == null)
			{
				return null;
			}

			return Json(breeds);
		}
	}



}
