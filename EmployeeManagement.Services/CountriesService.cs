using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.Services.DTOs;
using EmployeeManagement.Services.Mappers;
using EmployeeManagement.Core;

namespace EmployeeManagement.Services
{
    public class CountriesService :BaseService
    {
        public ServiceResponse AddCountry(CountryDto model)
        {
            try
            {
                ServiceResponse response = new ServiceResponse();
                response.validations = ValidateCountry(model);

                if (response.validations.isValid)
                {
                    var newCountry = model.toEntity();
                    UnitOfWork.Repository<Country>().Add(newCountry);
                    UnitOfWork.SaveChanges();

                    response.Status = Enums.ServiceResponseStatus.Success;
                    response.AlteredId = newCountry.Id;
                }
                else
                {
                    response.Status = Enums.ServiceResponseStatus.Invalid;
                }

                return response;
            }
            finally
            {
                UnitOfWork.Dispose();
            }
        }

        public ServiceResponse UpdateCountry(int Id, CountryDto model)
        {
            try
            {
                ServiceResponse response = new ServiceResponse();
                var Country = UnitOfWork.Repository<Country>().GetSingle(x => x.Id == Id);
                if (Country == null)
                {
                    response.Status = Enums.ServiceResponseStatus.Fail;
                    response.Message = "Country doesn't Exist";
                    return response;
                }
                model.Id = Id;
                response.validations = ValidateCountry(model);
                if (response.validations.isValid)
                {
                    UnitOfWork.Repository<Country>().Update(model.toEntity(Country));
                    UnitOfWork.SaveChanges();
                    response.Status = Enums.ServiceResponseStatus.Success;
                }
                else
                {
                    response.Status = Enums.ServiceResponseStatus.Invalid;
                }
                return response;
            }
            finally
            {
                UnitOfWork.Dispose();
            }
        }

        public ServiceResponse DeleteCountries(int[] Ids)
        {
            try
            {
                ServiceResponse response = new ServiceResponse();

                foreach (var id in Ids)
                {
                    var entity = UnitOfWork.Repository<Country>().GetSingle(x => x.Id == id);
                    if (entity == null)
                    {
                        response.Status = Enums.ServiceResponseStatus.Fail;
                        response.Message = $"Country With Id :'{id}'is not Exist";
                        return response;
                    }
                    if (entity != null)
                        UnitOfWork.Repository<Country>().Remove(entity);
                }
                UnitOfWork.SaveChanges();
                response.Status = Enums.ServiceResponseStatus.Success;
                return response;
            }
            finally
            {
                UnitOfWork.Dispose();
            }
        }

        public CountryDto GetCountry(int Id)
        {
            try
            {
                return UnitOfWork.Repository<Country>().GetSingle(x => x.Id == Id).toDTO();
            }
            finally
            {
                UnitOfWork.Dispose();
            }
        }

        public List<CountryDto> GetAllCountries()
        {
            try
            {
                return UnitOfWork.Repository<Country>().GetAll().toDTO();
            }
            finally
            {
                UnitOfWork.Dispose();
            }
        }

        private ValidationModel ValidateCountry(CountryDto model)
        {
            ValidationModel validations = new ValidationModel();

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                validations.AddError("Name", "Name Field is required");
            }
            else if (UnitOfWork.Repository<Country>().Any(x => x.Id != model.Id && x.Name.ToLower().Trim() == model.Name.ToLower().Trim()))
            {
                validations.AddError("", "Country already Exist");
            }
            return validations;
        }
    }
}
