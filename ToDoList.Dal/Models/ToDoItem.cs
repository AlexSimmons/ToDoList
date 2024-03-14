using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Core.Models
{
    public class ToDoItem
    {
        public ToDoItem()
        {
            Id = Guid.NewGuid();
            Title = string.Empty;
            CreatedAt = DateTime.UtcNow; // Initialize with the current UTC time
        }


        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Limited to 255 characters for data consistency and design limitations
        /// </summary>
        [Required]
        [StringLength(255, ErrorMessage = "Title length can't be more than 255 characters.")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the optional description of the task. Can be null.
        /// </summary>
        public string? Description { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Timestamp for the last modification. Can be null if the task has never been modified.
        /// </summary>
        public DateTime? ModifiedAt { get; set; }

        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
