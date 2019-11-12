using System;
using System.Threading.Tasks;
using Application.Services;
using Insfrastructure.Data;

namespace User.DataAccess
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        private readonly IdentityContext _context;
        private bool _disponsed = false;

        public UnitOfWork(IdentityContext context)
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
