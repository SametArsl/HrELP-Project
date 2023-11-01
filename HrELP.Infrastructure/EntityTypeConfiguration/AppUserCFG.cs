using HrELP.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Infrastructure.EntityTypeConfiguration
{
    public class AppUserCFG: BaseEntityConfiguration<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.Photo).IsRequired(false);
            builder.Property(x => x.FirstName).HasMaxLength(20).HasColumnType("nvarchar").IsRequired();
            builder.Property(x => x.SecondFirstName).IsRequired(false).HasMaxLength(30).HasColumnType("nvarchar");
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(30).HasColumnType("nvarchar");
            builder.Property(x => x.SecondLastName).IsRequired(false).HasMaxLength(30).HasColumnType("nvarchar");
            builder.Property(x => x.BirthDate).IsRequired();
            builder.Property(x => x.BirthPlace).IsRequired(false).HasMaxLength(30).HasColumnType("nvarchar");
            builder.Property(x => x.IdentityNumber).IsRequired().HasMaxLength(11);
            builder.Property(x => x.Profession).IsRequired(false).HasMaxLength(30).HasColumnType("nvarchar");
            builder.Property(x => x.Department).IsRequired().HasMaxLength(30).HasColumnType("nvarchar");
            builder.Property(x => x.Salary).IsRequired().HasDefaultValue(11400M).HasColumnType("money");
            builder.HasOne(x => x.Address).WithOne(x => x.AppUser).HasForeignKey<Address>(x => x.AppUserId);
            builder.HasMany(x => x.ExpenseRequests).WithOne(x => x.AppUser).HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.AdvanceRequests).WithOne(x => x.AppUser).HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.LeaveRequests).WithOne(x => x.AppUser).HasForeignKey(x => x.UserId);


            PasswordHasher<AppUser> hashPwd = new PasswordHasher<AppUser>();

            AppUser root = new AppUser()
            {
                Id = 1,
                FirstName = "Gamze",
                LastName = "Altınelli",
                SecurityStamp = Guid.NewGuid().ToString(),
                Photo = "/images/profilepic/GamzeAltınelli.png",
                IdentityNumber = "11111111111",
                HireDate = DateTime.Now,
                BirthDate = DateTime.Now,
                BirthPlace = "Istanbul",
                CompanyId = 1,
                Profession = "Computer Engineer",
                Department = "Trainee",
                AddressId = 1,
                Salary = 30000M,
                Gender = Domain.Entities.Enums.Gender.Female
            };
            AppUser root2 = new AppUser()
            {
                Id = 2,
                FirstName = "Sabri",
                LastName = "Sen",
                SecurityStamp = Guid.NewGuid().ToString(),
                Photo = "/images/profilepic/profilfoto.png",
                IdentityNumber = "22222222222",
                HireDate = DateTime.Now,
                BirthDate = DateTime.Now,
                BirthPlace = "Istanbul",
                CompanyId = 1,
                Profession = "Backend Developer",
                Department = "R&D",
                AddressId = 2,
                Salary = 23000M,
                Gender = Domain.Entities.Enums.Gender.Male,
                MarriageStatus = true
            };

            root.PasswordHash = hashPwd.HashPassword(root, "Mngr_123.");
            root.Email = $"gamze.altinelli@bilgeadamboost.com";
            root.UserName = root.Email;

            root2.PasswordHash = hashPwd.HashPassword(root2, "Sbr_123.");
            root2.Email = $"sabri.sen@bilgeadamboost.com";
            root2.UserName = root2.Email;

            builder.HasData(root);
            builder.HasData(root2);

            base.Configure(builder);
        }
    }
}
