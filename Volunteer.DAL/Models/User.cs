
namespace Volunteer.DAL.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public string RoleName { get; set; }

        public Guid? VolunteerId { get; set; }   
        public Volunteer? Volunteer { get; set; }
        public Guid? OrganizationId { get; set; }
        public Organization? Organization { get; set; }

    }
}
