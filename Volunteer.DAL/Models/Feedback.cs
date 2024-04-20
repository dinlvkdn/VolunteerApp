
namespace Volunteer.DAL.Models
{
    public class Feedback : BaseEntity
    {
        public string Description { get; set; }
        public int Rating { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid VolunteerId { get; set; }

        public Member Member { get; set; }
    }
}