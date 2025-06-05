using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class WorkConfirm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int WorkId { get; set; }
        public int ConfirmedBy { get; set; }
        public DateTime ConfirmedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Work? Work { get; set; }
        public User? ConfirmedUser { get; set; }
    }


}
