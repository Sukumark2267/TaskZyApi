using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;  // ✅ initialized

        [Required]
        public string Mobile { get; set; } = string.Empty;    // ✅ initialized

        public string? Email { get; set; }
        public string? Password { get; set; }

        public bool IsWorker { get; set; } = false;
        public bool IsFarmer { get; set; } = false;

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
        public string? DeviceToken { get; set; }

        // Navigation (keep commented if not used)
        // public ICollection<Work> PostedWorks { get; set; } = new List<Work>();
        // public ICollection<WorkConfirm> ConfirmedWorks { get; set; } = new List<WorkConfirm>();
    }


}
