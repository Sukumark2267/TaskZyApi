using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Mobile { get; set; }
        public string? Email { get; set; } // Optional field
        public string? Password { get; set; }
        public bool IsWorker { get; set; }
        public bool IsFarmer { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
        public string? DeviceToken { get; set; }
    }
}
