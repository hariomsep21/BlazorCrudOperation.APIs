﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoCRUD.Model.Models
{
    [Index(nameof(UserName), IsUnique = true)]
    public class Students
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? Phone { get; set; }

        // Navigation properties
        public virtual Gender? Gender { get; set; }
        public virtual State? State { get; set; }
        public int? GenderID { get; set; }
        public int? StateID { get; set; }
        public string? GenderName => Gender?.GenderName;
        public string? StateName => State?.StateName;
       
      

      
    }
}
