using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IConfirmRepository
    {
        Task<bool> ConfirmWork(int workId, int userId);
    }

}
