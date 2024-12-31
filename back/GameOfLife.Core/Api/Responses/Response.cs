using FluentValidation.Results;
using GameOfLife.Core.UseCases.Outputs.Exceptions;
using static GameOfLife.Core.Api.Constants.ResponseConstants;

namespace GameOfLife.Core.Api.Responses
{
    public class Response<T> where T : notnull
    {
        private readonly List<string> _messages;
        private readonly List<string> _errorMessages;
        private readonly Dictionary<int, string> _errorCodeMessages;

        public Response(T result, bool isSuccess = true)
        {
            IsSuccess = isSuccess;
            _messages = new List<string>();
            _errorMessages = new List<string>();
            _errorCodeMessages = new Dictionary<int, string>();

            AddResult(result);
        }

        public Response(ValidationResult validationResult)
        {
            _errorMessages = new List<string>();
            _messages = new List<string>();
            _errorCodeMessages = new Dictionary<int, string>();

            ProcessValidationResults(validationResult);
        }

        public Response(IEnumerable<ValidationResult> validationResults)
        {
            _errorMessages = new List<string>();
            _messages = new List<string>();
            _errorCodeMessages = new Dictionary<int, string>();

            ProcessValidationResults(validationResults.ToArray());
        }

        public Response(IEnumerable<string> messages, bool isSuccess)
        {
            IsSuccess = isSuccess;
            _errorMessages = new List<string>();
            _messages = new List<string>();
            _errorCodeMessages = new Dictionary<int, string>();

            ProcessMessageResults(messages);
        }

        public Response(IEnumerable<string> errorMessages) : this(errorMessages, false)
        {
        }

        public Response(IReadOnlyDictionary<int, string> errorCodeMessages)
        {
            _errorMessages = new List<string>();
            _messages = new List<string>();
            _errorCodeMessages = new Dictionary<int, string>();

            AddErrorCodeMessages(errorCodeMessages);
        }

        private void ProcessValidationResults(params ValidationResult[] validationResults)
        {
            foreach (var validationResult in validationResults)
            {
                CheckValidationResult(validationResult);
                AddValidationResult(validationResult);
            }

            VerifyValidity();
        }

        private void ProcessMessageResults(IEnumerable<string> messages)
        {
            if (IsSuccess)
            {
                _messages.AddRange(messages);
                return;
            }

            _errorMessages.AddRange(messages);
        }

        public virtual IReadOnlyCollection<string> ErrorMessages => _errorMessages?.AsReadOnly();

        public virtual IReadOnlyCollection<string> Messages => _messages?.AsReadOnly();

        public virtual IReadOnlyDictionary<int, string> ErrorCodeMessages => _errorCodeMessages;


        public bool IsSuccess { get; private set; }

        private void VerifyValidity() => IsSuccess = ErrorCodeMessages.Count == 0 && ErrorMessages.Count == 0;

        public T Result { get; private set; }

        public void AddErrorMessage(string message)
        {
            AddErrorMessages(message);
            VerifyValidity();
        }

        public void AddErrorMessages(params string[] messages)
        {
            foreach (string message in messages)
            {
                VerifyErrorMessage(message);

                _errorMessages.Add(message);
            }

            VerifyValidity();
        }

        public void AddMessage(string message) => AddMessages(message);

        public void AddMessages(params string[] messages)
        {
            foreach (var message in messages)
            {
                VerifyMessage(message);

                _messages.Add(message);
            }
        }

        public void AddErrorCodeMessage(int errorCode, string errorMessage)
        {
            VerifyErrorMessage(errorMessage);
            _errorCodeMessages.Add(errorCode, errorMessage);
            VerifyValidity();
        }

        public void AddErrorCodeMessages(IReadOnlyDictionary<int, string> errorCodeMessages)
        {
            foreach (var kvp in errorCodeMessages)
            {
                AddErrorCodeMessage(kvp.Key, kvp.Value);
            }
        }

        public void AddResult(T result) => Result = result ?? throw new ResultNullException(ResultNullMessage);

        public void AddValidationResult(ValidationResult validationResult)
        {
            IsSuccess = validationResult.IsValid;
            VerifyErrorMessages(validationResult);
        }

        private static void CheckValidationResult(ValidationResult validationResult)
        {
            if (validationResult == null)
            {
                throw new ValidationResultNullException(ValidationResultNullMessage);
            }
        }

        private static void VerifyErrorMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ErrorMessageNullOrEmptyException(ErrorMessageIsNullOrEmptyMessage);
            }
        }

        private static void VerifyMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new MessageNullOrEmptyException(MessageIsNullOrEmptyMessage);
            }
        }

        private void VerifyErrorMessages(ValidationResult validationResult)
        {
            _errorMessages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
    }

    public class Response : Response<object>
    {
        public Response(ValidationResult validationResult) : base(validationResult) { }
        public Response(IEnumerable<ValidationResult> validationResults) : base(validationResults) { }
        public Response(IEnumerable<string> errorMessages) : base(errorMessages) { }
        public Response(IEnumerable<string> messages, bool isSuccess) : base(messages, isSuccess) { }
        public Response(object result, bool isSuccess = true) : base(result, isSuccess) { }
        public Response(IReadOnlyDictionary<int, string> errorCodeMessages) : base(errorCodeMessages) { }
        public T GetResult<T>() => (T)Result;
    }
}
