using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Services.DTOs
{
    public class CityDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public int Country { get; set; }

        public virtual CountryDto CountryDto { get; set; }
    }
}
