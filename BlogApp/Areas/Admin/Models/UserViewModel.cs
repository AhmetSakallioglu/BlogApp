﻿using System.ComponentModel.DataAnnotations;

namespace BlogApp.Areas.Admin.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; } = null;
        public string Username { get; set; }
        public bool Locked { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? ProfileImageFileName { get; set; } = "no-image.jpg";
        public string Role { get; set; } = "user";
    }

    public class CreateUserModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, ErrorMessage = "Username can be max 30 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Fullname is required.")]
        [StringLength(50, ErrorMessage = "Fullname can be max 50 characters.")]
        public string FullName { get; set; }

        public bool Locked { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password can be min 6 characters.")]
        [StringLength(16, ErrorMessage = "Password can be max 16 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Re-Password is required.")]
        [MinLength(6, ErrorMessage = "Re-Password can be min 6 characters.")]
        [StringLength(16, ErrorMessage = "Re-Password can be max 16 characters.")]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "user";
    }

    public class EditUserModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, ErrorMessage = "Username can be max 30 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Fullname is required.")]
        [StringLength(50, ErrorMessage = "Fullname can be max 50 characters.")]
        public string FullName { get; set; }

        public bool Locked { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "user";
    }

}
