using APISoP.CrossCutting.Entities;
using APISoP.Data.Contracts.CRUD;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoP.Data.Repositories.CRUD
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApiSoPDbContext _context;
        public PermissionRepository(ApiSoPDbContext context)
        {
            _context = context;
        }
        public Task Add(Permission entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Permission>> GetAll(Guid id) => await _context.Permissions.Where(x=>x.IsActive).ToListAsync();

        public async Task<Permission> GetById(Guid guid) => await _context.Permissions.FirstOrDefaultAsync( x => x.PermissionId == guid);

        public Task Remove(Guid guid)
        {
            throw new NotImplementedException();
        }

        public Task Update(Permission entity)
        {
            throw new NotImplementedException();
        }
    }
}
