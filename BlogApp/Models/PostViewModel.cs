using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace BlogApp.Models
{
    public class CreatePostViewModel
    {
        [Required(ErrorMessage = "Başlık girmek zorunludur.")]
        [StringLength(255, ErrorMessage = "Başlık 255 karakteri geçmemelidir.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "İçerik girmek zorunludur.")]
        public string Content { get; set; }

        public bool IsActive { get; set; } = false;

        public List<int> TagId { get; set; }

        public List<IFormFile> Images { get; set; }

        public List<SelectListItem> Tags { get; set; }
    }

}
