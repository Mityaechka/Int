using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intemotion.Data;
using Intemotion.Entities;
using Intemotion.Enums;
using Intemotion.Hubs;
using Intemotion.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Intemotion.Configs;
using Intemotion.SystemNotifications;
using Intemotion.Models;

namespace Intemotion
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
           // var builder = new ConfigurationBuilder()
           //.SetBasePath(env.ContentRootPath)
           //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                                        builder.SetIsOriginAllowed(_ => true)
                                        .WithOrigins("https://localhost:42001", "http://192.168.0.220:3313", "https://192.168.0.220:451")
                                        .AllowAnyMethod()
                                        .AllowAnyHeader()
                                        .AllowCredentials());

                //options.AddPolicy("Angular",builder =>builder.WithOrigins("http://localhost:4200")
                //.AllowAnyMethod()
                //.AllowAnyOrigin()
                //.AllowAnyHeader()
                //.AllowCredentials());
            });
                    
            services.AddSignalR(o=>{
                o.EnableDetailedErrors = true;
            });

            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<Entities.User, IdentityRole>(options=> {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<DataContext>();

            services.AddControllersWithViews();

            services.AddControllers()
        .AddNewtonsoftJson();

            services.AddScoped<GameProcessContext>();


            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGameProcessService, GameProcessService>();
            services.AddScoped<ISponsorBannerService, SponsorBannerService>();
            services.AddScoped<IFileService, FileService>();


            services.AddSingleton<SystemNotification>();


            services.AddOptions();
            services.Configure<RoundConfig>(Configuration.GetSection("RoundConfig"));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,SystemNotification systemNotification)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHub<GameProcessHub>("/game");
                endpoints.MapHub<NotificationHub>("/notification");
                endpoints.MapHub<CameraHub>("/camera");

            });
            app.Use((context, next) =>
            {
                context.Response.Headers["Access-Control-Allow-Origin"] = "*";
                return next.Invoke();
            });
            systemNotification.RegisterEvent(new SystemEvent
            {
                Name = "SendMessage",
                Action = async (provider, o) =>
                {
                    var gameProcessContext = provider.GetService<GameProcessContext>();
                    await gameProcessContext.SendChatMessage((int)o);
                }
            });

            systemNotification.RegisterEvent(new SystemEvent
            {
                Name = "SendHubEvent",
                Action = async (provider, o) =>
                {
                    var data = ((dynamic)o);
                    string eventName = data.eventName;
                    ServiceResult<object> result = data.result;

                    var gameProcessContext = provider.GetService<GameProcessContext>();
                    await gameProcessContext.SendEvent(eventName,result);
                }
            });
        }
    }
}
