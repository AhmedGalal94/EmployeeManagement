using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core
{
    public class UnitOfWork :IDisposable
    {
        private dbContext _context;
        private bool disposed;

        public bool isDisposed { get { return disposed; } }

        public UnitOfWork(dbContext dbContext)
        {
            _context = dbContext;
        }
        public UnitOfWork()
        {
            _context = new dbContext();
        }

        public GenericRepository<T> Repository<T>() where T : class
        {
            GenericRepository<T> repo = new GenericRepository<T>(_context);
            return repo;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
