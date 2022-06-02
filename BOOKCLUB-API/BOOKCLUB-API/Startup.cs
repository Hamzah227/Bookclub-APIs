using BOOKCLUB_API.IRepository;
using BOOKCLUB_API.Models;
using BOOKCLUB_API.SqlRepo;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BOOKCLUB_API
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
            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("AllowAllHeaders", policy =>
                {
                    
                    policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                    //policy.WithOrigins("http://localhost:5003")
                    // .AllowAnyHeader()
                    // .AllowAnyMethod();
                });
            });

            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile(@"./bookclub-9b443-firebase-adminsdk-442s3-899ebd10e3.json")
            });

            services.AddMvc()
            .AddJsonOptions(options => {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });

            services.AddControllers().AddNewtonsoftJson();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BOOKCLUB_API", Version = "v1" });
                //var fileName = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var filePath = System.IO.Path.Combine(AppContext.BaseDirectory, fileName);
                //c.IncludeXmlComments(filePath);
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.Authority = "https://securetoken.google.com/bookclub-9b443";
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidIssuer = "https://securetoken.google.com/bookclub-9b443",
                       ValidateAudience = true,
                       ValidAudience = "bookclub-9b443",
                       ValidateLifetime = true
                   };
               });
            services.AddDbContext<BOOKCLUBFYPContext>(item => item.UseSqlServer("Password=sheheryar$$032;Persist Security Info=True;User ID=sheheryar321;Initial Catalog=bookClub;Data Source=66.165.248.146;"));
            //services.AddDbContext<BOOKCLUBFYPContext>(item => item.UseSqlServer("Password=123;Persist Security Info=True;User ID=sa;Initial Catalog=master;Data Source=LENOVO-IDEAPAD-"));
            services.AddScoped<IUserRepository, SqlUserRepository>();
            services.AddScoped<IAppUtill, SqlAppUtill>();
            services.AddScoped<IRequest, SqlRequestRepository>();
            //services.AddScoped<IGoogleMapsApi, GoogleMapsRepo>();
            services.AddScoped<IRider, SqlRiderRepo>();
            services.AddScoped<IFirebaseValidate, Controllers.Helper.FirebaseValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BOOKCLUB_API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("AllowAllHeaders");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
