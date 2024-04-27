namespace Domain.DTOs
{
    public class VolunteerRequestsDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public DateTime DateOfBirth { get; set; }
        public StatusRequest Status { get; set; }
        public string TitleJobOffer { get; set; }
        public Guid JobOfferId { get; set; }
    }
}
