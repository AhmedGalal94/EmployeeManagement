using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Services.DTOs
{
    public class CountryDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
