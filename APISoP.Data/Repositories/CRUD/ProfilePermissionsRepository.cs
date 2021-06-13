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
    public class ProfilePermissionsRepository : IProfilePermissionRepository
    {
        private readonly ApiSoPDbContext _context;
        public ProfilePermissionsRepository(ApiSoPDbContext context)
        {
            _context = context;
        }
        public async Task Add(ProfilePermissions entity)
        {
            await _context.ProfilePermissions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProfilePermissions>> GetAll(Guid id)
        {
            return await _context.ProfilePermissions.Where(x => x.ProfileId == id).ToListAsync();
        }

        public async Task<ProfilePermissions> GetById(Guid guid)
        {
            return await _context.ProfilePermissions.FirstOrDefaultAsync(x => x.ProfilePermissionsId == guid);
        }

        public async Task Remove(Guid guid)
        {
            var entity = await GetById(guid);
            _context.ProfilePermissions.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ProfilePermissions entity)
        {
            _context.ProfilePermissions.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
