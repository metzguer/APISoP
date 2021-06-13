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
    public class StoreRepository : IStoreRepository
    {
        private readonly ApiSoPDbContext _context;
        public StoreRepository(ApiSoPDbContext context)
        {
            _context = context;
        }
        public async Task Add(Store entity)
        {
            await _context.Stores.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Store>> GetAll(Guid EnterpriseId)
        {
            return await _context.Stores.Where( x => x.EnterpriseId == EnterpriseId).ToListAsync();
        }

        public async Task<Store> GetById(Guid guid)
        {
            return await _context.Stores.FirstOrDefaultAsync(x => x.EnterpriseId == guid);
        }

        public async Task Remove(Guid guid)
        {
            var entity = await GetById(guid);
            _context.Stores.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Store entity)
        {
            _context.Stores.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
