namespace PublisherDomain
{
    public class BookAuthorDto
    {
        public string AuthorName { get; set; } = string.Empty;
        public string BookTitle { get; set; } = string.Empty;
        public decimal BookPrice { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
