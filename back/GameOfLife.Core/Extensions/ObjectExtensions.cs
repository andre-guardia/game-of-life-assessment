using System.Text.Json;

namespace GameOfLife.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object obj)
        {
            if (obj == null)
                return string.Empty;

            return JsonSerializer.Serialize(obj);
        }
    }
}
