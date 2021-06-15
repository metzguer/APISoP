using APISoP.CrossCutting.Entities;
using APISoP.Data.Contracts.CRUD;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APISoP.Data.Repositories.CRUD
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiSoPDbContext _context;
        public UserRepository(ApiSoPDbContext context)
        {
            _context = context;
        }

        public async Task Add(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAll(Guid id)
        {
            return await _context.Users.Where(x => x.StoreId == id).ToListAsync();
        }

        public async Task<User> GetById(Guid guid)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserId == guid);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<User> GetUserForLogin(Guid guid) => await _context.Users
            .Include( s => s.Store)
            .ThenInclude(e=>e.Enterprise)
            .Include( p => p.Profile)
            .FirstOrDefaultAsync(x => x.UserId == guid);
       

        public async Task Remove(Guid guid)
        {
            var entity = await GetById(guid);
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
