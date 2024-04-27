namespace Domain.DTOs
{
    public class FeedbackDTO
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public Guid OrganizationId { get; set; }
    }
}
