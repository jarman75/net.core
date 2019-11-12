using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Application.Services;
using User.DataAccess.Data;

namespace User.DataAccess
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        private readonly ApplicationDbContext _context;
        private bool _disponsed = false;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disponsing)
        {
            if (!_disponsed)
            {
                if (disponsing)
                {
                    _context.Dispose();
                }
                _disponsed = true;
            }
        }

        public async Task<int> Save()
        {
            int affectedRows = await _context.SaveChangesAsync();
            return affectedRows;
        }
    }
}
