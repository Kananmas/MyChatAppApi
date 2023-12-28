using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyChatAppApi.Context;
using MyChatAppApi.MainHub;
using MyChatAppApi.Repository.Interfaces;
using MyChatAppApi.Repository.Services;
using MyChatAppApi.Utilites;

namespace MyChatAppApi
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
            services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            services.AddSingleton<ChatHubContext>();
            services.AddScoped<CommonUtillites>();
            services.AddScoped<IGroupSubscribtionRepositoryService, GroupSubscribtionRepositoryService>();
            services.AddScoped<IMessageRepositoryService, MessageRepositoryService>();
            services.AddScoped<IRoomRepositoryService, RoomRepositoryService>();
            services.AddScoped<IUserRepositoryService, UserRepositoryService>();

            services.AddControllers();
            services.AddSignalR();
            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyChatAppApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
         


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowAnyOrigin();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyChatAppApi v1"));

         

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseEndpoints(endpoint =>
            {
                endpoint.MapHub<ChatHub>("/chatHub");
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
