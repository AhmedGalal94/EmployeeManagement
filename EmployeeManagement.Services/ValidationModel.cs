using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Services
{
    public class ValidationModel
    {
        private List<FieldError> _fieldsErrors;

        internal void AddError(string fieldName, string Message)
        {
            FieldsErrors.Add(new FieldError { FieldName = fieldName, Message = Message });
        }

        public bool isValid
        {
            get
            {
                if (_fieldsErrors == null || !_fieldsErrors.Any())
                    return true;

                return false;
            }
        }

        public List<FieldError> FieldsErrors
        {
            get
            {
                return _fieldsErrors == null ? _fieldsErrors = new List<FieldError>() : _fieldsErrors;
            }
        }


    }
    public class FieldError
    {
        public string FieldName { get; internal set; }
        public string Message { get; internal set; }
    }
}
