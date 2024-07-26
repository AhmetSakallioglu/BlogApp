using BlogApp.Entities;
using System.ComponentModel.DataAnnotations;

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
        public List<string> ImageUrl { get; set; }
    }
}
