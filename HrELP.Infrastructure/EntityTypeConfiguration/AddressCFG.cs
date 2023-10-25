using HrELP.Domain.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Infrastructure.EntityTypeConfiguration
{
    public class AddressCFG: BaseEntityConfiguration<Address>
    {
        public override void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(x =>x.City).IsRequired().HasMaxLength(100).HasColumnType("varchar");
            builder.Property(x =>x.Town).IsRequired().HasMaxLength(100).HasColumnType("varchar");
            builder.Property(x =>x.District).IsRequired().HasMaxLength(100).HasColumnType("varchar");
            builder.Property(x =>x.Quarter).IsRequired().HasMaxLength(100).HasColumnType("varchar");
            builder.Property(x =>x.PostalCode).IsRequired().HasPrecision(5).HasColumnType("int");
            builder.Property(x =>x.FullAddress).IsRequired().HasMaxLength(100).HasColumnType("varchar");
            builder.HasOne(x => x.AppUser).WithOne(x => x.Address);

            builder.HasData(new Address
            {
                AppUserId = 1,
                Id = 1,
                City = "Bursa",
                Town = "Gemlik",
                District = "Gemlik",
                Quarter = "Ata Mh.",
                PostalCode = 16848,
                FullAddress = "A sk. No: 32 D: 3"
            });
            builder.HasData(new Address
            {
                AppUserId = 2,
                Id = 2,
                City = "İstanbul",
                Town = "Kadıköy",
                District = "Göztepe",
                Quarter = "Göztepe Mh.",
                PostalCode = 34730,
                FullAddress = "A sk. No: 32 D: 3"
            });

            base.Configure(builder);
        }
    }
}
