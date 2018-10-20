using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.Core;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Services.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Range(1.0, double.MaxValue ,ErrorMessage ="Invalid Salary")]
        public double Salary { get; set; }

        [Required]
        public int Country { get; set; }

        [Required]
        public int City { get; set; }

        public virtual CityDto CityDto { get; set; }
    }
}
