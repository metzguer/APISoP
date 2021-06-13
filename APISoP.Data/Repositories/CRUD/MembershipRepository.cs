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
    public class MembershipRepository : IMembershipRepository
    {
        private readonly ApiSoPDbContext _context;
        public MembershipRepository(ApiSoPDbContext context)
        {
            _context = context;
        }
        public async Task Add(Membership entity)
        {
            await _context.Memberships.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Membership>> GetAll(Guid id)
        {
            return await _context.Memberships.Where(x => x.MembershipId == id).ToListAsync();
        }

        public async Task<Membership> GetById(Guid guid)
        {
            return await _context.Memberships.FirstOrDefaultAsync(x => x.MembershipId == guid);
        }

        public async Task Remove(Guid guid)
        {
            var entity = await GetById(guid);
            _context.Memberships.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Membership entity)
        {
            _context.Memberships.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
