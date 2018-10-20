using EmployeeManagement.Core;
using EmployeeManagement.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.Services.Mappers;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Services
{
    public class EmployeesService : BaseService
    {
        public ServiceResponse AddEmployee(EmployeeDto model)
        {
            try
            {
                ServiceResponse response = new ServiceResponse();
                response.validations = ValidateEmployee(model);

                if (response.validations.isValid)
                {
                    var newEmployee = model.toEntity();
                    UnitOfWork.Repository<Employee>().Add(newEmployee);
                    UnitOfWork.SaveChanges();

                    response.Status = Enums.ServiceResponseStatus.Success;
                    response.AlteredId = newEmployee.Id;
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

        public ServiceResponse UpdateEmployee(int Id,EmployeeDto model)
        {
            try
            {
                ServiceResponse response = new ServiceResponse();
                var employee = UnitOfWork.Repository<Employee>().GetSingle(x => x.Id == Id);
                if (employee == null)
                {
                    response.Status = Enums.ServiceResponseStatus.Fail;
                    response.Message = "Employee doesn't Exist";
                    return response;
                }
                model.Id = Id;
                response.validations = ValidateEmployee(model);
                if (response.validations.isValid)
                {
                    UnitOfWork.Repository<Employee>().Update(model.toEntity());
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

        public ServiceResponse DeleteEmployees(int[] Ids)
        {
            try
            {
                ServiceResponse response = new ServiceResponse();
                foreach (var id in Ids)
                {
                    var entity = UnitOfWork.Repository<Employee>().GetSingle(x => x.Id == id);

                    if (entity == null)
                    {
                        response.Status = Enums.ServiceResponseStatus.Fail;
                        response.Message = $"Employee With Id :'{id}' not Exist ";
                        return response;
                    }
                    UnitOfWork.Repository<Employee>().Remove(entity);
                        
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

        public EmployeeDto GetEmployee(int Id)
        {
            try
            {
                return UnitOfWork.Repository<Employee>().GetSingle(x => x.Id == Id).toDTO();
            }
            finally
            {
                UnitOfWork.Dispose();
            }
        }

        public List<EmployeeDto> GetAllEmployees()
        {
            try
            {
                return UnitOfWork.Repository<Employee>().GetAll().toDTO();
            }
            finally
            {
                UnitOfWork.Dispose();
            }
        }

        private ValidationModel ValidateEmployee(EmployeeDto model)
        {
            ValidationModel validations = new ValidationModel();

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                validations.AddError("Name", "Name Field is required");
            }
            if (model.Salary <= 0)
            {
                validations.AddError("Salary", "invaild Salary");
            }

            if (model.City == 0)
            {
                validations.AddError("City", "City is required");
            }
            else if (UnitOfWork.Repository<City>().Any(x => x.Id == model.City))
            {
                validations.AddError("City", "City doesn't Exist");
            }


            if (string.IsNullOrWhiteSpace(model.Email))
            {
                validations.AddError("City", "Email Field is required");
            }
            if (new EmailAddressAttribute().IsValid(model.Email))
            {
                validations.AddError("City", "Invaild Email");
            }
            else if (UnitOfWork.Repository<Employee>().Any(x => x.Id != model.Id && x.Email == model.Email))
            {
                validations.AddError("City", "This Email  already Exist");
            }
            return validations;
        }
    }
}
