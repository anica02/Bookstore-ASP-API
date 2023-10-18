using Bookstore.Application.UseCases.DTO;

using Bookstore.API.ErrorLogger;
using Bookstore.Application.ErrorLogger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.API.DTO;
using Bookstore.Implementation.Validators;
using Bookstore.API.Jwt;

namespace Bookstore.API.Extensions
{
    public static class ContainerExtensions
    {
        public static void AddLogger(this IServiceCollection services)
        {
            services.AddTransient<IErrorLogger>(x =>
            {
                var accesor = x.GetService<IHttpContextAccessor>();

                if (accesor == null || accesor.HttpContext == null)
                {
                    return new ConsoleErrorLogger();
                }

                var logger = accesor.HttpContext.Request.Headers["Logger"].FirstOrDefault();

                return new ConsoleErrorLogger();
            });
        }



        public static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<CreateBookValidator>();
     
            services.AddTransient<CreateBookPriceValidator>();
            services.AddTransient<CreateBookDiscountValidator>();
            services.AddTransient<CreateImageValidator>();
            services.AddTransient<CreateBookPublisherValidator>();
            services.AddTransient<CreateBookPublisher2Validator>();
            services.AddTransient<UpdateBookValidator>();
            services.AddTransient<UpdatePublisherValidator>();
            services.AddTransient<UpdateAuthorValidator>();
            services.AddTransient<UpdateBookPublisherValidator>();
            services.AddTransient<UpdateBookDiscountValidator>();
            services.AddTransient<UpdateImageValidator>();
            services.AddTransient<UpdateUserValidator>();
            services.AddTransient<BookAuthorValidator>();
            services.AddTransient<BookGenreValidator>();
    
            services.AddTransient<CreateUserValidator>();
            services.AddTransient<CreateAuthorValidator>();
            services.AddTransient<CreatePublisherValidator>();
            services.AddTransient<CreateGenreValidator>();
            services.AddTransient<CreateUserCartValidator>();
            services.AddTransient<CreateUserCartItemValidator>();
            services.AddTransient<UpdateAuthorValidator>();
            services.AddTransient<CreateOrderValidator>();
          
        }

        public static void AddJwt(this IServiceCollection services, AppSettings settings)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = settings.Jwt.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Jwt.SecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                cfg.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        //Authorization header
                        var header = context.Request.Headers["Authorization"];

                        var token = header.ToString().Split("Bearer ")[1];

                        var handler = new JwtSecurityTokenHandler();

                        var tokenObj = handler.ReadJwtToken(token);

                        string jti = tokenObj.Claims.FirstOrDefault(x => x.Type == "jti").Value;


                        //ITokenStorage

                        ITokenStorage storage = context.HttpContext.RequestServices.GetService<ITokenStorage>();

                        bool isValid = storage.TokenExists(jti);

                        if (!isValid)
                        {
                            context.Fail("Token is not valid.");
                        }

                        return Task.CompletedTask;
                    }
                };
            });
        }

    }

}
