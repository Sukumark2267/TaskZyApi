using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Work
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string WorkName { get; set; } = string.Empty;  // ✅ Initialized

        [Required]
        public string Place { get; set; } = string.Empty;     // ✅ Initialized

        public string? Time { get; set; }
        public int? NoOfPeople { get; set; }

        [Required]
        [Phone]
        public string ContactNumber { get; set; } = string.Empty; // ✅ Initialized

        public string? ImagePath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsConfirmed { get; set; } = false;  // ✅ Default for bool

        public int? ConfirmedBy { get; set; }
        public int? PostedBy { get; set; }

        // Navigation
        public User? PostedUser { get; set; }
        public User? ConfirmedUser { get; set; }
    }


}
