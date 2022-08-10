namespace testWebApi.Domain
{
    public class Book
    {
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Publisher { get; set; }
    }
}