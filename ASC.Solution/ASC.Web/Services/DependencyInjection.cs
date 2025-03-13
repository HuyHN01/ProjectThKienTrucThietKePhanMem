using ASC.DataAccess.Interfaces;
using ASC.DataAccess;
using ASC.Web.Configuration;
using ASC.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASC.Web.Services
{
    public static class DependencyInjection
    {
        // Cấu hình các dịch vụ
        public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration config)
        {
            // Thêm AddDbContext với connectionString để kết nối cơ sở dữ liệu
            var connectionString = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            // Thêm các dịch vụ Options và lấy dữ liệu từ appsettings.json với "AppSettings"
            services.AddOptions(); // IOOption
            services.Configure<ApplicationSettings>(config.GetSection("AppSettings"));

            return services;
        }

        // Cấu hình các dịch vụ
        public static IServiceCollection AddMyDependencyGroup(this IServiceCollection services)
        {
            // Thêm ApplicationDbContext
            services.AddScoped<DbContext, ApplicationDbContext>();

            // Cấu hình IdentityUser và IdentityRole
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Thêm các dịch vụ khác
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddSingleton<IIdentitySeed, IdentitySeed>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Add cache, session
            services.AddSession();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Thêm RazorPages và MVC
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddControllersWithViews();


            return services;
        }
    }
}
