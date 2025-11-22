using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Mooditor.Api.Data;
using Mooditor.Api.Repositories;
using Mooditor.Api.Services;

namespace Mooditor.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterRepositoriesAndServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMoodEntryRepository, MoodEntryRepository>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMoodEntryService, MoodEntryService>();

            services.AddScoped<IPasswordHasher<Mooditor.Api.Models.User>, PasswordHasher<Mooditor.Api.Models.User>>();

            return services;
        }
    }
}
