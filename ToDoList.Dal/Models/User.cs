using System.ComponentModel.DataAnnotations;

namespace ToDoList.Core.Models
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
            Email = string.Empty;
            ToDoItems = new List<ToDoItem>();
            CreatedAt = DateTime.UtcNow;
        }

        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Limited to 747 characters in honour of the longest name in the world! .
        /// </summary>
        [Required]
        [StringLength(747, ErrorMessage = "Name length can't be more than 747 characters.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the user's email address. Limited to 320 characters.
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(320, ErrorMessage = "Email length can't be more than 320 characters.")]
        public string Email { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Timestamp for when the user's details were last updated.
        /// </summary>
        public DateTime? ModifiedAt { get; set; }

        /// <summary>
        /// Navigation property for the tasks associated with the user.
        /// </summary>
        public List<ToDoItem> ToDoItems { get; set; }
    }
}
