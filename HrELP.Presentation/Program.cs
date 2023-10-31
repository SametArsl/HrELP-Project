using HrELP.Application.Services.AddressService;
using HrELP.Application.Services.AppUserService;
using HrELP.Application.Services.CompanyService;
using HrELP.Application.Services.EmailService;
using HrELP.Domain.Entities.Concrete;
using HrELP.Domain.Repositories;
using HrELP.Infrastructure.Repositories;
using HrELP.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HrELP.Application.Services.RequestTypeService;
using HrELP.Application.Services.RequestCategoryService;
using HrELP.Application.Services.LeaveRequestService;
using HrELP.Application.Services.ExpenseRequestService;
using HrELP.Application.Services.AdvanceRequestService;
using HrELP.Application.Services.AddressAPIService;
using HrELP.Application.AutoMapper;

namespace HrELP.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<HrElpContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20); // Adjust the timeout as needed
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddIdentity<AppUser, AppRole>(x =>
            {
                // Diðer ayarlar buraya yazýlabilir
                x.Password.RequiredLength = 8;
                x.Password.RequireUppercase = true;
                x.Password.RequireLowercase = true;
                x.Password.RequireDigit = true;
            }).AddRoles<AppRole>().AddEntityFrameworkStores<HrElpContext>().AddDefaultTokenProviders();

            #region Services
            builder.Services.AddControllers().AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            builder.Services.AddSingleton(builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            builder.Services.AddScoped<IEmailService, EmailService>();

            builder.Services.AddTransient<IAppRoleRepository, AppRoleRepository>();
            builder.Services.AddTransient<IRequestTypeRepository, RequestTypeRepository>();
            builder.Services.AddTransient<IRequestTypeService, RequestTypeService>();

            builder.Services.AddTransient<IRequestCategoryRepository, RequestCategoryRepository>();
            builder.Services.AddTransient<IRequestCategoryService, RequestCategoryService>();

            builder.Services.AddTransient<IAppUserService, AppUserService>();
            builder.Services.AddTransient<IAppUserRepository, AppUserRepository>();

            builder.Services.AddTransient<ILeaveRequestRepository, LeaveRequestRepository>();
            builder.Services.AddTransient<ILeaveRequestService, LeaveRequestService>();

            builder.Services.AddTransient<IExpenseRequestRepository, ExpenseRequestRepository>();
            builder.Services.AddTransient<IExpenseRequestService, ExpenseRequestService>();

            builder.Services.AddTransient<IAdvanceRequestRepository, AdvanceRequestRepository>();
            builder.Services.AddTransient<IAdvanceRequestService, AdvanceRequestService>();

            builder.Services.AddTransient<IAddressService, AddressService>();
            builder.Services.AddTransient<IAddressRepository, AddressRepository>();

            builder.Services.AddScoped<AddressAPIService>();

            builder.Services.AddTransient<ICompanyService, CompanyService>();
            builder.Services.AddTransient<ICompanyRepository, CompanyRepository>();

            //AutoMapper için gerekli ayar...
            builder.Services.AddAutoMapper(x => x.AddProfile(typeof(HrELPMapper)));
            
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Login}/{id?}");

            app.Run();
        }
    }
}