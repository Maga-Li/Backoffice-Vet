using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectoFinal_LR.Models;
using ProjectoFinal_LR.Services.Clients;

namespace ProjectoFinal_LR.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly IClientsServices _clientsServices;
        private readonly INotyfService _notyfService;

        public ClientController(IClientsServices clientsServices, INotyfService notyfService)
        {
            _clientsServices = clientsServices;
            _notyfService = notyfService;
        }

        [HttpGet]
        public IActionResult List()
        {
            var list =  _clientsServices.GetList();

            if(list == null)
            {
                return NotFound();
            }

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int idClient)
        {
            var client = await _clientsServices.GetOne(idClient);

            if(client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var client = new Client();

            return View(client);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Client client)
        {
			if (client == null)
			{
				return BadRequest();
			}

			if (await _clientsServices.Add(client))
            {
                _notyfService.Success("Cliente criado com sucesso!");
                return RedirectToAction(nameof(List));
            }
            else
            {
                _notyfService.Error("Erro ao criar o cliente");
			    ViewData["Message"] = "NIF já existente!";
			}

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int idClient)
        {
            if(idClient == 0)
            {
                return NotFound();
            }

            var client = await _clientsServices.GetOne(idClient);

            if(client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Client client)
        {
            if(await _clientsServices.Update(client))
            {
                _notyfService.Success("Cliente editado com sucesso!");
                return RedirectToAction(nameof(List));
            }
			else
			{
                _notyfService.Error("Erro ao editar o cliente");
				ViewData["Message"] = "NIF já existente!";
			}

			return View();
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int idClient)
        {
            if(await _clientsServices.Remove(idClient))
            {
                return RedirectToAction(nameof(List));
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string name)
        {
            var client = await _clientsServices.Filter(name);

            if(client == null)
            {
                return NotFound();
            }

            return View("List", client);
        }

    }
}
