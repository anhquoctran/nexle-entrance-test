using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NexleInterviewTesting.Infrastructure.DatabaseContexts;
using System.Text;

namespace NexleInterviewTesting.Api
{
    public static class AuthConfigure
    {
        /// <summary>
        /// Configure custom identity auth
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureCustomAuth(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentity<User, IdentityRole<int>>(c =>
            {
                c.SignIn.RequireConfirmedPhoneNumber = false;
                c.SignIn.RequireConfirmedEmail = false;
                c.SignIn.RequireConfirmedAccount = false;
                c.User.RequireUniqueEmail = true;

            })
                .AddEntityFrameworkStores<NexleDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(c =>
            {
                c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                c.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(c =>
                {
                    c.Audience = config["Authentication:JwtBearer:Audience"];
                    c.SaveToken = true;
                    c.RequireHttpsMetadata = false;

                    c.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["Authentication:JwtBearer:Securitykey"])),

                        ValidateIssuer = true,
                        ValidIssuer = config["Authentication:JwtBearer:Issuer"],

                        ValidAudience = config["Authentication:JwtBearer:Audience"],

                        ValidateLifetime = true,

                        ClockSkew = TimeSpan.Zero,
                    };

                    c.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async (context) =>
                        {
                            var principal = context.Principal;
                            var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<User>>();

                            var validatedUser = await signInManager.ValidateSecurityStampAsync(principal);

                            if (validatedUser == null)
                            {
                                context.Fail("Unauthorized user login attempt");
                                context.Result.Failure.HResult = 100;
                            }

                            context.Success();
                        },

                    };

                });

            return services;
        }
    }
}
