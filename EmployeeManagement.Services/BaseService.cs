using EmployeeManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Services
{
    public class BaseService
    {
        private UnitOfWork _unitOfWork;
        protected UnitOfWork UnitOfWork => (_unitOfWork == null || _unitOfWork.isDisposed) ? (_unitOfWork = new UnitOfWork()) : (_unitOfWork);

        public BaseService()
        {
            _unitOfWork = new UnitOfWork();
        }
    }
}
