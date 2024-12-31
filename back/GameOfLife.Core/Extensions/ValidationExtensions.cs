namespace GameOfLife.Core.Extensions
{
    public static class ValidationExtensions
    {
        private const string IS_REQUIRED = " is required";
        private const string IS_NOT_VALID = " is not valid";

        public static string IsRequired(this string txt)
        {
            return $"{txt} {IS_REQUIRED}";
        }

        public static string IsNotValid(this string txt)
        {
            return $"{txt} {IS_NOT_VALID}";
        }
    }
}
