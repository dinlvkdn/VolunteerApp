namespace Domain.DTOs
{
    public class FeedbackPaginationDTO
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
