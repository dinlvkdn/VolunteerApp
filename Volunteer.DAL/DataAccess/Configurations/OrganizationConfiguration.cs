using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volunteer.DAL.Models;

namespace Volunteer.DAL.DataAccess.Configurations
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasMany(x => x.JobOffers)
                 .WithOne(x => x.Organization)
                 .HasForeignKey(x => x.OrganizationId);

            builder.HasMany(x => x.StatusHistories)
                .WithOne(x => x.Organization)
                .HasForeignKey(x => x.OrganizationId);

            builder.HasKey(x => x.Id);
        }
    }
}
