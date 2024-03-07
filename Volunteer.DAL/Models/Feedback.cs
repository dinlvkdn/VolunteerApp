
namespace Volunteer.DAL.Models
{
    public class Feedback : BaseEntity
    {
        public string Description { get; set; }


        public Member Member { get; set; }
    }
}