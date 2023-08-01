using System.ComponentModel.DataAnnotations;
using DemoBookApp.Audit;

namespace DemoBookApp.Models
{
    public class AuthorModel: Auditable
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
        public int Gender { get; set; } 
        public virtual ICollection<BookModel>? Books { get; set; }
    }
}
