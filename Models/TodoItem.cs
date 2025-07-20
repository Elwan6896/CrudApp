using System.ComponentModel.DataAnnotations;

namespace CrudApp.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Title { get; set; }

        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
