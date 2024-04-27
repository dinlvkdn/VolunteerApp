namespace Domain.DTOs
{
    public class ResumeDTO
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public Guid VolunteerId { get; set; }
    }
}
