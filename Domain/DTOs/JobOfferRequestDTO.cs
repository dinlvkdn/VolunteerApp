namespace Domain.DTOs
{
    public class JobOfferRequestDTO
    {
        public Guid Id { get; set; }
        public string JobOfferTitle { get; set; }
        public string JobOfferStreet { get; set; }
        public string JobOfferCity { get; set; }
        public string JobOfferCountry { get; set; }
        public DateTime DateTime { get; set; }
        public StatusRequest Status { get; set; }
    }
}
