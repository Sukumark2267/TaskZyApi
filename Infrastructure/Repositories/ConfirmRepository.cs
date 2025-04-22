using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ConfirmRepository : IConfirmRepository
    {
        private readonly ApplicationDbContext _context;

        public ConfirmRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ConfirmWork(int workId, int userId)
        {
            try
            {
                var work = await _context.Work.FindAsync(workId); // ✅ Corrected DbSet reference
                if (work == null)
                {
                    return false; // Work not found OR already confirmed
                }

                // Insert into WorkConfirm table
                var confirmation = new WorkConfirm
                {
                    WorkId = workId,
                    ConfirmedBy = userId,
                    ConfirmedAt = DateTime.UtcNow // ✅ Optional: Add timestamp
                };
                await _context.WorkConfirm.AddAsync(confirmation); // ✅ Fixed incorrect DbSet reference

                // Update Work table
                work.IsConfirmed = true;
                work.ConfirmedBy = userId;

                // Save all changes at once
                await _context.SaveChangesAsync();
                return true;
            }
          catch (Exception ex)
            {
                string message = ex.Message;
                return false;
            }
        }

    }

}
