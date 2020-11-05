using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLOG_API.DB;
using BLOG_API.Services;
using BLOG_API.Services.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BLOG_API
{
    public class Startup
    {
        private const string DefaultConnection = "DefaultConnection";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BlogDbContext>(opt =>
            opt.UseLazyLoadingProxies()
            .UseSqlServer(this.Configuration.GetConnectionString(DefaultConnection)));
            services.AddControllers();
            services.AddTransient<IUserService, UsersService>();
            services.AddTransient<IBlogService, BlogsService>();
            services.AddTransient<IPostsService, PostsService>();
            services.AddTransient<ICommentService, CommentService>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
