using EmployeeManagement.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Services
{
    public class ServiceResponse
    {
        public ServiceResponseStatus Status { get; internal set; }
        public string Message { get; internal set; }
        public ValidationModel validations { get; internal set; }
        public object AlteredId { get; internal set; }
    }
}
