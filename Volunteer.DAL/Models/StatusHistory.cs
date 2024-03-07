using Volunteer.DAL.Models;

namespace Volunteer.DAL
{
    public class StatusHistory : BaseEntity
    {
        public DateTime Time { get; set; }
        public string Status { get; set; }

        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}