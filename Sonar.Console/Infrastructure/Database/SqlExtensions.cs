using Sonar.Console.Application;

namespace Sonar.Console.Infrastructure.Database
{
    public static class SqlExtensions
    {
        public static string CustomerUpsert()
        {
            return
                @"INSERT INTO customers (customer_id, name, representative_name, representative_email, representative_phone)
                    VALUES(@id, @name, @representative_name, @representative_email, @representative_phone) 
                    ON CONFLICT (customer_id) 
                    DO 
                       UPDATE SET 
                           name = @name,
                           representative_name = @representative_name, 
                           representative_email = @representative_email, 
                           representative_phone = @representative_phone";
        }

        public static string MetricUpsert()
        {
            return
                @"INSERT INTO metrics (id, customer_id, name, expression)
                    VALUES(@id, @customer_id, @name, @expression) 
                    ON CONFLICT (id) 
                    DO 
                       UPDATE SET 
                           customer_id = @customer_id,
                           name = @name,
                           expression = @expression";
        }
    }
}
