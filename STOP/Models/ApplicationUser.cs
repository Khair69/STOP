﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace STOP.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 2)]
        [PersonalData]
        public string? FirstName { get; set; }
        [Required]
        [PersonalData]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 2)]
        public string? LastName { get; set; }
        [Required]
        [PersonalData]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
    }
}
