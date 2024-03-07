using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volunteer.DAL.Models;

namespace Volunteer.DAL.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasOne(x => x.Volunteer)
                .WithOne(x => x.User)
                .HasForeignKey<User>(x => x.VolunteerId);
            builder
                .HasOne(x => x.Organization)
                .WithOne(x => x.User)
                .HasForeignKey<User>(x => x.OrganizationId);
            builder.HasKey(x => x.Id);
        }
    }
}
