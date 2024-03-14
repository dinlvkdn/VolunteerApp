using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volunteer.DAL.Models;

namespace Volunteer.DAL.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Organization)
            .WithOne(x => x.User)
            .HasForeignKey<Organization>(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Volunteer)
                .WithOne(x => x.User)
                .HasForeignKey<Models.Volunteer>(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
