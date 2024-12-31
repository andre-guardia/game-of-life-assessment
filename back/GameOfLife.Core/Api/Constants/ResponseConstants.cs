namespace GameOfLife.Core.Api.Constants
{
    public static class ResponseConstants
    {
        public static string ErrorMessageIsNullOrEmptyMessage => "Error while trying to add string to ErrorMessage Collection. It is null or empty, please verify.";
        public static string MessageIsNullOrEmptyMessage => "Error while trying to add string to Message Collection. It is null or empty, please verify.";
        public static string ResultNullMessage => "Result object is null, please verify.";
        public static string ValidationResultNullMessage => "ValidationResult is null, please verify.";
        public static string ErrorMessageWhenOccurExceptionInRequest => "An error occurred during the request. Please see the application logs";
    }
}
