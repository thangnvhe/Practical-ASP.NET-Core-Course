using Lesson01_API.Repositories.Implementations;
using Lesson01_API.Repositories.Interfaces;
using Lesson01_API.Services.Implementations;
using Lesson01_API.Services.Interfaces;

namespace Lesson01_API.Configurations
{
    public static class DIConfiguration
    {
        public static void AddDependenceInjections(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryService, CategoryService>();
        }
    }
}
