namespace Volunteer.DAL.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public Organization Organization { get; set; }
        public Volunteer Volunteer { get; set; }
    }
}
