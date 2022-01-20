using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MessageBoard.Models;
using Microsoft.AspNetCore.Identity;

namespace MessageBoard
{
    public class Startup
    {
    public Startup(IWebHostEnvironment env)
    {
        var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json");
        Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; set; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();

        services.AddEntityFrameworkMySql()
        .AddDbContext<MessageBoardContext>(options => options
        .UseMySql(Configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(Configuration["ConnectionStrings:DefaultConnection"])));

        services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<MessageBoardContext>()
        .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 0;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredUniqueChars = 0;
        });
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseDeveloperExceptionPage();

        app.UseAuthentication(); 

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(routes =>
        {
        routes.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
        });

        app.UseStaticFiles();

        app.Run(async (context) =>
        {
        await context.Response.WriteAsync("Hello World!");
        });
    }
    }
}
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using Microsoft.EntityFrameworkCore;
// // using Microsoft.OpenApi.Models; (lesson 4 note)
// using MessageBoard.Models;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Http;

// namespace MessageBoard
// {
//     public class Startup
//     {
//         public Startup(IConfiguration configuration)
//         {
//             Configuration = configuration;
//         }

//         public IConfiguration Configuration { get; }

//         // This method gets called by the runtime. Use this method to add services to the container.
//         public void ConfigureServices(IServiceCollection services)
//         {

//             services.AddDbContext<MessageBoardContext>(opt =>
//                 opt.UseMySql(Configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(Configuration["ConnectionStrings:DefaultConnection"])));
//             services.AddControllers();
//         }

//         // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//         public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//         {
//             if (env.IsDevelopment())
//             {
//                 app.UseDeveloperExceptionPage();
//             }

//             // app.UseHttpsRedirection();

//             app.UseRouting();

//             app.UseAuthorization();

//             app.UseEndpoints(endpoints =>
//             {
//                 endpoints.MapControllers();
//             });
//         }
//     }
// }