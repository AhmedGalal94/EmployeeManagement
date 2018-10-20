using EmployeeManagement.Core;
using EmployeeManagement.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Services.Mappers
{
    public static class CountryMapper
    {
        public static CountryDto toDTO(this Country entity)
        {
            if (entity != null)
            {
                return new CountryDto
                {
                    Id = entity.Id,
                    Name = entity.Name
                };
            }
            return null;
        }

        public static Country toEntity(this CountryDto model)
        {
            if (model != null)
            {
                return new Country
                {
                    Id = model.Id,
                    Name = model.Name
                };
            }
            return null;
        }

        public static List<CountryDto> toDTO(this List<Country> entities)
        {
            if (entities != null)
            {
                List<CountryDto> models = new List<CountryDto>();
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
