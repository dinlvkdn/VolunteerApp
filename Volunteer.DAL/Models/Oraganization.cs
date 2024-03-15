
namespace Volunteer.DAL.Models
{
    public class Organization : BaseEntity
    {
        public string Name { get; set; }
        public int YearOfFoundation { get; set; }
        public string Description { get; set; }
        public ICollection<StatusHistory> StatusHistories { get; set; }
        public ICollection<JobOffer> JobOffers { get; set; }
    }
}