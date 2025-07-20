using System;
using System.ComponentModel.DataAnnotations;

namespace CrudApp.Models
{
    public class Info
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [Phone]
        public required string Phone { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        public required string Country { get; set; }

        [Display(Name = "Subscribed")]
        public bool IsSubscribed { get; set; }

    }
}
