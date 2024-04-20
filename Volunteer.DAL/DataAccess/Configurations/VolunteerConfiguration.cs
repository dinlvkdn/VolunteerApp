using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace Volunteer.DAL.DataAccess.Configurations
{
    public class VolunteerConfiguration : IEntityTypeConfiguration<Models.Volunteer>
    {
        public void Configure(EntityTypeBuilder<Models.Volunteer> builder)
        {
            builder.HasMany(x => x.Members)
                .WithOne(x => x.Volunteer)
                .HasForeignKey(x => x.VolunteerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(x => x.Id);
        }
    }
}
