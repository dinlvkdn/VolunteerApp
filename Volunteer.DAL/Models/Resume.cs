namespace Volunteer.DAL.Models
{
    public class Resume : BaseEntity
    {
        public string FileUrl { get; set; }
        public string FileName { get; set; }
        public Volunteer Volunteer { get; set; }
    }
}