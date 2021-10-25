using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Store.Sts.Background;
using Store.Sts.Data;
using Store.Sts.Models;
using System;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Store.Sts
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Store.STS", Version = "v1" });
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {   
                options.UseNpgsql();
                options.UseOpenIddict();
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = Claims.Role;
                options.ClaimsIdentity.EmailClaimType = Claims.Email;
            });

            services.AddOpenIddict()
                .AddCore(options => { options.UseEntityFrameworkCore().UseDbContext<ApplicationDbContext>(); })
                .AddServer(options =>
                {
                    options.UseDataProtection()
                       .PreferDefaultAccessTokenFormat()
                       .PreferDefaultRefreshTokenFormat()
                       .PreferDefaultUserCodeFormat();
                    // ¬ключение нужных маршрутов
                    options
                        .SetTokenEndpointUris("/connect/token")
                        .SetIntrospectionEndpointUris("/connect/introspect")
                        .SetUserinfoEndpointUris("/connect/userinfo");
                    options
                        .AllowPasswordFlow()
                        .AllowRefreshTokenFlow()
                        .AllowCustomFlow("verification_token");
                    options
                        .AddEphemeralEncryptionKey()
                        .AddEphemeralSigningKey()
                        .DisableAccessTokenEncryption();
                    options.UseReferenceAccessTokens()
                        .UseReferenceRefreshTokens();
                    options.SetAccessTokenLifetime(TimeSpan.FromMinutes(30));
                    options.SetRefreshTokenLifetime(TimeSpan.FromMinutes(60));
                    options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles);
                    options.UseAspNetCore()
                       .EnableTokenEndpointPassthrough()
                       .EnableUserinfoEndpointPassthrough()
                       .DisableTransportSecurityRequirement();
                })
                .AddValidation(options =>
                {
                    options.UseLocalServer();
                    options.UseAspNetCore();
                    options.UseDataProtection();
                });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = Schemes.Bearer;
                options.DefaultChallengeScheme = Schemes.Bearer;
            });
            services.AddHostedService<TestData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Store.STS v1"));
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
