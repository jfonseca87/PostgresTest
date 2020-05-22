using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
// using PostgresTest.ADORepository;
using PostgresTest.DapperRepository;
//using PostgresTest.EFCoreRepository;
using PostgresTest.Business.Classess;
using PostgresTest.Business.Interfaces;
using PostgresTest.Domain.Models;
using PostgresTest.Repository.Interfaces;

namespace PostgresTest.Client
{
    public class Startup
    {
        private readonly IConfiguration _configutarion;

        public Startup(IConfiguration configuration)
        {
            _configutarion = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //services.AddDbContext<PgContext>(options => options.UseNpgsql(_configutarion.GetConnectionString("PgConnection")));

            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();

            services.AddTransient<IPostBusiness, PostBusiness>();
            services.AddTransient<ICommentBusiness, CommentBusiness>();

            services.Configure<SettingsValues>(options =>
            {
                options.ConnectionString = _configutarion.GetConnectionString("PgConnection");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default", 
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
