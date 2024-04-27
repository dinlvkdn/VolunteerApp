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
                        Description = "I am passionate about making a positive impact on the lives of others and contributing to meaningful causes. With a background in community service and a desire to give back, I am dedicated to helping those in need and making the world a better place to live. I believe that every small act of kindness can create a ripple effect of change, and I am committed to being a part of that change.\"\r\n"
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
                    },
                    new DAL.Models.Volunteer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219664"),
                        FirstName = "John",
                        LastName = "Smith",
                        DateOfBirth = new DateTime(1990, 8, 15),
                        Description = "I have a background in social work and I am passionate about helping marginalized communities. I have volunteered with various NGOs and shelters, providing support and resources to those in need. My areas of focus include homelessness, refugee assistance, and mental health advocacy."
                    },
                    new DAL.Models.Volunteer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219665"),
                        FirstName = "Emily",
                        LastName = "Johnson",
                        DateOfBirth = new DateTime(1985, 3, 5),
                        Description = "As a healthcare professional, I am committed to promoting wellness and providing care to underserved populations. I volunteer my time at free clinics and community health centers, offering medical assistance to individuals who lack access to healthcare services. I am particularly interested in preventive medicine and health education."
                    },
                    new DAL.Models.Volunteer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219666"),
                        FirstName = "Michael",
                        LastName = "Brown",
                        DateOfBirth = new DateTime(1978, 11, 12),
                        Description = "I am an environmental activist dedicated to preserving our planet for future generations. I volunteer with conservation organizations, participating in tree planting initiatives, beach cleanups, and wildlife habitat restoration projects. I also advocate for sustainable living practices and raise awareness about the importance of environmental stewardship."
                    },
                    new DAL.Models.Volunteer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219667"),
                        FirstName = "Sophia",
                        LastName = "Miller",
                        DateOfBirth = new DateTime(1993, 6, 20),
                        Description = "I am a social entrepreneur with a passion for empowering women and girls. Through my volunteer work, I strive to create opportunities for female education, entrepreneurship, and leadership. I organize workshops, mentorship programs, and networking events to support women in realizing their full potential and achieving gender equality."
                    },
                    new DAL.Models.Volunteer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219668"),
                        FirstName = "Daniel",
                        LastName = "Garcia",
                        DateOfBirth = new DateTime(1982, 9, 8),
                        Description = "I am a retired veteran who is passionate about serving my community. I volunteer with local veterans' organizations, providing support to fellow veterans and their families. I also volunteer at homeless shelters and food banks, helping to address issues of poverty and homelessness in my community."
                    },
                    new DAL.Models.Volunteer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219669"),
                        FirstName = "Olivia",
                        LastName = "Taylor",
                        DateOfBirth = new DateTime(1975, 12, 3),
                        Description = "I am an artist and educator committed to promoting creativity and self-expression. I volunteer my time teaching art classes to children from underserved communities, helping them develop their artistic skills and explore their imaginations. I believe in the transformative power of art to inspire, heal, and connect people."
                    },
                    new DAL.Models.Volunteer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219670"),
                        FirstName = "David",
                        LastName = "Martinez",
                        DateOfBirth = new DateTime(1996, 4, 12),
                        Description = "I am a software engineer with a passion for coding and innovation. In addition to my professional work, I volunteer my time teaching coding classes to underprivileged youth, helping them develop valuable skills for the future. I believe in the power of technology to create positive change and bridge the digital divide."
                    },
                    new DAL.Models.Volunteer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219671"),
                        FirstName = "Isabella",
                        LastName = "Gonzalez",
                        DateOfBirth = new DateTime(1988, 10, 25),
                        Description = "I am a lawyer dedicated to providing pro bono legal services to those in need. I volunteer with legal aid organizations, offering assistance to low-income individuals facing legal challenges. I specialize in immigration law and human rights advocacy, working to protect the rights and dignity of vulnerable populations."
                    },
                    new DAL.Models.Volunteer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219672"),
                        FirstName = "Ethan",
                        LastName = "Clark",
                        DateOfBirth = new DateTime(1999, 7, 8),
                        Description = "I am a student passionate about environmental conservation and sustainability. I volunteer with environmental organizations, participating in conservation projects and environmental education initiatives. I believe that protecting our planet is essential for future generations, and I am committed to making a positive impact."
                    },
                    new DAL.Models.Volunteer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219673"),
                        FirstName = "Emma",
                        LastName = "White",
                        DateOfBirth = new DateTime(1992, 3, 18),
                        Description = "I am a psychologist committed to mental health advocacy and support. I volunteer with crisis hotlines, providing emotional support and guidance to individuals in distress. I also organize workshops and support groups to raise awareness about mental health issues and promote self-care and resilience."
                    },
                    new DAL.Models.Volunteer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219674"),
                        FirstName = "Noah",
                        LastName = "Adams",
                        DateOfBirth = new DateTime(1980, 9, 30),
                        Description = "I am a firefighter dedicated to serving and protecting my community. In addition to my professional duties, I volunteer with disaster response teams, providing assistance during emergencies and natural disasters. I am committed to ensuring the safety and well-being of all individuals in times of crisis."
                    },
                    new DAL.Models.Volunteer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219675"),
                        FirstName = "Ava",
                        LastName = "Thomas",
                        DateOfBirth = new DateTime(1997, 11, 7),
                        Description = "I am an animal lover passionate about animal welfare and conservation. I volunteer at animal shelters, caring for and socializing with shelter animals awaiting adoption. I also advocate for animal rights and habitat preservation, working to protect endangered species and promote responsible pet ownership."
                    },
                    new DAL.Models.Volunteer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219676"),
                        FirstName = "Liam",
                        LastName = "Wilson",
                        DateOfBirth = new DateTime(1994, 7, 25),
                        Description = "I am an educator passionate about providing educational opportunities to underserved communities. I volunteer with literacy programs, tutoring students in reading and writing skills. I believe that access to quality education is a fundamental right, and I am committed to breaking down barriers to learning."
                    },
                    new DAL.Models.Volunteer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219677"),
                        FirstName = "Mia",
                        LastName = "Brown",
                        DateOfBirth = new DateTime(1989, 5, 12),
                        Description = "I am a nurse dedicated to improving healthcare access and outcomes for vulnerable populations. I volunteer with medical missions, providing medical care to underserved communities around the world. I also advocate for healthcare policy reform and work to address disparities in healthcare delivery."
                    },
                    new DAL.Models.Volunteer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219678"),
                        FirstName = "Elijah",
                        LastName = "Martinez",
                        DateOfBirth = new DateTime(1983, 10, 8),
                        Description = "I am a chef passionate about addressing food insecurity and promoting nutrition education. I volunteer at community kitchens, preparing and serving nutritious meals to individuals experiencing homelessness. I also teach cooking classes to empower individuals to make healthy food choices on a limited budget."
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
                        Id = Guid.NewGuid(),
                        FileUrl = "C:\\Users\\Diana\\Exoft\\ResumeUploads\\78ae91f4-6d5e-40cc-bc60-e27e84219661",
                        FileName = "78ae91f4-6d5e-40cc-bc60-e27e84219661",
                        VolunteerId = vounteers[0].Id
                    },
                    new Resume()
                    {
                        Id = Guid.NewGuid(),
                        FileUrl = "C:\\Users\\Diana\\Exoft\\ResumeUploads\\" + vounteers[1].Id,
                        FileName = "" + vounteers[1].Id,
                        VolunteerId = vounteers[1].Id
                    },
                    new Resume()
                    {
                        Id = Guid.NewGuid(),
                        FileUrl = "C:\\Users\\Diana\\Exoft\\ResumeUploads\\" + vounteers[2].Id,
                        FileName = "" + vounteers[2].Id,
                        VolunteerId = vounteers[2].Id
                    },
                    new Resume()
                    {
                        Id = Guid.NewGuid(),
                        FileUrl = "C:\\Users\\Diana\\Exoft\\ResumeUploads\\" + vounteers[3].Id,
                        FileName = "" + vounteers[3].Id,
                        VolunteerId = vounteers[3].Id
                    },
                    new Resume()
                    {
                        Id = Guid.NewGuid(),
                        FileUrl = "C:\\Users\\Diana\\Exoft\\ResumeUploads\\" + vounteers[4].Id,
                        FileName = "" + vounteers[4].Id,
                        VolunteerId = vounteers[4].Id
                    },
                    new Resume()
                    {
                        Id = Guid.NewGuid(),
                        FileUrl = "C:\\Users\\Diana\\Exoft\\ResumeUploads\\" + vounteers[5].Id,
                        FileName = "" + vounteers[5].Id,
                        VolunteerId = vounteers[5].Id
                    },
                    new Resume()
                    {
                        Id = Guid.NewGuid(),
                        FileUrl = "C:\\Users\\Diana\\Exoft\\ResumeUploads\\" + vounteers[6].Id,
                        FileName = "" + vounteers[6].Id,
                        VolunteerId = vounteers[6].Id
                    },
                    new Resume()
                    {
                        Id = Guid.NewGuid(),
                        FileUrl = "C:\\Users\\Diana\\Exoft\\ResumeUploads\\" + vounteers[7].Id,
                        FileName = "" + vounteers[7].Id,
                        VolunteerId = vounteers[7].Id
                    },
                    new Resume()
                    {
                        Id = Guid.NewGuid(),
                        FileUrl = "C:\\Users\\Diana\\Exoft\\ResumeUploads\\" + vounteers[8].Id,
                        FileName = "" + vounteers[8].Id,
                        VolunteerId = vounteers[8].Id
                    },
                    new Resume()
                    {
                        Id = Guid.NewGuid(),
                        FileUrl = "C:\\Users\\Diana\\Exoft\\ResumeUploads\\" + vounteers[9].Id,
                        FileName = "" + vounteers[9].Id,
                        VolunteerId = vounteers[9].Id
                    },
                    new Resume()
                    {
                        Id = Guid.NewGuid(),
                        FileUrl = "C:\\Users\\Diana\\Exoft\\ResumeUploads\\" + vounteers[10].Id,
                        FileName = "" + vounteers[10].Id,
                        VolunteerId = vounteers[10].Id
                    },
                    new Resume()
                    {
                        Id = Guid.NewGuid(),
                        FileUrl = "C:\\Users\\Diana\\Exoft\\ResumeUploads\\" + vounteers[11].Id,
                        FileName = "" + vounteers[11].Id,
                        VolunteerId = vounteers[11].Id
                    },
                    new Resume()
                    {
                        Id = Guid.NewGuid(),
                        FileUrl = "C:\\Users\\Diana\\Exoft\\ResumeUploads\\" + vounteers[12].Id,
                        FileName = "" + vounteers[12].Id,
                        VolunteerId = vounteers[12].Id
                    },
                    new Resume()
                    {
                        Id = Guid.NewGuid(),
                        FileUrl = "C:\\Users\\Diana\\Exoft\\ResumeUploads\\" + vounteers[13].Id,
                        FileName = "" + vounteers[13].Id,
                        VolunteerId = vounteers[13].Id
                    },
                    new Resume()
                    {
                        Id = Guid.NewGuid(),
                        FileUrl = "C:\\Users\\Diana\\Exoft\\ResumeUploads\\" + vounteers[14].Id,
                        FileName = "" + vounteers[14].Id,
                        VolunteerId = vounteers[14].Id
                    },
                    new Resume()
                    {
                        Id = Guid.NewGuid(),
                        FileUrl = "C:\\Users\\Diana\\Exoft\\ResumeUploads\\" + vounteers[15].Id,
                        FileName = "" + vounteers[15].Id,
                        VolunteerId = vounteers[15].Id
                    },
                    new Resume()
                    {
                        Id = Guid.NewGuid(),
                        FileUrl = "C:\\Users\\Diana\\Exoft\\ResumeUploads\\" + vounteers[16].Id,
                        FileName = "" + vounteers[16].Id,
                        VolunteerId = vounteers[16].Id
                    },
                    new Resume()
                    {
                        Id = Guid.NewGuid(),
                        FileUrl = "C:\\Users\\Diana\\Exoft\\ResumeUploads\\" + vounteers[17].Id,
                        FileName = "" + vounteers[17].Id,
                        VolunteerId = vounteers[17].Id
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
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219665"),
                        Name = "Nature's Bounty Organic Co-op",
                        YearOfFoundation = 2008,
                        Description = "Nature's Bounty Organic Co-op is a community-owned cooperative dedicated to providing fresh, local organic produce to our members. Our cooperative model promotes food sovereignty, supports local farmers, and strengthens community bonds. We believe in the power of sustainable agriculture to nourish both people and the planet."
                    },
                    new Organization()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219666"),
                        Name = "EarthGuard Conservation Society",
                        YearOfFoundation = 2010,
                        Description = "EarthGuard Conservation Society is a non-profit organization committed to protecting and conserving the natural resources of our planet. Through advocacy, education, and community support initiatives, we work to address environmental challenges such as climate change, deforestation, and species extinction. Our goal is to inspire collective action and promote a sustainable future for future generations."
                    },
                    new Organization()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219667"),
                        Name = "OceanWatch Marine Conservation",
                        YearOfFoundation = 2012,
                        Description = "OceanWatch Marine Conservation is dedicated to preserving and restoring marine ecosystems through research, education, and conservation efforts. Our organization conducts scientific studies, engages in habitat restoration projects, and advocates for sustainable fishing practices. We believe in the importance of protecting our oceans for the health of marine life and the well-being of coastal communities."
                    },
                    new Organization()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219668"),
                        Name = "GreenPath Environmental Education",
                        YearOfFoundation = 2014,
                        Description = "GreenPath Environmental Education is committed to empowering individuals and communities to become stewards of the environment. Through hands-on workshops, educational programs, and outdoor experiences, we educate people of all ages about ecological principles, conservation practices, and sustainable living. Our goal is to foster a deeper connection to nature and inspire positive environmental action."
                    },
                    new Organization()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219669"),
                        Name = "Wildlife Guardians Alliance",
                        YearOfFoundation = 2016,
                        Description = "Wildlife Guardians Alliance is dedicated to protecting and preserving wildlife habitats and species around the world. We work collaboratively with local communities, governments, and conservation organizations to implement effective strategies for wildlife conservation. From anti-poaching efforts to habitat restoration projects, we are committed to safeguarding the biodiversity of our planet."
                    },
                    new Organization()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219670"),
                        Name = "Global Health Initiative",
                        YearOfFoundation = 2008,
                        Description = "The Global Health Initiative is dedicated to improving healthcare access and outcomes in underserved communities worldwide. Through partnerships with local healthcare providers and organizations, we support initiatives that address health disparities, promote disease prevention, and strengthen healthcare infrastructure. Our goal is to create sustainable solutions for better health and well-being for all."
                    },
                    new Organization()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219671"),
                        Name = "Education for All Foundation",
                        YearOfFoundation = 2010,
                        Description = "The Education for All Foundation is committed to providing quality education to children and adults in marginalized communities. We build schools, provide scholarships, and offer educational resources to empower individuals to reach their full potential. Our programs focus on literacy, numeracy, vocational training, and life skills development, ensuring that everyone has access to learning opportunities."
                    },
                    new Organization()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219672"),
                        Name = "Community Development Alliance",
                        YearOfFoundation = 2005,
                        Description = "The Community Development Alliance works to strengthen communities and improve quality of life for residents through economic empowerment, social inclusion, and infrastructure development. We partner with local governments, NGOs, and community groups to implement projects in areas such as affordable housing, job training, healthcare access, and cultural preservation. Our aim is to foster vibrant, resilient communities where everyone can thrive."
                    },
                    new Organization()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219673"),
                        Name = "Environmental Conservation Society",
                        YearOfFoundation = 2007,
                        Description = "The Environmental Conservation Society is dedicated to protecting and preserving our planet's natural resources and ecosystems. Through advocacy, research, and conservation projects, we work to combat climate change, conserve biodiversity, and promote sustainable living practices. Our efforts include habitat restoration, wildlife conservation, and public education campaigns to raise awareness about environmental issues."
                    },
                    new Organization()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219674"),
                        Name = "Women's Empowerment Network",
                        YearOfFoundation = 2009,
                        Description = "The Women's Empowerment Network is committed to advancing gender equality and empowering women and girls worldwide. We support initiatives that promote women's rights, economic empowerment, and leadership development. Our programs include skills training, mentorship, advocacy, and community outreach, aiming to create a more inclusive and equitable society where women can thrive."
                    },
                    new Organization()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219675"),
                        Name = "Youth Development Initiative",
                        YearOfFoundation = 2012,
                        Description = "The Youth Development Initiative focuses on empowering young people to reach their full potential and become active contributors to society. We provide educational opportunities, leadership training, and mentorship programs to help youth develop essential skills and navigate challenges they face. By investing in youth, we aim to create a brighter future for individuals and communities."
                    }
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
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219679"),
                        City = "Greenville",
                        Country = "United States",
                        DateTime = DateTime.Now,
                        Description = "Become an essential member of our cooperative and play a pivotal role " +
                        "in distributing fresh, organic produce to our valued members. Join us as we strive to" +
                        " build a community-driven food system that prioritizes sustainability, quality, and local" +
                        " resilience.\r\n\r\nAs a volunteer with our cooperative, you'll have the unique opportunity" +
                        " to contribute to every step of the farm-to-table journey, from harvest to distribution." +
                        " Your efforts will directly impact the health and well-being of our members, ensuring" +
                        " they have access to the freshest, most nutritious produce available.\r\n\r\nOne of" +
                        " the most rewarding aspects of volunteering with us is being part of a close-knit" +
                        " community that shares a passion for sustainable agriculture and healthy living. " +
                        "You'll work alongside like-minded individuals who are dedicated to making a positive" +
                        " impact on the environment and supporting local farmers.\r\n\r\nYour role as a " +
                        "volunteer will involve assisting with various tasks related to the distribution" +
                        " of organic produce. From sorting and packing fruits and vegetables to preparing" +
                        " orders for delivery, you'll play a vital role in ensuring that our members receive" +
                        " their weekly shares of fresh, seasonal produce.\r\n\r\nIn addition to your day-to-day" +
                        " responsibilities, you'll also have the opportunity to engage with our members, building" +
                        " relationships and fostering connections within the community. Whether you're answering" +
                        " questions about our farming practices or sharing recipe ideas, your interactions will" +
                        " help strengthen the bond between our cooperative and its members.\r\n\r\nAt the heart" +
                        " of our cooperative is a commitment to sustainability and environmental stewardship. We " +
                        "prioritize organic farming methods, regenerative agriculture practices, and minimizing" +
                        " waste to reduce our ecological footprint. By volunteering with us, you'll learn about" +
                        " the importance of these principles and gain valuable skills that you can apply to your" +
                        " own life and community.\r\n\r\nAs a token of our appreciation for your dedication and " +
                        "hard work, we provide volunteers with access to fresh, organic produce and other perks," +
                        " such as discounts on farm products and educational workshops. We want you to feel valued" +
                        " and supported as part of our cooperative family.\r\n\r\nJoin us in our mission to create" +
                        " a more resilient, equitable food system that nourishes both people and the planet. " +
                        "Together, we can build a future where everyone has access to healthy, sustainably grown" +
                        " food and communities thrive in harmony with nature.",
                        OrganizationId = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219665"),
                        Street = "Bounty Lane",
                        Title = "Produce Distribution Assistant"
                    },
                    new JobOffer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219680"),
                        City = "Greenville",
                        Country = "United States",
                        DateTime = DateTime.Now,
                        Description = "Immerse yourself in the world of organic agriculture by joining our" +
                        " farm operations as a dedicated volunteer. Gain hands-on experience working alongside " +
                        "experienced local farmers while supporting sustainable food production practices.\r\n\r\nAs" +
                        " a volunteer, you'll have the opportunity to engage in a variety of tasks essential " +
                        "to the success of our farm. From planting and cultivating crops to harvesting and" +
                        " packaging fresh produce, you'll play a vital role in every stage of the farming " +
                        "process.\r\n\r\nWorking closely with our team of farmers, you'll learn valuable skills" +
                        " and techniques that are fundamental to organic agriculture. Gain insights into soil " +
                        "health management, pest control strategies, and crop rotation methods as you contribute" +
                        " to the daily operations of our farm.\r\n\r\nBeyond acquiring practical skills," +
                        " volunteering with us offers a unique opportunity to connect with the land and the community." +
                        " Experience the satisfaction of working outdoors, surrounded by the natural beauty of our " +
                        "farm, while forging meaningful relationships with fellow volunteers and local" +
                        " farmers.\r\n\r\nOur commitment to sustainable food production means that every task " +
                        "you undertake as a volunteer contributes to the health of the environment and the " +
                        "well-being of our community. By supporting organic agriculture, you'll play a direct " +
                        "role in promoting biodiversity, conserving natural resources, and mitigating the " +
                        "impacts of climate change.\r\n\r\nAs a token of our appreciation for your dedication " +
                        "and hard work, we offer volunteers the chance to enjoy the fruits of their labor with " +
                        "access to fresh, organic produce straight from the farm. Additionally, you'll have the" +
                        " opportunity to participate in educational workshops and farm tours, further " +
                        "enriching your understanding of sustainable farming practices.\r\n\r\nJoin us on our " +
                        "journey towards a more resilient and regenerative food system. By lending your time" +
                        " and energy to our farm operations, you'll not only learn valuable skills but also " +
                        "become an integral part of a community-driven effort to promote organic agriculture" +
                        " and sustainable living.",
                        OrganizationId = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219665"),
                        Street = "Bounty Lane",
                        Title = "Farm Assistant"
                    },
                    new JobOffer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219681"),
                        City = "Greenville",
                        Country = "United States",
                        DateTime = DateTime.Now,
                        Description = "Become a vital member of our team and contribute to the management" +
                        " of our cooperative store, where we prioritize offering fresh, organic produce and" +
                        " promoting healthy eating habits within our community.\r\n\r\nAs a volunteer, " +
                        "you'll play a crucial role in ensuring the smooth operation of our cooperative " +
                        "store. Your responsibilities will include assisting with inventory management, " +
                        "organizing product displays, and maintaining a clean and inviting store" +
                        " environment.\r\n\r\nOne of the key aspects of your role will be to provide " +
                        "exceptional customer service to our patrons. You'll greet customers warmly," +
                        " answer their questions about our products, and offer guidance on healthy food" +
                        " choices. By engaging with customers in a friendly and knowledgeable manner, " +
                        "you'll help foster a positive shopping experience and encourage repeat visits" +
                        " to our store.\r\n\r\nIn addition to managing the day-to-day operations of the" +
                        " store, you'll also have the opportunity to contribute to our efforts to promote" +
                        " healthy eating habits within the community. This may involve organizing educational" +
                        " events or workshops focused on nutrition and wellness, as well as developing" +
                        " promotional materials to highlight the nutritional benefits of our products.\r\n\r\nBy" +
                        " volunteering with us, you'll gain valuable experience in retail management, " +
                        "customer service, and food advocacy. You'll have the opportunity to work alongside" +
                        " a passionate team dedicated to making a positive impact on the health and " +
                        "well-being of our community.\r\n\r\nJoin us in our mission to provide access" +
                        " to fresh, organic produce and inspire healthier lifestyles. Together, we can" +
                        " create a welcoming environment where everyone feels empowered to make nutritious" +
                        " food choices and lead happier, healthier lives.",
                        OrganizationId = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219665"),
                        Street = "Bounty Lane",
                        Title = "Store Assistant"
                    },
                    new JobOffer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219682"),
                        City = "Global",
                        Country = "Various",
                        DateTime = DateTime.Now,
                        Description = "Become an integral part of our conservation team and embark on a" +
                        " journey to participate in impactful research projects across the globe. Your role" +
                        " will involve contributing to vital efforts aimed at safeguarding endangered species" +
                        " and preserving delicate ecosystems.\r\n\r\nAs a volunteer, you'll have the opportunity" +
                        " to work alongside experienced conservationists and researchers, gaining hands-on experience" +
                        " in the field of wildlife conservation. You'll assist in collecting data, conducting " +
                        "surveys, and monitoring wildlife populations in various habitats.\r\n\r\nYour" +
                        " contributions will directly contribute to our mission of protecting endangered" +
                        " species and preserving biodiversity. Whether it's tracking the movements of" +
                        " elusive animals or studying the health of fragile ecosystems, your work will play" +
                        " a crucial role in informing conservation strategies and decision-making.\r\n\r\nJoining" +
                        " our team offers a unique opportunity to immerse yourself in the world of conservation" +
                        " biology and make a meaningful impact on the future of our planet. By dedicating your" +
                        " time and skills to conservation efforts, you'll be part of a global movement working" +
                        " towards a more sustainable and ecologically balanced world.",
                        OrganizationId = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219666"),
                        Street = "Conservation Way",
                        Title = "Conservation Research Assistant"
                    },
                    new JobOffer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219683"),
                        City = "Global",
                        Country = "Various",
                        DateTime = DateTime.Now,
                        Description = "Join our marine conservation team and become a vital part of our efforts " +
                        "to protect and restore coral reefs and marine habitats. As a volunteer, you'll have the" +
                        " opportunity to dive into action and make a real difference for our oceans.\r\n\r\nYour" +
                        " role will involve participating in various conservation initiatives aimed at preserving" +
                        " marine biodiversity and promoting sustainable ocean practices. You'll work alongside" +
                        " experienced marine biologists and conservationists, gaining hands-on experience in" +
                        " underwater research and habitat restoration.\r\n\r\nWhether it's conducting reef surveys," +
                        " monitoring marine life, or assisting with coral transplantation efforts, your contributions" +
                        " will directly impact the health and resilience of fragile marine ecosystems. By " +
                        "dedicating your time and energy to marine conservation, you'll play a crucial role " +
                        "in safeguarding the future of our oceans.\r\n\r\nJoining our team offers an exciting" +
                        " opportunity to combine your passion for diving with meaningful conservation work. " +
                        "By immersing yourself in the underwater world, you'll not only gain valuable skills" +
                        " and experience but also make a positive impact on the marine environment for " +
                        "generations to come.",
                        OrganizationId = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219666"),
                        Street = "Conservation Way",
                        Title = "Marine Conservation Volunteer"
                    },
                    new JobOffer()
                    {
                        Id = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219684"),
                        City = "Global",
                        Country = "Various",
                        DateTime = DateTime.Now,
                        Description = "Join our dynamic advocacy team and play a vital role in raising awareness " +
                        "about pressing environmental issues. As a member of our team, you'll have the opportunity" +
                        " to mobilize communities and drive meaningful change for our planet.\r\n\r\nYour" +
                        " responsibilities will include engaging with diverse audiences through outreach " +
                        "initiatives, educational campaigns, and community events. By leveraging your passion " +
                        "for environmental conservation, you'll inspire others to take action and become stewards" +
                        " of the Earth.\r\n\r\nThrough strategic advocacy efforts, you'll amplify key messages," +
                        " promote sustainable practices, and advocate for policies that protect our natural world." +
                        " Whether it's organizing rallies, hosting workshops, or collaborating with like-minded" +
                        " organizations, you'll be at the forefront of the environmental movement.\r\n\r\nBy" +
                        " joining forces with our dedicated team, you'll be empowered to make a tangible " +
                        "difference in the fight against climate change, habitat destruction, and pollution." +
                        " Together, we'll work towards a brighter, greener future for generations to come.",
                        OrganizationId = new Guid("78ae91f4-6d5e-40cc-bc60-e27e84219666"),
                        Street = "Conservation Way",
                        Title = "Environmental Advocate"
                    }
                };
                _volunteerDBContext.JobOffer.AddRange(jobOffers);
                _volunteerDBContext.SaveChanges();
            }
        }
    }

}
