using Bupa.BookOwners.Api.HttpServices.Implementations;
using Bupa.BookOwners.Api.HttpServices.Interfaces;
using Bupa.BookOwners.Api.Services.Implementations;
using Bupa.BookOwners.Api.Services.Interfaces;

namespace Bupa.BookOwner.Api.Extensions
{
    public static class BookOwnerStartupExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBookOwnersService, BookOwnersService>();
            services.AddScoped<IBookOwnersHttpService, BookOwnersHttpService>();
            return services;
        }
    }
}
