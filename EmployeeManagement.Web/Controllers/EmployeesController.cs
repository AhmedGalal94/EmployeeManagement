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
    public class EmployeesController : Controller
    {
        EmployeesService service;
        CountriesService countriesService;

        public EmployeesController()
        {
            service = new EmployeesService();
            countriesService = new CountriesService();
        }


        // GET: Employees
        public ActionResult Index()
        {
            return View(service.GetAllEmployees());
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            var countries = countriesService.GetAllCountries().Select(x => new { Id = x.Id, Name = x.Name });
            ViewBag.Countries = new SelectList(countries, "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        public ActionResult Create(EmployeeDto model)
        {
            if(ModelState.IsValid)
            {
                var response = service.AddEmployee(model);
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
            ViewBag.selectedCity = model.City;
            return View(model);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                var model = service.GetEmployee(id.Value);
                if (model != null)
                {
                    var countries = countriesService.GetAllCountries().Select(x => new { Id = x.Id, Name = x.Name });
                    ViewBag.Countries = new SelectList(countries, "Id", "Name");
                    ViewBag.selectedCity = model.City;
                    return View(model);
                }
            }
            return RedirectToAction("Index");
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var response = service.UpdateEmployee(id,model);
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
            ViewBag.selectedCity = model.City;
            return View(model);
        }


        // POST: Employees/Delete/5
        [HttpPost]
        public JsonResult Delete(int[] ids)
        {
            if(ids !=null)
            {
                var response = service.DeleteEmployees(ids);
                if (response.Status == ServiceResponseStatus.Success)
                    return Json(new { success = true});


                return Json(new { success = false , responseText = response.Message});

            }
            return Json(new { success = false, responseText = "select items to Delete" });
        }
    }
}
