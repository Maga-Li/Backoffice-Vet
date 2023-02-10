using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectoFinal_LR.Models;
using ProjectoFinal_LR.Services.Animals;
using ProjectoFinal_LR.Services.Appointments;
using ProjectoFinal_LR.Services.Motives;
using ProjectoFinal_LR.Services.Priorities;
using ProjectoFinal_LR.Services.Veterinarians;

namespace ProjectoFinal_LR.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentsServices _appointmentsServices;
        private readonly IAnimalsServices _animalsServices;
        private readonly IVeterinariansServices _veterinariansServices;
        private readonly IMotivesServices _motivesServices;
        private readonly IPrioritiesServices _prioritiesServices;
        private readonly INotyfService _notyfService;

        public AppointmentController(IAppointmentsServices appointmentsServices,IAnimalsServices animalsServices, IVeterinariansServices veterinariansServices, IMotivesServices motivesServices, IPrioritiesServices prioritiesServices, INotyfService notyfService)
        {
            _appointmentsServices= appointmentsServices;
            _animalsServices = animalsServices;
            _veterinariansServices = veterinariansServices;
            _motivesServices = motivesServices;
            _prioritiesServices = prioritiesServices;
            _notyfService = notyfService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var list = await _appointmentsServices.GetList();

            if(list == null)
            {
                return NotFound();
            }

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int idAppointment)
        {
            var appointment = await _appointmentsServices.GetOne(idAppointment);

            if(appointment == null)

            {
                return NotFound();
            }

            return View(appointment);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var appointment = new Appointment();

            ViewBag.IdAnimal = _animalsServices.SelectAnimals();
            ViewBag.IdVeterinarian = _veterinariansServices.SelectVeterinarians();
            ViewBag.IdMotive = _motivesServices.SelectMotives();
            ViewBag.IdPriority = _prioritiesServices.SelectPriorities();

            return View(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            if (appointment == null)
            {
                return BadRequest();
            }

            if(await _appointmentsServices.Add(appointment))
            {
                _notyfService.Success("Consulta criada com sucesso!");
                return RedirectToAction(nameof(List));
            }
            else
            {
                _notyfService.Error("Erro ao criar a consulta!");
                ViewData["Message"] = "Horário ocupado!";
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int idAppointment)
        {
            if(idAppointment == 0)
            {
                return NotFound();
            }

            var appointment = await _appointmentsServices.GetOne(idAppointment);

            if(appointment == null)
            {
                return NotFound();
            }

            ViewBag.IdAnimal = _animalsServices.SelectAnimals();
            ViewBag.IdVeterinarian = _veterinariansServices.SelectVeterinarians();
            ViewBag.IdMotive = _motivesServices.SelectMotives();
            ViewBag.IdPriority = _prioritiesServices.SelectPriorities();

			return View(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Appointment appointment)
        {
            if(await _appointmentsServices.Update(appointment))
            {
                _notyfService.Success("Consulta editada com sucesso!");
                return RedirectToAction(nameof(List));

            }else
            {
                _notyfService.Error("Erro ao editar a consulta!");
                ViewData["Message"] = "Horário ocupado!";
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int idAppointment)
        {
            if(await _appointmentsServices.Remove(idAppointment))
            {
                return RedirectToAction(nameof(List));
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string name)
        {
            var appointment = await _appointmentsServices.Filter(name);

            if(appointment == null)
            {
                return NotFound();
            }

            return View("List", appointment);
        }

    }
}
