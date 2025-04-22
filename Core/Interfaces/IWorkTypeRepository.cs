using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IWorkTypeRepository
    {
        Task<IEnumerable<WorkType>> GetAllAsync();
        Task<WorkType> GetByIdAsync(int id);
        Task AddAsync(WorkType workType);
        Task UpdateAsync(WorkType workType);
        Task DeleteAsync(int id);
    }
}
