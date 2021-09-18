using System;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Sonar.Console.Application;

namespace Sonar.Console.Infrastructure.Database
{
    public interface IDbRepository
    {
        Task SaveAsync(Customer customer);
        Task SaveAsync(Metric metric);
    }

    public class PostgresRepository: IDbRepository
    {
        private readonly string _connectionString;

        public PostgresRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task SaveAsync(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            var parameters = new
            {
                id = customer.Id,
                name = customer.Name,
                representative_name = customer.Representative.Name,
                representative_email = customer.Representative.Email,
                representative_phone = customer.Representative.Phone
            };

            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.ExecuteAsync(new CommandDefinition(SqlExtensions.CustomerUpsert(), parameters));
        }

        public async Task SaveAsync(Metric metric)
        {
            if (metric == null)
                throw new ArgumentNullException(nameof(metric));

            var parameters = new
            {
                id = metric.Id,
                customer_id = metric.CustomerId,
                name = metric.Name,
                expression = metric.Expression
            };

            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.ExecuteAsync(new CommandDefinition(SqlExtensions.MetricUpsert(), parameters));
        }
    }
}
