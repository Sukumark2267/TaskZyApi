﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class WorkType
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }  

        [MaxLength(255)]
        public string ImageUrl { get; set; } 
        public bool IsActive { get; set; } = true;  

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int CategoryId { get; set; } = 1;
    }
}
