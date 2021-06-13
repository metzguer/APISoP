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
    public class CashRegisterRepository : ICashRegisterRepository
    {
        private readonly ApiSoPDbContext _context;
        public CashRegisterRepository(ApiSoPDbContext context)
        {
            _context = context;
        }

        public async Task Add(CashRegister entity)
        {
            await _context.CashRegisters.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CashRegister>> GetAll(Guid id)
        {
            return await _context.CashRegisters.Where(x => x.StoreId == id).ToListAsync();
        }

        public async Task<CashRegister> GetById(Guid guid)
        {
            return await _context.CashRegisters.FirstOrDefaultAsync(x => x.CashRegisterId == guid);
        }

        public async Task Remove(Guid guid)
        {
            var entity = await GetById(guid);
            _context.CashRegisters.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CashRegister entity)
        {
            _context.CashRegisters.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
