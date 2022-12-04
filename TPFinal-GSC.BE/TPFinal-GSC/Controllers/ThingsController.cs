using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TPFinal_GSC.DataAccess.Interfaces;
using TPFinal_GSC.Entities;
using TPFinal_GSC.Models;

namespace TPFinal_GSC.Controllers
{
    public class ThingsController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public ThingsController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var things = uow.ThingRepository.GetAll();
            var thingsViewModel = mapper.Map<List<ThingViewModel>>(things);
            return View(thingsViewModel);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = uow.CategoryRepository.GetAllAsSelectList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ThingViewModel thingViewModel)
        {
            ViewBag.Categories = uow.CategoryRepository.GetAllAsSelectList();

            if (!ModelState.IsValid)
                return View("Create", thingViewModel);

            if (uow.ThingRepository.ExistsDescription(thingViewModel.Description))
            {
                ModelState.AddModelError(String.Empty, "Ya existe una cosa con esta descripción.");
                return View(thingViewModel);
            };

            var category = uow.CategoryRepository.GetById(thingViewModel.CategoryId);
            if (category is null)
            {
                ModelState.AddModelError(String.Empty, "No existe la categoria seleccionada.");
                return View(thingViewModel);
            }

            var thing = mapper.Map<Thing>(thingViewModel);
            uow.ThingRepository.Add(thing);
            uow.Complete();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id is null)
                return NotFound();

            ViewBag.Categories = uow.CategoryRepository.GetAllAsSelectList(); ;

            var thing = uow.ThingRepository.GetById(id.Value);
            if (thing is null)
                return NotFound();

            var thingViewModel = mapper.Map<ThingViewModel>(thing);

            return View(thingViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ThingViewModel thingViewModel)
        {
            ViewBag.Categories = uow.CategoryRepository.GetAllAsSelectList();

            if (id != thingViewModel.Id)
                return NotFound();

            var category = uow.CategoryRepository.GetById(thingViewModel.CategoryId);
            if (category is null)
            {
                ModelState.AddModelError(String.Empty, "No existe la categoria seleccionada.");
                return View(thingViewModel);
            }

            if (ModelState.IsValid)
            {
                var thing = mapper.Map<Thing>(thingViewModel);
                uow.ThingRepository.Update(thing);
                uow.Complete();

                return RedirectToAction(nameof(Index));
            }

            return View(thingViewModel);
        }

        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var thing = uow.ThingRepository.GetById(id.Value);
            if (thing is null)
                return NotFound();

            var thingsViewModel = mapper.Map<ThingViewModel>(thing);
            return View(thingsViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var deleted = uow.ThingRepository.Delete(id);
            uow.Complete();
            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));                
        }
    }
}
