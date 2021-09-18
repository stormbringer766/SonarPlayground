using System.Diagnostics.CodeAnalysis;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Sonar.Console.Infrastructure.Database;

namespace Sonar.Console
{
    [ExcludeFromCodeCoverage]
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbRepository, PostgresRepository>(s =>
                new PostgresRepository(configuration["database:connectionString"]));
            CreateTables(configuration["database:connectionString"]);
            return services;
        }

        private static void CreateTables(string connectionString)
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Execute(new CommandDefinition(CreateCustomersSql));
            connection.Execute(new CommandDefinition(CreateMetricsSql));
        }

        private static readonly string CreateCustomersSql = @"
CREATE TABLE IF NOT EXISTS public.customers (
    customer_id integer NOT NULL,
    name character varying(50) NOT NULL,
    representative_name character varying(50) NOT NULL,
    representative_email character varying(50) NOT NULL,
    representative_phone character varying(20) NOT NULL,
    CONSTRAINT customers_customer_id PRIMARY KEY (customer_id)
);";

        private static readonly string CreateMetricsSql = @"
CREATE TABLE IF NOT EXISTS public.metrics (
    id integer NOT NULL,
    customer_id integer NOT NULL,
    name character varying(50) NOT NULL,
    expression character varying(100) NOT NULL,
    CONSTRAINT metrics_id PRIMARY KEY (id)
);";
    }
}
