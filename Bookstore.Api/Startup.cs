
using Bookstore.Application.ErrorLogger;
using Bookstore.Application.Logging;
using Bookstore.Application.UseCaseHandiling;
using Bookstore.DataAccess;
using Bookstore.Implementation.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Application.UseCases.Commands;
using Bookstore.Implementation.UseCases.Commands;
using Bookstore.API.DTO;
using Bookstore.API.Jwt;
using Bookstore.API.Jwt.TokenStorage;
using Bookstore.API.Extensions;
using Bookstore.API.Middleware;
using Bookstore.Application;
using Bookstore.Implementation.Validators;
using Bookstore.Application.UseCases.Queries;
using Bookstore.Implementation.UseCases.Queries;
using Bookstore.Application.Uploads;
using Bookstore.Implementation.Uploads;

namespace Bookstore.Implementation
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
                options.AddPolicy("AllowAll", builder =>
                {
                    builder
                           .WithOrigins("http://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();

                   
                });
            });

            var appSettings = new AppSettings();
            Configuration.Bind(appSettings);

            services.AddTransient<ITokenStorage, InMemoryTokenStorage>();

            services.AddTransient<JwtManager>(x =>
            {
                var context = x.GetService<BookstoreContext>();
                var tokenStorage = x.GetService<ITokenStorage>();
                return new JwtManager(context, appSettings.Jwt.Issuer, appSettings.Jwt.SecretKey, appSettings.Jwt.DurationSeconds, tokenStorage);
            });

            services.AddJwt(appSettings);

            services.AddTransient<BookstoreContext>(x =>
            {
                DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
                builder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=Bookstore;Integrated Security=True");
                return new BookstoreContext(builder.Options);
            });

            services.AddLogger();
            services.AddValidators();

            services.AddTransient<QueryHandler>();

            services.AddTransient<ICommandHandler, CommandHandler>();

            services.AddTransient<ICreateBookCommand, EfCreateBookCommand>();
            services.AddTransient<IUpdateBookCommand, EfUpdateBookCommand>();
            services.AddTransient<IDeleteBookCommand, EfDeleteBookCommand>();

            services.AddTransient<ICreateAuthorCommand, EfCreateAuthorCommand>();
            services.AddTransient<IUpdateAuthorCommand, EfUpdateAuthorCommand>();
            services.AddTransient<IDeleteAuthorCommand, EfDeleteAuthorCommand>();

            services.AddTransient<ICreatePublisherCommand, EfCreatePublisherCommand>();
            services.AddTransient<IUpdatePublisherCommand, EfUpdatePublisherCommand>();
            services.AddTransient<IDeletePublisherCommand, EfDeletePublisherCommand>();

            services.AddTransient<ICreateGenreCommand, EfCreateGenreCommand>();
            services.AddTransient<IUpdateGenreCommand, EfUpdateGenreCommand>();
            services.AddTransient<IDeleteGenreCommand, EfDeleteGenreCommand>();

        

            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
            services.AddTransient<IUpdateUserCommand, EfUpdateUserCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();


            services.AddTransient<ICreateBookPublisherCommand, EfCreateBookPublisherCommand>();
            services.AddTransient<IUpdateBookPublisherCommand, EfUpdateBookPublisherCommand>();
            services.AddTransient<IDeleteBookPublisherCommand, EfDeleteBookPublisherCommand>();


            services.AddTransient<ICreateUserCartCommand, EfCreateUserCartCommand>();
            services.AddTransient<IUpdateUserCartCommand, EfUpdateUserCartCommand>();
            services.AddTransient<IDeleteCartCommand, EfDeleteCartCommand>();

            services.AddTransient<ICreateOrderCommand, EfCreateOrderCommand>();
            services.AddTransient<IUpdateOrderCommand, EfUpdateOrderCommand>();
            services.AddTransient<IDeleteOrderCommand, EfDeleteOrderCommand>();

            services.AddTransient<IUseCaseLogger, EfUseCaseLogger>();

            services.AddTransient<IFindGenreQuery, EfFindGenreQuery>();
            services.AddTransient<IBase64FileUploader, Base64FileUploader>();
            services.AddTransient<IFindPublisherQuery, EfFindPublisherQuery>();
            services.AddTransient<IFindUserQuery, EfFindUserQuery>();
            services.AddTransient<IFindAuthorQuery, EfFindAuthorQuery>();
            services.AddTransient<IFindBookQuery, EfFindBookQuery>();
            services.AddTransient<IFindUserCartQuery, EfFindCartQuery>();
            services.AddTransient<IFindOrderQuery, EfFindOrderQuery>();
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
            services.AddTransient<IGetAuthorsQuery, EfGetAuthorsQuery>();
            services.AddTransient<IGetGenresQuery, EfGetGenresQuery>();
            services.AddTransient<IGetPublishersQuery, EfGetPublishersQuery>();
            services.AddTransient<IGetBooksQuery, EfGetBooksQuery>();
            services.AddTransient<IGetBooksAdminQuery, EfGetBooksAdminQuery>();

            services.AddTransient<IGetBookPublishersQuery, EfGetBookPublishersQuery>();
            services.AddTransient<IGetCartsQuery, EfGetCartsQuery>();
            services.AddTransient<IGetOrdersQuery, EfGetOrdersQuery>();
            services.AddHttpContextAccessor();

            services.AddScoped<IApplicationActor>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                var header = accessor.HttpContext.Request.Headers["Authorization"];

                var data = header.ToString().Split("Bearer ");

                if (data.Length < 2)
                {
                    return new UnauthorizedActor();
                }

                var handler = new JwtSecurityTokenHandler();

                var tokenObj = handler.ReadJwtToken(data[1].ToString());

                var claims = tokenObj.Claims;

                var email = claims.First(x => x.Type == "Email").Value;
                var id = claims.First(x => x.Type == "Id").Value;
                var username = claims.First(x => x.Type == "Username").Value;
                var useCases = claims.First(x => x.Type == "UseCases").Value;

                List<int> useCaseIds = JsonConvert.DeserializeObject<List<int>>(useCases);

                return new JwtActor
                {
                    Email = email,
                    AllowedUseCases = useCaseIds,
                    Id = int.Parse(id),
                    Username = username,
                };
            });

          
            services.AddControllers();

            services.AddTransient<IQueryHandler>(x =>
            {
                var actor = x.GetService<IApplicationActor>();
                var logger = x.GetService<IUseCaseLogger>();
                var queryHandler = new QueryHandler();
                var timeTrackingHandler = new TimeTrackingQueryHandeler(queryHandler);
                var loggingHandler = new LoggingQueryHanler(timeTrackingHandler, actor, logger);
                var decoration = new AuthorizationQueryHanler(actor, loggingHandler);

                return decoration;
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bookstore.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAll");
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bookstore.API v1"));
            
           

            app.UseRouting();

            
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ExceptionHanlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           
        }
    }
}
