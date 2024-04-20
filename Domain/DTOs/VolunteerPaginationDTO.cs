namespace Domain.DTOs
{
    public class VolunteerPaginationDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
