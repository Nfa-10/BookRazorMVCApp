using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DemoBookApp.Models
{
    public class BookModel
    {
       
        [Key]
        public Guid BookId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Title must be within 4-30 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Edition is required")]
        [Range(1,30, ErrorMessage = "Range must be with 1-30")]
        public int Edition { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(20.0,10000.0,ErrorMessage ="Range must be with 2-10000")]
        public float Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime dateOfPublishing { get; set; }

        [ForeignKey("AuthorID")]
        public Guid AuthorID { get; set; }
        public AuthorModel Author { get; set; }

    }
}

