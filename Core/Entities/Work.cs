using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Work
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string WorkName { get; set; }

        [Required]
        public string Place { get; set; }

        public string? Time { get; set; } // Optional

        public int? NoOfPeople { get; set; } //optional

        [Required]
        [Phone]
        public string ContactNumber { get; set; } // New field

        public string? ImagePath { get; set; } // Optional

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool? IsConfirmed { get; set; } = false;
        public int? ConfirmedBy { get; set; }

        public int? postedBy { get; set; }
    }
}
