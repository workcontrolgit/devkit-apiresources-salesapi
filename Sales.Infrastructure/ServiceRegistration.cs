using Microsoft.Extensions.DependencyInjection;
using Sales.Application.Interfaces;
using Sales.Infrastructure.Repository;
using SqlKata.Compilers;
using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Sales.Application.Constants;
using SqlKata.Execution;

namespace Sales.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //SQLKata DI Container https://sqlkata.com/docs/
            services.AddScoped(factory =>
            {
                return new QueryFactory
                {
                    Compiler = new SqlServerCompiler(),
                    Connection = new SqlConnection(configuration[ConfigurationConstants.ConnectionString]),
                    Logger = compiled => Console.WriteLine(compiled)
                };
            });


        }
    }
}
