
namespace Volunteer.DAL.Models
{
    public class Member : BaseEntity
    {
        public Guid VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }

        public Guid? FeedbackId { get; set; }
        public Feedback? Feedback { get; set; }

        public Guid JobOfferId { get; set; }
        public JobOffer JobOffer { get; set; }
    }
}