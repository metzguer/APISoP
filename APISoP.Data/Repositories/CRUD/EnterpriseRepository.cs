using APISoP.CrossCutting.Entities;
using APISoP.Data.Contracts.CRUD;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APISoP.Data.Repositories.CRUD
{
    public class EnterpriseRepository : IEnterpriseRepository
    {
        private readonly ApiSoPDbContext _context;
        public EnterpriseRepository(ApiSoPDbContext context)
        {
            _context = context;
        }

        public async Task Add(Enterprise entity)
        {
            await _context.Enterprises.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Enterprise>> GetAll(Guid id)
        {
            return await _context.Enterprises.Where( x => x.MembershipId == id).ToListAsync();
        }

        public async Task<Enterprise> GetById(Guid guid)
        {
            return await _context.Enterprises.FirstOrDefaultAsync( x => x.EnterpriseId == guid);
        }

        public async Task<Enterprise> GetEnterpriseAndStores(Guid id)
        {
            return await _context.Enterprises.Include( s => s.Stores).FirstOrDefaultAsync(x => x.EnterpriseId == id);
        }

        public async Task Remove(Guid guid)
        {
            var entity = await GetById(guid);
            _context.Enterprises.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Enterprise entity)
        {
            _context.Enterprises.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
