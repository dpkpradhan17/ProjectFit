﻿using System.ComponentModel.DataAnnotations;

namespace ProjectFit.Entities
{
    public class Coaches
    {
        [Key]
        public int CoachId { get; set; }
        [Required]
        public string CoachName { get; set;}
        [Required]
        public string CoachType { get; set;}
        [Required]
        public string CoachEmail { get; set;}
        [Required]
        public int CountryCode { get; set;}
        [Required]
        [Phone]
        public string CoachPhone { get; set;}
        public string CoachRating { get; set;}

    }
}
