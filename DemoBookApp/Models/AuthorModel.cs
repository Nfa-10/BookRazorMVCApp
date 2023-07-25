﻿using System.ComponentModel.DataAnnotations;

namespace DemoBookApp.Models
{
    public class AuthorModel
    {
        public Guid Id
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be within 3-30 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Gender is required")]
        public int Gender;
        public virtual ICollection<BookModel>? Books { get; set; }
    }
}
