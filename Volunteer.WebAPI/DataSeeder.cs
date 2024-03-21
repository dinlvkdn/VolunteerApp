using Volunteer.DAL.DataAccess;
using Volunteer.DAL.Models;

namespace Volunteer.WebAPI
{
    public class DataSeeder
    {
        private readonly VolunteerDBContext _volunteerDBContext;

        public DataSeeder(VolunteerDBContext volunteerDBContext)
        {
            _volunteerDBContext = volunteerDBContext;
        }

        public void Seed()
        {
            if (!_volunteerDBContext.Volunteers.Any())
            {
                var volunteers = new List<DAL.Models.Volunteer>()
                {
                    new DAL.Models.Volunteer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219661"),
                        FirstName = "Iryna",
                        LastName = "Tytar",
                        DateOfBirth = new DateTime(1995, 5, 10),
                        Description = "I want to help those who need support and make the world a better place to live."
                    },
                    new DAL.Models.Volunteer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219662"),
                        FirstName = "Catherine",
                        LastName = "Liskova",
                        DateOfBirth = new DateTime(2000, 2, 24),
                        Description = "I am a volunteer with experience working in various charitable organizations for the past five years."
                    },
                    new DAL.Models.Volunteer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219663"),
                        FirstName = "Lilia",
                        LastName = "Kindrat",
                        DateOfBirth = new DateTime(2003, 12, 20),
                        Description = "I actively participate in various events and projects, together with other volunteers we collect funds for charitable organizations and conduct educational events for young people."
                    }
                };
                _volunteerDBContext.Volunteers.AddRange(volunteers);
                _volunteerDBContext.SaveChanges();
            }

            var vounteers = _volunteerDBContext.Volunteers.ToList();

            if (!_volunteerDBContext.Resumes.Any())
            {
                var resumes = new List<Resume>()
                {
                    new Resume()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219661"),
                        FileUrl = "C:\\Users\\Diana\\Pictures\\SavedPictures\\di.jpg",
                        FileName = "resume1",
                        VolunteerId = vounteers[0].Id
                    },
                    new Resume()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219662"),
                        FileUrl = "C:\\Users\\Diana\\Pictures\\SavedPictures\\di.jpg",
                        FileName = "resume2",
                        VolunteerId = vounteers[1].Id
                    },
                    new Resume()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219663"),
                        FileUrl = "C:\\Users\\Diana\\Pictures\\SavedPictures\\di.jpg",
                        FileName = "resume3",
                        VolunteerId = vounteers[2].Id
                    }
                };
                _volunteerDBContext.Resumes.AddRange(resumes);
                _volunteerDBContext.SaveChanges();
            }

            if (!_volunteerDBContext.Organizations.Any())
            {
                var organizations = new List<Organization>()
                {
                    new Organization()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219661"),
                        Name = "BioGro Certified Organic",
                        YearOfFoundation = 2001,
                        Description = "We have an organic and conventional commercial orchard nestled under Te Mata Peak in beautiful Havelock North. The orchard is about 3km from Havelock North ad is located in the Te Mata Special character area. It sits beside a river, biking tracks, cafes, restaurants and some world renown wineries. 20 minutes to Ocean, Waimarama, Haumoana and Te Awanga beaches. "
                    },
                    new Organization()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219662"),
                        Name = "Dio",
                        YearOfFoundation = 2003,
                        Description = "We have an organic and conventional commercial orchard nestled under Te Mata Peak in beautiful Havelock North. The orchard is about 3km from Havelock North ad is located in the Te Mata Special character area. It sits beside a river, biking tracks, cafes, restaurants and some world renown wineries. 20 minutes to Ocean, Waimarama, Haumoana and Te Awanga beaches. "
                    },
                    new Organization()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219663"),
                        Name = "Vio",
                        YearOfFoundation = 2003,
                        Description = "We have an organic and conventional commercial orchard nestled under Te Mata Peak in beautiful Havelock North. The orchard is about 3km from Havelock North ad is located in the Te Mata Special character area. It sits beside a river, biking tracks, cafes, restaurants and some world renown wineries. 20 minutes to Ocean, Waimarama, Haumoana and Te Awanga beaches. "
                    },
                };
                _volunteerDBContext.Organizations.AddRange(organizations);
                _volunteerDBContext.SaveChanges();
            }

            var organization = _volunteerDBContext.Organizations.ToList();

            if (!_volunteerDBContext.JobOffer.Any())
            {
                var jobOffers = new List<JobOffer>()
                {
                    new JobOffer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219662"),
                        City = "Lviv",
                        Country = "Ukraine",
                        DateTime = DateTime.Now,
                        Description = "We are looking for volunteers to help with community projects.",
                        OrganizationId = organization[1].Id,
                        Street = "Main Street",
                        Title = "Volunteer Opportunity at Community Projects"
                    },
                    new JobOffer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219663"),
                        City = "Kyiv",
                        Country = "Ukraine",
                        DateTime = DateTime.Now.AddDays(7),
                        Description = "Join us to make a difference in the lives of underprivileged children.",
                        OrganizationId = organization[1].Id,
                        Street = "Central Avenue",
                        Title = "Volunteer for Children's Education Program"
                    },
                    new JobOffer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219664"),
                        City = "Odessa",
                        Country = "Ukraine",
                        DateTime = DateTime.Now.AddDays(14),
                        Description = "Calling all volunteers interested in environmental conservation!",
                        OrganizationId = organization[2].Id,
                        Street = "Ocean Drive",
                        Title = "Environmental Conservation Project"
                    },
                    new JobOffer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219665"),
                        City = "Kharkiv",
                        Country = "Ukraine",
                        DateTime = DateTime.Now.AddDays(21),
                        Description = "Help us provide shelter and care for stray animals in our community.",
                        OrganizationId = organization[3].Id,
                        Street = "Park Avenue",
                        Title = "Volunteer at Animal Shelter"
                    },
                    new JobOffer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219666"),
                        City = "Lviv",
                        Country = "Ukraine",
                        DateTime = DateTime.Now.AddDays(28),
                        Description = "Looking for volunteers to assist with food distribution to the homeless.",
                        OrganizationId = organization[3].Id,
                        Street = "Market Street",
                        Title = "Volunteer for Homeless Food Distribution"
                    }

                };
                _volunteerDBContext.JobOffer.AddRange(jobOffers);
                _volunteerDBContext.SaveChanges();
            }
        }
    }

}
