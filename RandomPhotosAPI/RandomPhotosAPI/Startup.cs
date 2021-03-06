using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RandomPhotosAPI.Database;
using RandomPhotosAPI.Services;
using RandomPhotosAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomPhotosAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllers();
            services.AddSwaggerGen();

            services.AddScoped<IPhotoHistoryService, PhotoHistoryService>();
            services.AddSingleton<IRandomPhotoService>(t =>
                    new RedditRandomPhotoService(new RedditConnectionData
                    {
                        ClientID = Configuration.GetValue<string>("AppIdentitySettings:RedditAPIClientID"),
                        SecretKey = Configuration.GetValue<string>("AppIdentitySettings:RedditAPISecretKey"),
                        UserName = Configuration.GetValue<string>("AppIdentitySettings:RedditUserName"),
                        Password = Configuration.GetValue<string>("AppIdentitySettings:RedditPassword"),
                        RedditAccessTokenUriStr = Configuration.GetValue<string>("RedditAccessTokenUrl"),
                        Subreddit = Configuration.GetValue<string>("RedditSubreddit")
                    }));

            services.AddDbContext<RandomPhotosDBContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
