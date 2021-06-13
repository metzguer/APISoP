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
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApiSoPDbContext _context;
        public ProfileRepository(ApiSoPDbContext context)
        {
            _context = context;
        }
        public async Task Add(Profile entity)
        {
            await _context.Profiles.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Profile>> GetAll(Guid id)
        {
            return await _context.Profiles.Where(x => x.StoreId == id).ToListAsync() ;
        }

        public async Task<Profile> GetById(Guid guid)
        {
            return await _context.Profiles.FirstOrDefaultAsync( x=> x.ProfileId == guid );
        }

        public async Task Remove(Guid guid)
        {
            var entity = await GetById(guid);
            _context.Profiles.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Profile entity)
        {
            _context.Profiles.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
