using System;
using System.Collections.Generic;
using EmployeeManagement.Core;
using EmployeeManagement.Services.DTOs;
using EmployeeManagement.Services.Mappers;

namespace EmployeeManagement.Services
{
    public class CitiesService : BaseService
    {
        public ServiceResponse AddCity(CityDto model)
        {
            try
            {
                ServiceResponse response = new ServiceResponse();
                response.validations = ValidateCity(model);

                if (response.validations.isValid)
                {
                    var newCity = model.toEntity();
                    UnitOfWork.Repository<City>().Add(newCity);
                    UnitOfWork.SaveChanges();

                    response.Status = Enums.ServiceResponseStatus.Success;
                    response.AlteredId = newCity.Id;
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

        public ServiceResponse UpdateCity(int Id, CityDto model)
        {
            try
            {
                ServiceResponse response = new ServiceResponse();
                var City = UnitOfWork.Repository<City>().GetSingle(x => x.Id == Id);
                if (City == null)
                {
                    response.Status = Enums.ServiceResponseStatus.Fail;
                    response.Message = "City doesn't Exist";
                    return response;
                }
                model.Id = Id;
                response.validations = ValidateCity(model);
                if (response.validations.isValid)
                {
                    UnitOfWork.Repository<City>().Update(model.toEntity(City));
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

        public ServiceResponse DeleteCities(int[] Ids)
        {
            try
            {
                ServiceResponse response = new ServiceResponse();

                foreach (var id in Ids)
                {
                    var entity = UnitOfWork.Repository<City>().GetSingle(x => x.Id == id);

                    if (entity == null)
                    {
                        response.Status = Enums.ServiceResponseStatus.Fail;
                        response.Message = $"City With Id :'{id}' not Exist ";
                        return response;
                    }
                    UnitOfWork.Repository<City>().Remove(entity);
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

        public CityDto GetCity(int Id)
        {
            try
            {
                return UnitOfWork.Repository<City>().GetSingle(x => x.Id == Id,x=>x.Country).toDTO();
            }
            finally
            {
                UnitOfWork.Dispose();
            }
        }

        public List<CityDto> GetAllCities()
        {
            try
            {
                return UnitOfWork.Repository<City>().GetAll(null,x=>x.Country).toDTO();
            }
            finally
            {
                UnitOfWork.Dispose();
            }
        }

        public List<CityDto> GetCities(int CountryId)
        {
            try
            {
                return UnitOfWork.Repository<City>().GetAll(x=>x.CountryId == CountryId).toDTO();
            }
            finally
            {
                UnitOfWork.Dispose();
            }
        }

        private ValidationModel ValidateCity(CityDto model)
        {
            ValidationModel validations = new ValidationModel();

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                validations.AddError("Name", "Name Field is required");
            }
            else if (UnitOfWork.Repository<City>().Any(x=>x.Id !=model.Id && x.Name.ToLower().Trim() == model.Name.ToLower().Trim() && x.CountryId == model.Country))
            {
                validations.AddError("", "City already Exist");
            }

            if (model.Country == 0)
            {
                validations.AddError("Country", "Country is required");
            }
            else if (!UnitOfWork.Repository<City>().Any(x => x.Id == model.Country))
            {
                validations.AddError("Country", "Country doesn't Exist");
            }
            return validations;
        }
    }
}
