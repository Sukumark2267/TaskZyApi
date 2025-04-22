using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IWorkPostRepository
    {
        Task<IEnumerable<Work>> GetAllWorkPostsAsync();
        Task<Work> GetWorkPostByIdAsync(int id);
        Task AddWorkPostAsync(Work workPost);
    }
}
