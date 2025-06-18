using Microsoft.Extensions.DependencyInjection;
using Repository.IRepository;
using Repository.Repositories;
using Microsoft.AspNetCore.OData;
using System.Reflection;


namespace Service.Extensions
{
    public static class ServiceExtensions
    {

        public static void AddRepoBase(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblies(Assembly.Load("Repository"))
                .AddClasses(classes => classes
                    .Where(type => type.Namespace == "Repository.Repositories" && type.Name.EndsWith("Repo")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }

        public static void ConfigureServices(this IServiceCollection services)
        {

            services.Scan(scan => scan
            .FromAssemblies(Assembly.Load("Services"))
            .AddClasses(classes => classes
                .Where(type => type.Namespace == "Services.Services" && type.Name.EndsWith("Service")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
           

        }
    }
}
