namespace Volunteer.DAL.Models
{
    public class JobOffer : BaseEntity
    {
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Title {  get; set; }

        public ICollection<VolunteerJobOffer> VolunteerJobOffers { get; set; }

        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public ICollection<Member> Members { get; set; }
    }
}