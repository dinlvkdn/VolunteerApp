﻿namespace Volunteer.DAL.Models
{
    public class Volunteer : BaseEntity
    {
        //public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Description { get; set; }
        public ICollection<Member> Members { get; set; }
        public Resume Resume { get; set; }
        public ICollection<VolunteerJobOffer> VolunteerJobOffers { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}