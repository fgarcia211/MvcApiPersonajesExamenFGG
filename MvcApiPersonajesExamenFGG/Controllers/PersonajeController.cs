using Microsoft.AspNetCore.Mvc;
using MvcApiPersonajesExamenFGG.Models;
using MvcApiPersonajesExamenFGG.Services;

namespace MvcApiPersonajesExamenFGG.Controllers
{
    public class PersonajeController : Controller
    {
        private ServiceApiPersonajes service;

        public PersonajeController(ServiceApiPersonajes service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await this.service.GetPersonajesAPI());
        }

        [HttpPost]
        public async Task<IActionResult> Index(int idserie)
        {
            return View(await this.service.GetPersonajesSerieAPI(idserie));
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await this.service.FindPersonajeAPI(id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Personaje personaje)
        {
            await this.service.PostPersonajeAPI(personaje);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(await this.service.FindPersonajeAPI(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Personaje personaje)
        {
            await this.service.PutPersonajeAPI(personaje);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeletePersonajeAPI(id);
            return RedirectToAction("Index");
        }
    }
}
