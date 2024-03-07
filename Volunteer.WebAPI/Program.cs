using Microsoft.EntityFrameworkCore;
using Volunteer.BL.Interfaces;
using Volunteer.BL.Services;

namespace Volunteer.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Seeder
            builder.Services.AddTransient<DataSeeder>();

            builder.Services.AddControllers();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IJobOfferService,JobOfferService>();
            builder.Services.AddScoped<IVolunteerService, VolunteerService>();
            builder.Services.AddScoped<IOrganizationService, OrganizationService>();
           
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DAL.DataAccess.VolunteerDBContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("db"));
            });

            var app = builder.Build();

            if (args.Length == 1 && args[0].ToLower() == "seeddata")
                SeedData(app);

            void SeedData(IHost app)
            {
                var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

                using (var scope = scopedFactory.CreateScope())
                {
                    var service = scope.ServiceProvider.GetService<DataSeeder>();
                    service.Seed();
                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
