using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volunteer.DAL.Models;

namespace Volunteer.DAL.DataAccess.Configurations
{
    public class ResumeConfiguration : IEntityTypeConfiguration<Resume>
    {
        public void Configure(EntityTypeBuilder<Resume> builder)
        {
            builder.HasOne(x => x.Volunteer)
                .WithOne(x => x.Resume)
                .HasForeignKey<Resume>(x => x.VolunteerId);

            builder.HasKey(x => x.Id);

        }
    }
}
