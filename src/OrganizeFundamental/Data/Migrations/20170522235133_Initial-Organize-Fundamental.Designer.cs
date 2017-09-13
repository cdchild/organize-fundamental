using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using OrganizeFundamental.Models;

namespace OrganizeFundamental.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170522235133_Initial-Organize-Fundamental")]
    partial class InitialOrganizeFundamental
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("OrganizeFundamental.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("OrganizeFundamental.Models.Calendar.EventLabel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("EventLabels","Calendar");
                });

            modelBuilder.Entity("OrganizeFundamental.Models.CoupleScriptureLog.SharedScriptures", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<int>("PersonID");

                    b.Property<string>("ScriptureReference")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("Shared");

                    b.Property<int>("SharingPersonID");

                    b.HasKey("ID");

                    b.HasIndex("PersonID");

                    b.HasIndex("SharingPersonID");

                    b.ToTable("SharedScriptures","CoupleScriptureLog");
                });

            modelBuilder.Entity("OrganizeFundamental.Models.Organization", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("ParentOrganizationID");

                    b.HasKey("ID");

                    b.HasIndex("ParentOrganizationID");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("OrganizeFundamental.Models.Person", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("ID");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("OrganizeFundamental.Models.Scripture.Book", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FullTitle");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int?>("ParentID");

                    b.Property<string>("ShortLabel")
                        .HasMaxLength(8);

                    b.Property<string>("Summary");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("ID");

                    b.HasIndex("ParentID");

                    b.ToTable("Books","Scripture");
                });

            modelBuilder.Entity("OrganizeFundamental.Models.Scripture.Chapter", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookID");

                    b.Property<int>("Number");

                    b.HasKey("ID");

                    b.HasIndex("BookID");

                    b.ToTable("Chapter","Scripture");
                });

            modelBuilder.Entity("OrganizeFundamental.Models.Scripture.Verse", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChapterID");

                    b.Property<int>("Number");

                    b.HasKey("ID");

                    b.HasIndex("ChapterID");

                    b.ToTable("Verses","Scripture");
                });

            modelBuilder.Entity("OrganizeFundamental.Models.UtahEmployee.Accrual", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<byte>("AccrualTypeID");

                    b.Property<int>("PersonID");

                    b.HasKey("ID");

                    b.HasIndex("AccrualTypeID");

                    b.HasIndex("PersonID");

                    b.ToTable("Accruals","UtahEmployee");
                });

            modelBuilder.Entity("OrganizeFundamental.Models.UtahEmployee.AccrualType", b =>
                {
                    b.Property<byte>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("AccrualTypes","UtahEmployee");
                });

            modelBuilder.Entity("OrganizeFundamental.Models.UtahEmployee.EmployeePayPeriod", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CheckDate");

                    b.Property<DateTime>("EndDate");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("YearRank");

                    b.HasKey("ID");

                    b.ToTable("EmployeePayPeriods","UtahEmployee");
                });

            modelBuilder.Entity("OrganizeFundamental.Models.UtahEmployee.EmployeePayRate", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Effective");

                    b.Property<int>("PersonID");

                    b.Property<decimal>("Rate")
                        .HasColumnType("DECIMAL(9,4)");

                    b.HasKey("ID");

                    b.HasIndex("PersonID");

                    b.ToTable("EmployeePayRate","UtahEmployee");
                });

            modelBuilder.Entity("OrganizeFundamental.Models.UtahEmployee.Hour", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AccrualID");

                    b.Property<DateTime>("Date");

                    b.Property<double>("Hours");

                    b.Property<string>("Note");

                    b.Property<int>("PersonID");

                    b.HasKey("ID");

                    b.HasIndex("PersonID");

                    b.ToTable("Hours","UtahEmployee");
                });

            modelBuilder.Entity("OrganizeFundamental.Models.UtahEmployee.PaidHolidayObservance", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int>("EventLabelID");

                    b.HasKey("ID");

                    b.HasIndex("EventLabelID");

                    b.ToTable("PaidHolidayObservances","UtahEmployee");
                });

            modelBuilder.Entity("OrganizeFundamental.Models.UtahEmployee.PunchHour", b =>
                {
                    b.Property<int>("PersonID");

                    b.Property<int>("PayPeriodID");

                    b.Property<DateTime?>("Date");

                    b.Property<bool>("IsConsideredWorking");

                    b.Property<bool>("IsLunch");

                    b.Property<double>("ActualHours");

                    b.Property<DateTime>("CheckDate");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("FirstName");

                    b.Property<bool?>("HasPotentialError");

                    b.Property<int?>("HoursTally");

                    b.Property<bool?>("IsCurrentPayPeriod");

                    b.Property<bool?>("IsFuturePayPeriod");

                    b.Property<bool?>("IsPastPayPeriod");

                    b.Property<string>("LastName");

                    b.Property<int?>("MinutesTally");

                    b.Property<double>("RoundedHours");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("YearRank");

                    b.HasKey("PersonID", "PayPeriodID", "Date", "IsConsideredWorking", "IsLunch", "ActualHours");

                    b.ToTable("PunchHours","UtahEmployee");
                });

            modelBuilder.Entity("OrganizeFundamental.Models.UtahEmployee.ViewPayPeriod", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CheckDate");

                    b.Property<DateTime>("EndDate");

                    b.Property<bool>("IsCurrent");

                    b.Property<bool>("IsFuture");

                    b.Property<bool>("IsPast");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("YearRank");

                    b.HasKey("ID");

                    b.ToTable("ViewPayPeriods","UtahEmployee");
                });

            modelBuilder.Entity("OrganizeFundamental.Models.UtahEmployee.WorkStamp", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsConsideredWorking");

                    b.Property<bool>("IsLunch");

                    b.Property<int>("PersonID");

                    b.Property<DateTime>("Stamp");

                    b.HasKey("ID");

                    b.HasIndex("PersonID");

                    b.ToTable("WorkStamps","UtahEmployee");
                });

            modelBuilder.Entity("OrganizeFundamental.Models.UtahEmployee.WorkTimeCardEntry", b =>
                {
                    b.Property<int>("StampID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CheckDate");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsConsideredWorking");

                    b.Property<bool?>("IsCurrentPayPeriod");

                    b.Property<bool?>("IsFuturePayPeriod");

                    b.Property<bool>("IsLunch");

                    b.Property<bool?>("IsPastPayPeriod");

                    b.Property<string>("LastName");

                    b.Property<int>("PayPeriodID");

                    b.Property<int>("PersonID");

                    b.Property<DateTime>("Stamp");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("YearRank");

                    b.HasKey("StampID");

                    b.ToTable("WorkTimeCardEntries","UtahEmployee");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("OrganizeFundamental.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("OrganizeFundamental.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OrganizeFundamental.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OrganizeFundamental.Models.CoupleScriptureLog.SharedScriptures", b =>
                {
                    b.HasOne("OrganizeFundamental.Models.Person", "Person")
                        .WithMany("ScripturesReceived")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OrganizeFundamental.Models.Person", "SharingPerson")
                        .WithMany("ScripturesShared")
                        .HasForeignKey("SharingPersonID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OrganizeFundamental.Models.Organization", b =>
                {
                    b.HasOne("OrganizeFundamental.Models.Organization", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentOrganizationID");
                });

            modelBuilder.Entity("OrganizeFundamental.Models.Scripture.Book", b =>
                {
                    b.HasOne("OrganizeFundamental.Models.Scripture.Book", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentID");
                });

            modelBuilder.Entity("OrganizeFundamental.Models.Scripture.Chapter", b =>
                {
                    b.HasOne("OrganizeFundamental.Models.Scripture.Book", "Book")
                        .WithMany("Chapters")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OrganizeFundamental.Models.Scripture.Verse", b =>
                {
                    b.HasOne("OrganizeFundamental.Models.Scripture.Chapter", "Chapter")
                        .WithMany("Verses")
                        .HasForeignKey("ChapterID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OrganizeFundamental.Models.UtahEmployee.Accrual", b =>
                {
                    b.HasOne("OrganizeFundamental.Models.UtahEmployee.AccrualType", "AccrualType")
                        .WithMany("Accruals")
                        .HasForeignKey("AccrualTypeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OrganizeFundamental.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OrganizeFundamental.Models.UtahEmployee.EmployeePayRate", b =>
                {
                    b.HasOne("OrganizeFundamental.Models.Person", "Person")
                        .WithMany("EmployeePayRates")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OrganizeFundamental.Models.UtahEmployee.Hour", b =>
                {
                    b.HasOne("OrganizeFundamental.Models.Person", "Person")
                        .WithMany("Hours")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OrganizeFundamental.Models.UtahEmployee.PaidHolidayObservance", b =>
                {
                    b.HasOne("OrganizeFundamental.Models.Calendar.EventLabel", "EventLabel")
                        .WithMany("PaidHolidayObservances")
                        .HasForeignKey("EventLabelID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OrganizeFundamental.Models.UtahEmployee.WorkStamp", b =>
                {
                    b.HasOne("OrganizeFundamental.Models.Person", "Person")
                        .WithMany("WorkStamps")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
