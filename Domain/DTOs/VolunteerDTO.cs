
namespace Domain.DTOs
{
    public  class VolunteerDTO
    {
        public Guid IdVolunteer { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required string Description { get; set; }
    }
}
