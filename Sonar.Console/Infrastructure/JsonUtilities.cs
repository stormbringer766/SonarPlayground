using Newtonsoft.Json;

namespace Sonar.Console.Infrastructure
{
    public static class JsonUtilities
    {
        public static T FromJson<T>(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return default;

            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
