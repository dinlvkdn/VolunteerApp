using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volunteer.DAL.Models;


namespace Volunteer.DAL.DataAccess.Configurations
{
    public class JobOfferConfiguration : IEntityTypeConfiguration<JobOffer>
    {
        public void Configure(EntityTypeBuilder<JobOffer> builder)
        {
            builder.HasMany(x => x.Members)
                .WithOne(x => x.JobOffer)
                .HasForeignKey(x => x.JobOfferId);

            builder.HasKey(x => x.Id);
        }
    }
}
