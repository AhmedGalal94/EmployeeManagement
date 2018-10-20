using EmployeeManagement.Core;
using EmployeeManagement.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Services.Mappers
{
    public static class CityMapper
    {
        public static CityDto toDTO(this City entity)
        {
            if(entity != null)
            {
                return new CityDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Country = entity.CountryId,
                    CountryDto = entity.Country.toDTO()
                };
            }
            return null;
        }

        public static City toEntity(this CityDto model, City entity = null)
        {
            if (model != null)
            {
                if(entity == null)
                return new City
                {
                    Id = model.Id,
                    Name = model.Name,
                    CountryId = model.Country
                };

                entity.Id = model.Id;
                entity.Name = model.Name;
                entity.CountryId = model.Country;
                return entity;
            }
            return null;
        }

        public static List<CityDto> toDTO(this List<City> entities)
        {
            if (entities != null)
            {
                List<CityDto> models = new List<CityDto>();
                foreach(var entity in entities)
                {
                    models.Add(entity.toDTO());
                }
                return models;
            }
            return null;
        }

    }
}
