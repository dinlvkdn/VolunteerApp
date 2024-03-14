namespace Domain.DTOs
{
    public class StatusHistoryDTO
    {  
        public Guid StatusHistoryId { get; set; }
        public DateTime Time { get; set; }
        public string Status { get; set; }
        public Guid OrganizationId { get; set; }
    }
}
