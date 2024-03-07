using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.DAL.Models;

namespace Volunteer.DAL.DataAccess.Configurations
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasOne(x => x.Feedback)
                .WithOne(x => x.Member)
                .HasForeignKey<Member>(x => x.FeedbackId);

            builder.HasKey(x => x.Id);
        }
    }
}
