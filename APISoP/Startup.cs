using ApiSoP.RegisterServices.RegisterServices;
using APISoP.Config.JWT;
using APISoP.Models;
using APISoP.UsersAuthManager.Services;
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

namespace APISoP
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
            //register context on library data
            RegisterContextData.AddRegistration(services, Configuration.GetConnectionString("SalesPoint") );
            
            //register service context for identity / Presentation
            services.AddDbContext<APIAuthDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SalesPoint"))
            );

            //register jwt services config
            #region jwtServices
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            var secret = Configuration["JwtConfig:Secret"];

            services.AddSingleton<TokenValidationParameters>();
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt => {
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new ParametersTokens(secret).TokenValidationParameters;
            });

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<APIAuthDbContext>();

            services.AddScoped<ITokenManager, TokenManager>();

            #endregion
            //end register services config jwt

            services.AddControllers();
            
            //register all services
            IOCRegister.AddRegistration(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "APISoP", Version = "v1" });
                //added for jwt token to request
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Auth header"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
                //end added for jwt token to request
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "APISoP v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            //add for auth users
            app.UseAuthentication();
            //
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
