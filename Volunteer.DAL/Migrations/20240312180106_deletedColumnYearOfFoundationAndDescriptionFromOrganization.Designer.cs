﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Volunteer.DAL.DataAccess;

#nullable disable

namespace Volunteer.DAL.Migrations
{
    [DbContext(typeof(VolunteerDBContext))]
    [Migration("20240312180106_deletedColumnYearOfFoundationAndDescriptionFromOrganization")]
    partial class deletedColumnYearOfFoundationAndDescriptionFromOrganization
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Volunteer.DAL.Models.Feedback", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("Volunteer.DAL.Models.JobOffer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("JobOffer");
                });

            modelBuilder.Entity("Volunteer.DAL.Models.Member", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FeedbackId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("JobOfferId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VolunteerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FeedbackId")
                        .IsUnique();

                    b.HasIndex("JobOfferId");

                    b.HasIndex("VolunteerId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("Volunteer.DAL.Models.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("Volunteer.DAL.Models.Resume", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("VolunteerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("VolunteerId")
                        .IsUnique();

                    b.ToTable("Resumes");
                });

            modelBuilder.Entity("Volunteer.DAL.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("VolunteerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId")
                        .IsUnique()
                        .HasFilter("[OrganizationId] IS NOT NULL");

                    b.HasIndex("VolunteerId")
                        .IsUnique()
                        .HasFilter("[VolunteerId] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Volunteer.DAL.Models.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Volunteers");
                });

            modelBuilder.Entity("Volunteer.DAL.Models.VolunteerJobOffer", b =>
                {
                    b.Property<Guid>("VolunteerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("JobOfferId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VolunteerId", "JobOfferId");

                    b.HasIndex("JobOfferId");

                    b.ToTable("VolunteerJobOffers");
                });

            modelBuilder.Entity("Volunteer.DAL.StatusHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("StatusHistory");
                });

            modelBuilder.Entity("Volunteer.DAL.Models.JobOffer", b =>
                {
                    b.HasOne("Volunteer.DAL.Models.Organization", "Organization")
                        .WithMany("JobOffers")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("Volunteer.DAL.Models.Member", b =>
                {
                    b.HasOne("Volunteer.DAL.Models.Feedback", "Feedback")
                        .WithOne("Member")
                        .HasForeignKey("Volunteer.DAL.Models.Member", "FeedbackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Volunteer.DAL.Models.JobOffer", "JobOffer")
                        .WithMany("Members")
                        .HasForeignKey("JobOfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Volunteer.DAL.Models.Volunteer", "Volunteer")
                        .WithMany("Members")
                        .HasForeignKey("VolunteerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Feedback");

                    b.Navigation("JobOffer");

                    b.Navigation("Volunteer");
                });

            modelBuilder.Entity("Volunteer.DAL.Models.Resume", b =>
                {
                    b.HasOne("Volunteer.DAL.Models.Volunteer", "Volunteer")
                        .WithOne("Resume")
                        .HasForeignKey("Volunteer.DAL.Models.Resume", "VolunteerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Volunteer");
                });

            modelBuilder.Entity("Volunteer.DAL.Models.User", b =>
                {
                    b.HasOne("Volunteer.DAL.Models.Organization", "Organization")
                        .WithOne("User")
                        .HasForeignKey("Volunteer.DAL.Models.User", "OrganizationId");

                    b.HasOne("Volunteer.DAL.Models.Volunteer", "Volunteer")
                        .WithOne("User")
                        .HasForeignKey("Volunteer.DAL.Models.User", "VolunteerId");

                    b.Navigation("Organization");

                    b.Navigation("Volunteer");
                });

            modelBuilder.Entity("Volunteer.DAL.Models.VolunteerJobOffer", b =>
                {
                    b.HasOne("Volunteer.DAL.Models.JobOffer", "JobOffer")
                        .WithMany("VolunteerJobOffers")
                        .HasForeignKey("JobOfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Volunteer.DAL.Models.Volunteer", "Volunteer")
                        .WithMany("VolunteerJobOffers")
                        .HasForeignKey("VolunteerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobOffer");

                    b.Navigation("Volunteer");
                });

            modelBuilder.Entity("Volunteer.DAL.StatusHistory", b =>
                {
                    b.HasOne("Volunteer.DAL.Models.Organization", "Organization")
                        .WithMany("StatusHistories")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("Volunteer.DAL.Models.Feedback", b =>
                {
                    b.Navigation("Member")
                        .IsRequired();
                });

            modelBuilder.Entity("Volunteer.DAL.Models.JobOffer", b =>
                {
                    b.Navigation("Members");

                    b.Navigation("VolunteerJobOffers");
                });

            modelBuilder.Entity("Volunteer.DAL.Models.Organization", b =>
                {
                    b.Navigation("JobOffers");

                    b.Navigation("StatusHistories");

                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("Volunteer.DAL.Models.Volunteer", b =>
                {
                    b.Navigation("Members");

                    b.Navigation("Resume")
                        .IsRequired();

                    b.Navigation("User")
                        .IsRequired();

                    b.Navigation("VolunteerJobOffers");
                });
#pragma warning restore 612, 618
        }
    }
}
