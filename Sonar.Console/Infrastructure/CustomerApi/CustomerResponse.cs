namespace Sonar.Console.Infrastructure.CustomerApi
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CustomerResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public string representative { get; set; }
        public string representative_email { get; set; }
        public string representative_phone { get; set; }
    }
}
