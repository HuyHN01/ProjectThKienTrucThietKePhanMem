﻿using ASC.DataAccess.Interfaces;
using ASC.DataAccess;
using ASC.Web.Configuration;
using ASC.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ASC.Business.Interfaces;
using ASC.Web.Areas.Configuration.Models;
using ASC.Business;

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


            //Using a gmail authentication provider for customer authentication
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    IConfigurationSection googleAuthNSection = config.GetSection("Authentication:Google");
                    options.ClientId = config["Google:Identity:ClientId"];
                    options.ClientSecret = config["Google:Identity:ClientSecret"];

                    if (string.IsNullOrEmpty(options.ClientId) || string.IsNullOrEmpty(options.ClientSecret))
                    {
                        throw new InvalidOperationException("Chưa cấu hình Google ClientId hoặc ClientSecret.");
                    }
                });

            //services.AddDistributedMemoryCache();
            services.AddStackExchangeRedisCache(options => 
            {
                options.Configuration = config.GetSection("CacheSettings:CacheConnectionString").Value;
                options.InstanceName = config.GetSection("CacheSettings:CacheInstance").Value;
            });
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

            //Add service NavigationCacheOperations và Cache => cập nhật class DependencyInjection => phương thức AddMyDependencyGroup
            services.AddDistributedMemoryCache();
            services.AddSingleton<INavigationCacheOperations, NavigationCacheOperations>();

            //Add service MasterDataCacheOperations trong method AddMyDependencyGroup thuộc lớp DependencyInjection (Lab 7, trang 4)
            services.AddScoped<IMasterDataCacheOperations, MasterDataCacheOperations>();
            services.AddScoped<IServiceRequestOperations, ServiceRequestOperations>();

            // Thêm RazorPages và MVC
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddControllersWithViews();

            //Lab số 6: Bước 4
            services.AddScoped<IMasterDataOperations, ASC.Business.MasterDataOperations>();
            services.AddAutoMapper(typeof(ApplicationDbContext));
            services.AddControllersWithViews().AddJsonOptions(options => {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            return services;
        }
    }
}
