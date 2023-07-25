using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DemoBookApp.Models
{
    public class BookModel
    {
       
        [Key]
        public Guid BookId { get; set; }
        public string? Title { get; set; }
        public int Edition { get; set; }
        public float Price { get; set; }
        public DateTime dateOfPublishing { get; set; }

        [ForeignKey("AuthorID")]
        public Guid AuthorID { get; set; }
        public AuthorModel Author { get; set; }

    }
}

