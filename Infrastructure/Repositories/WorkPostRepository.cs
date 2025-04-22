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
    public class WorkPostRepository : IWorkPostRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkPostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Work>> GetAllWorkPostsAsync()
        {
            return await _context.Work.ToListAsync();
        }

        public async Task<Work> GetWorkPostByIdAsync(int id)
        {
            return await _context.Work.FindAsync(id);
        }

        public async Task AddWorkPostAsync(Work workPost)
        {
            _context.Work.Add(workPost);
            await _context.SaveChangesAsync();
        }
    }
}
