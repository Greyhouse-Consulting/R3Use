using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication.ExtendedProtection;

namespace R3Use.Web.Models
{
    public class AssignmentModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime From { get; set; }

        public DateTime? To { get; set; }


        public string Description { get; set; } 

    }
}