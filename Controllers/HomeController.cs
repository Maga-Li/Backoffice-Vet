using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using ProjectoFinal_LR.Models;
using ProjectoFinal_LR.Services.Animals;
using ProjectoFinal_LR.Services.Appointments;
using ProjectoFinal_LR.Services.Clients;
using ProjectoFinal_LR.Services.Motives;
using ProjectoFinal_LR.Services.Priorities;
using ProjectoFinal_LR.Services.Veterinarians;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Web.Mvc;

namespace ProjectoFinal_LR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly IAnimalsServices _animalsServices;
        private readonly IClientsServices _clientsServices;
        private readonly IAppointmentsServices _appointmentsServices;
        private readonly IVeterinariansServices _veterinariansServices;
        private readonly IMotivesServices _motivesServices;
        private readonly IPrioritiesServices _prioritiesServices;
        private readonly INotyfService _notyfService;

        public HomeController(ILogger<HomeController> logger, IAnimalsServices animalsServices,
            IClientsServices clientsServices , IAppointmentsServices appointmentsServices,
            IVeterinariansServices veterinariansServices, IMotivesServices motivesServices,
            IPrioritiesServices prioritiesServices, INotyfService notyfService)
        {
            _logger = logger;
            _animalsServices = animalsServices;
            _clientsServices = clientsServices;
            _appointmentsServices = appointmentsServices;
            _veterinariansServices = veterinariansServices;
            _motivesServices = motivesServices;
            _prioritiesServices = prioritiesServices;
            _notyfService = notyfService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.CountAnimals = _animalsServices.GetList().Count();
            ViewBag.CountClients = _clientsServices.GetList().Count();
            ViewBag.CountAppointments = _appointmentsServices.GetList().Result.Count();
            ViewBag.CountAppointmentsByDay = _appointmentsServices.GetListByDay().Count();

            ViewBag.IdAnimal = _animalsServices.SelectAnimals();
            ViewBag.IdVeterinarian = _veterinariansServices.SelectVeterinarians();
            ViewBag.IdMotive = _motivesServices.SelectMotives();
            ViewBag.IdPriority = _prioritiesServices.SelectPriorities();

            var list =  _appointmentsServices.GetListByDay();

            return View(list);
        }

        public JsonResult ErrorJSON()
        {
            // Retorna um JsonResult similar ao BadRequest() do ActionResult - Erro 400
            return new JsonResult(new { message = "Mensagem de erro" })
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }

        [HttpGet]
        public async Task<JsonResult> GetEvents()
        {
            var events = await _appointmentsServices.GetEvents();

            return Json(new { result = events });

        }

        [HttpPost]
        public async Task<JsonResult> SaveEvent(Appointment appointment)
        {
            var status = false;

            if (appointment == null)
            {
                return ErrorJSON();
            }


            if (await _appointmentsServices.SaveEvent(appointment))
            {
                _notyfService.Success("Consulta criada com sucesso!");
                status = true;
            }
            else
            {
                _notyfService.Error("Erro ao criar a consulta!");
                ViewData["Message"] = "Horário ocupado!";
            }

            return Json(new { status, appointment});
        }

        [HttpPost]
        public JsonResult GetListByDay()
        {

            var list = _appointmentsServices.GetListByDay();

            return Json(new { list });
        }


        [HttpPost]
        public async Task<JsonResult> DeleteEvent(int idAppointment)
        {
            var status = false;

            if (idAppointment == 0)
            {
                return ErrorJSON();
            }

            if (await _appointmentsServices.Remove(idAppointment))
            {
                status = true;
            }
            return Json(new { status });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}