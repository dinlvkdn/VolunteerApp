namespace Domain.DTOs
{
    public class CreateJobOfferDTO
    {
        public string JobOfferTitle { get; set; }
        public string JobOfferStreet { get; set; }
        public string JobOfferCity { get; set; }
        public string JobOfferCountry { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
    }
}
