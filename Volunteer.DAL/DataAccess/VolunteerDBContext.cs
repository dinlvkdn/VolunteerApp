using Microsoft.EntityFrameworkCore;
using Volunteer.DAL.DataAccess.Configurations;
using Volunteer.DAL.Models;

namespace Volunteer.DAL.DataAccess
{
    public class VolunteerDBContext : DbContext
    {
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<JobOffer> JobOffer { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Organization> Organizations {get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<StatusHistory> StatusHistory { get; set; }
        public DbSet<Models.Volunteer> Volunteers { get; set; }
        public DbSet<VolunteerJobOffer> VolunteerJobOffers { get; set; }

        public VolunteerDBContext()
        {

        }
        public VolunteerDBContext(DbContextOptions<VolunteerDBContext> option) : base(option)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=VolunteerDB;Trusted_Connection=True;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new FeedbackConfiguration());
            builder.ApplyConfiguration(new JobOfferConfiguration());
            builder.ApplyConfiguration(new MemberConfiguration());
            builder.ApplyConfiguration(new OrganizationConfiguration());
            builder.ApplyConfiguration(new ResumeConfiguration());
            builder.ApplyConfiguration(new StatusHistoryConfiguration());
            builder.ApplyConfiguration(new VolunteerConfiguration());
            builder.ApplyConfiguration(new VolunteerJobOfferConfiguration());

            base.OnModelCreating(builder);
        }
    }
}




