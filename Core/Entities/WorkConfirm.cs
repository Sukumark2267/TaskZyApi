using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class WorkConfirm
    {
        [Key]
        public int Id { get; set; }
        public int WorkId { get; set; } // FK to Work table
        public int ConfirmedBy { get; set; } // User who confirmed
        public DateTime ConfirmedAt { get; set; } = DateTime.UtcNow; // Auto timestamp
    }

}
