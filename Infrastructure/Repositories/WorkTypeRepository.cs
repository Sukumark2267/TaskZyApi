using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class WorkTypeRepository : IWorkTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkType>> GetAllAsync()
        {
            return await _context.WorkTypes.ToListAsync();
        }

        public async Task<WorkType> GetByIdAsync(int id)
        {
            return await _context.WorkTypes.FindAsync(id);
        }

        public async Task AddAsync(WorkType workType)
        {
            await _context.WorkTypes.AddAsync(workType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(WorkType workType)
        {
            _context.WorkTypes.Update(workType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var workType = await _context.WorkTypes.FindAsync(id);
            if (workType != null)
            {
                _context.WorkTypes.Remove(workType);
                await _context.SaveChangesAsync();
            }
        }
    }
}
