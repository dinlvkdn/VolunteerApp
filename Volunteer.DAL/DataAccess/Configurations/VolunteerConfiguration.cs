using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace Volunteer.DAL.DataAccess.Configurations
{
    public class VolunteerConfiguration : IEntityTypeConfiguration<Models.Volunteer>
    {
        public void Configure(EntityTypeBuilder<Models.Volunteer> builder)
        {
            builder.HasOne(x => x.Resume)
                .WithOne(x => x.Volunteer)
                .HasForeignKey<Models.Volunteer>(x => x.ResumeId);

            builder.HasMany(x => x.Members)
                .WithOne(x => x.Volunteer)
                .HasForeignKey(x => x.VolunteerId)
                .OnDelete(DeleteBehavior.Restrict);

                //.OnDelete(DeleteBehavior.SetNull);

            builder.HasKey(x => x.Id);
        }
    }
}
