using LibraryManagement.Application.QueryServices;
using LibraryManagement.Application.Services;
using LibraryManagement.Domain.BookContext.Repositories;
using LibraryManagement.Domain.Common;
using LibraryManagement.Domain.MemberContext.Repositories;
using LibraryManagement.Infrastructure.Common;
using LibraryManagement.Infrastructure.Data;
using LibraryManagement.Infrastructure.Data.Repositories;
using LibraryManagement.Infrastructure.QueryServices;
using LibraryManagement.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationManager configuration)
        {
            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILibraryService, LibraryService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IPasswordHashingService, PasswordHashingService>();
            services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IBookQueryService, BookQueryService>();
            services.AddScoped<IMemberQueryService, MemberQueryService>();
            services.AddScoped<IPublisher, MediatRPublisher>();
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
            return services;
        }
    }
}
