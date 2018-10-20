using EmployeeManagement.Services;
using EmployeeManagement.Services.DTOs;
using EmployeeManagement.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Web.Controllers
{
    public class CitiesController : Controller
    {
        CitiesService service;
        CountriesService countriesService;

        public CitiesController()
        {
            service = new CitiesService();
            countriesService = new CountriesService();
        }

        public ActionResult Index()
        {
            return View(service.GetAllCities());
        }

        // GET: Cities/Create
        public ActionResult Create()
        {
            var countries = countriesService.GetAllCountries().Select(x => new { Id = x.Id, Name = x.Name });
            ViewBag.Countries = new SelectList(countries, "Id", "Name");
            return View();
        }

        // POST: Cities/Create
        [HttpPost]
        public ActionResult Create(CityDto model)
        {
            if (ModelState.IsValid)
            {
                var response = service.AddCity(model);
                if (response.Status == ServiceResponseStatus.Success)
                    return RedirectToAction("Index");

                if (response.Status == ServiceResponseStatus.Fail)
                    ModelState.AddModelError("", response.Message);
                else
                {
                    foreach (var FieldError in response.validations.FieldsErrors)
                        ModelState.AddModelError(FieldError.FieldName, FieldError.Message);
                }
            }
            var countries = countriesService.GetAllCountries().Select(x => new { Id = x.Id, Name = x.Name });
            ViewBag.Countries = new SelectList(countries, "Id", "Name");
            return View(model);
        }

        // GET: Cities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                var model = service.GetCity(id.Value);
                if (model != null)
                {
                    var countries = countriesService.GetAllCountries().Select(x => new { Id = x.Id, Name = x.Name });
                    ViewBag.Countries = new SelectList(countries, "Id", "Name");
                    return View(model);
                }
            }
            return RedirectToAction("Index");
        }

        // POST: Cities/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CityDto model)
        {
            if (ModelState.IsValid)
            {
                var response = service.UpdateCity(id, model);
                if (response.Status == ServiceResponseStatus.Success)
                    return RedirectToAction("Index");

                if (response.Status == ServiceResponseStatus.Fail)
                    ModelState.AddModelError("", response.Message);
                else
                {
                    foreach (var FieldError in response.validations.FieldsErrors)
                        ModelState.AddModelError(FieldError.FieldName, FieldError.Message);
                }
            }
            var countries = countriesService.GetAllCountries().Select(x => new { Id = x.Id, Name = x.Name });
            ViewBag.Countries = new SelectList(countries, "Id", "Name");
            return View(model);
        }


        // POST: Cities/Delete/5
        [HttpPost]
        public JsonResult Delete(int[] ids)
        {
            if (ids != null)
            {
                var response = service.DeleteCities(ids);
                if (response.Status == ServiceResponseStatus.Success)
                    return Json(new { success = true });


                return Json(new { success = false, responseText = response.Message });

            }
            return Json(new { success = false, responseText = "select items to Delete" });
        }


        public JsonResult GetCities(int CountryId)
        {
            var Cities = service.GetCities(CountryId);
            return Json(new { success = true, cities = Cities.Select(x => new { Id = x.Id, Name = x.Name }) }, JsonRequestBehavior.AllowGet);
        }
    }
}
