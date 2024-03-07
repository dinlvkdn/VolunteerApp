
namespace Volunteer.DAL.Models
{
    public class Volunteer : BaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required string Description { get; set; }
        
        public User User { get; set; }

        public ICollection<Member> Members { get; set; }

        public Guid ResumeId { get; set; }
        public Resume Resume { get; set; }


        public ICollection<VolunteerJobOffer> VolunteerJobOffers { get; set; }


    }
}