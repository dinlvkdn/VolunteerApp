
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
            //if (!_volunteerDBContext.Users.Any())
            //{
            //    var users = new List<User>()
            //{
            //    new User()
            //    {
            //        Id = Guid.NewGuid(),
            //        Email = "vikakostryb2007@gmail.com",
            //        IsDeleted = false,
            //        RoleName = "volunteer",
            //        Name = "Vika"
            //    },
            //    new User()
            //    {
            //        Id = Guid.NewGuid(),
            //        Email = "nadiagolub2008@gmail.com",
            //        IsDeleted = false,
            //        RoleName = "volunteer",
            //        Name = "Nadia"
            //    },
            //    new User()
            //    {
            //        Id = Guid.NewGuid(),
            //        Email = "olegivashko2000@gmail.com",
            //        IsDeleted = false,
            //        RoleName = "volunteer",
            //        Name = "Oleg"
            //    }
            //};
            //    _volunteerDBContext.Users.AddRange(users);
            //    _volunteerDBContext.SaveChanges();
            //}


            if (!_volunteerDBContext.Volunteers.Any())
            {
                var volunteers = new List<DAL.Models.Volunteer>()
                {
                    new DAL.Models.Volunteer()
                    {
                       
                    //    FirstName = "Iryna",
                    //    LastName = "Tytar",
                    //    DateOfBirth = new DateTime(1995, 5, 10),
                    //    Description = "I want to help those who need support and make the world a better place to live."
                    },
                    new DAL.Models.Volunteer()
                    {
                        //FirstName = "Catherine",
                        //LastName = "Liskova",
                        //DateOfBirth = new DateTime(2000, 2, 24),
                        //Description = "I am a volunteer with experience working in various charitable organizations for the past five years."
                    },
                    new DAL.Models.Volunteer()
                    {
                        //FirstName = "Lilia",
                        //LastName = "Kindrat",
                        //DateOfBirth = new DateTime(2003, 12, 20),
                        //Description = "I actively participate in various events and projects, together with other volunteers we collect funds for charitable organizations and conduct educational events for young people."
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
                        FileUrl = "C:1",
                        FileName = "resume1",
                        VolunteerId = vounteers[0].Id
                    },
                    new Resume()
                    {
                        FileUrl = "C:2",
                        FileName = "resume2",
                        VolunteerId = vounteers[1].Id
                    },
                    new Resume()
                    {
                        FileUrl = "C:3",
                        FileName = "resume3",
                        VolunteerId = vounteers[2].Id
                    }
                };
                _volunteerDBContext.Resumes.AddRange(resumes);
                _volunteerDBContext.SaveChanges();
            }

            //if (!_volunteerDBContext.Organizations.Any())
            //{
            //    var organizations = new List<Organization>()
            //    {
            //        new Organization()
            //        {
            //            Id = Guid.NewGuid(),
            //            Name = "BioGro Certified Organic",
            //            YearOfFoundation = 2001,
            //            Description = "We have an organic and conventional commercial orchard nestled under Te Mata Peak in beautiful Havelock North. The orchard is about 3km from Havelock North ad is located in the Te Mata Special character area. It sits beside a river, biking tracks, cafes, restaurants and some world renown wineries. 20 minutes to Ocean, Waimarama, Haumoana and Te Awanga beaches. "
            //        }
            //    };
            //    _volunteerDBContext.Organizations.AddRange(organizations);
            //    _volunteerDBContext.SaveChanges();
            //}
        }
    }

}
