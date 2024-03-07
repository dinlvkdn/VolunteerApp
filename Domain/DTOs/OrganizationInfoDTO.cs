
namespace Domain.DTOs
{
    public class OrganizationInfoDTO
    {
        public Guid IdOrganization { get; set; }
        public string Name { get; set; }
        public int YearOfFoundation { get; set; } 
        public string Description { get; set; }
    }
}
