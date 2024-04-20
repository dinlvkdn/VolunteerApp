namespace Domain.DTOs
{
    public class JobOfferPaginationDTO
    {
        public Guid Id { get; set; }
        public string JobOfferTitle { get; set; }
        public string JobOfferStreet { get; set; }
        public string JobOfferCity { get; set; }
        public string JobOfferCountry { get; set; }
        public DateTime DateTime { get; set; }
    }
}
