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
    public class CountriesController : Controller
    {
        
        CountriesService service;

        public CountriesController()
        {
            service = new CountriesService();
        }

        public ActionResult Index()
        {
            return View(service.GetAllCountries());
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        [HttpPost]
        public ActionResult Create(CountryDto model)
        {
            if (ModelState.IsValid)
            {
                var response = service.AddCountry(model);
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
            return View(model);
        }

        // GET: Countries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                var model = service.GetCountry(id.Value);
                if (model != null)
                {
                    return View(model);
                }
            }
            return RedirectToAction("Index");
        }

        // POST: Countries/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CountryDto model)
        {
            if (ModelState.IsValid)
            {
                var response = service.UpdateCountry(id, model);
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
            return View(model);
        }


        // POST: Countries/Delete/5
        [HttpPost]
        public JsonResult Delete(int[] ids)
        {
            if (ids != null)
            {
                var response = service.DeleteCountries(ids);
                if (response.Status == ServiceResponseStatus.Success)
                    return Json(new { success = true });


                return Json(new { success = false, responseText = response.Message });

            }
            return Json(new { success = false, responseText = "select items to Delete" });
        }
    }
}
