namespace Sonar.Console.Infrastructure.MetricsApi
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MetricsResponse
    {
        public int id { get; set; }
        public int customer_id { get; set; }
        public string name { get; set; }
        public string expression { get; set; }
    }
}
