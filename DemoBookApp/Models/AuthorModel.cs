namespace DemoBookApp.Models
{
    public class AuthorModel
    {
        public Guid Id
        {
            get;
            set;
        }
        public string? Name { get; set; }

        public int Gender;
        public virtual ICollection<BookModel>? Books { get; set; }
    }
}
