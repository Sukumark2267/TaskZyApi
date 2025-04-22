using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface INotificationService
    {
        Task<bool> SendPushNotification(string deviceToken, string title, string body, string type, string workId);
    }
}
