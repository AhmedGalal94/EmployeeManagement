using EmployeeManagement.Core;
using EmployeeManagement.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Services.Mappers
{
    public static class  EmployeeMapper
    {
        public static EmployeeDto toDTO(this Employee entity)
        {
            if (entity != null)
            {
                return new EmployeeDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    City = entity.CityId,
                    Country = entity.City!=null? entity.City.CountryId:0,
                    Email = entity.Email,
                    Salary = entity.Salary,
                    CityDto = entity.City.toDTO()
                };
            }
            return null;
        }

        public static Employee toEntity(this EmployeeDto model,Employee entity = null)
        {
            if (model != null)
            {
                if(entity == null)
                return new Employee
                {
                    Id = model.Id,
                    Name = model.Name,
                    CityId = model.City,
                    Email = model.Email,
                    Salary = model.Salary
                };

                entity.Id = model.Id;
                entity.Name = model.Name;
                entity.CityId = model.City;
                entity.Email = model.Email;
                entity.Salary = model.Salary;
                return entity;

            }
            return null;
        }

        public static List<EmployeeDto> toDTO(this List<Employee> entities)
        {
            if (entities != null)
            {
                List<EmployeeDto> models = new List<EmployeeDto>();
                foreach (var entity in entities)
                {
                    models.Add(entity.toDTO());
                }
                return models;
            }
            return null;
        }
    }
}
