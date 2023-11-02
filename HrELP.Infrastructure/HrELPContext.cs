using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Entities.Concrete.Requests;
using HrELP.Infrastructure.EntityTypeConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Infrastructure
{
    public class HrElpContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public HrElpContext()
        {

        }
        public HrElpContext(DbContextOptions<HrElpContext> options) : base(options)
        {

        }
        public DbSet<AppUser>? AppUsers { get; set; }
        public DbSet<AppRole>? AppRoles { get; set; }
        public DbSet<Address>? Addresses { get; set; }
        public DbSet<Company>? Companies { get; set; }
        public DbSet<AdvanceRequest>? AdvanceRequests { get; set; }
        public DbSet<ExpenseRequest>? ExpenseRequests { get; set; }
        public DbSet<LeaveTypes>? LeaveTypes { get; set; }
        public DbSet<LeaveRequest>? LeaveRequests { get; set; }
        public DbSet<RequestType>? RequestTypes { get; set; }
        public DbSet<RequestCategory>? RequestCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:hrelp-server.database.windows.net,1433;Initial Catalog=hrelpDB;Persist Security Info=False;User ID=hrelp;Password=Admin_123.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration<AppRole>(new AppRoleCFG());
            builder.ApplyConfiguration<AppUser>(new AppUserCFG());
            builder.ApplyConfiguration<Address>(new AddressCFG());
            builder.ApplyConfiguration<Company>(new CompanyCFG());
            builder.ApplyConfiguration<RequestType>(new RequestTypeCFG());
                builder.ApplyConfiguration<LeaveTypes>(new LeaveTypeCFG());
            builder.ApplyConfiguration<LeaveRequest>(new LeaveRequestCFG());

            builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int> { RoleId = 1, UserId = 1 });
            builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int> { RoleId = 3, UserId = 2 });
            builder.Entity<AppUser>().HasIndex(x=>x.IdentityNumber).IsUnique();

            base.OnModelCreating(builder);
        }
    }
}
