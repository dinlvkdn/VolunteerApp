using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volunteer.DAL.Models;

namespace Volunteer.DAL.DataAccess.Configurations
{
    public class VolunteerJobOfferConfiguration : IEntityTypeConfiguration<VolunteerJobOffer>
    {
        public void Configure(EntityTypeBuilder<VolunteerJobOffer> builder)
        {
            builder.HasKey(x => new { x.VolunteerId, x.JobOfferId });

            builder.HasOne(x => x.Volunteer)
                .WithMany(x => x.VolunteerJobOffers)
                .HasForeignKey(x => x.VolunteerId);

            builder.HasOne(x => x.JobOffer)
                .WithMany(x => x.VolunteerJobOffers)
                .HasForeignKey(x => x.JobOfferId);
        }
    }
}
