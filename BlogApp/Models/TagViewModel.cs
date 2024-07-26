using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class TagViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
