
namespace Volunteer.DAL.Models
{
    public class VolunteerJobOffer
    {
        public string Status { get; set; }

        public Guid VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }

        public Guid JobOfferId { get; set; }
        public JobOffer JobOffer { get; set; }  

    }
}