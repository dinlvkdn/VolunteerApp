
namespace Domain.DTOs
{
    public class ShowJobOfferDTO
    {
        public Guid Id { get; set; }
        public string JobOfferTitle { get; set; }
        public string JobOfferStreet { get; set; }
        public string JobOfferCity { get; set; }
        public string JobOfferCountry { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public string OrganizationName { get; set; }
        public Guid OrganizationId { get; set; }


    }
}
