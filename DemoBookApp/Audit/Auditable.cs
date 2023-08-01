namespace DemoBookApp.Audit
{
    public abstract class Auditable
    {
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        
    }
}
