namespace GameOfLife.Core.Extensions
{
    public static class StringExtensions
    {
        public static string OnlyNumbers(this string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? value
                : new string(value.Where(c => char.IsDigit(c)).ToArray());
        }

        public static bool IsEmpty(this string value)
            => string.IsNullOrWhiteSpace(value);
    }
}
