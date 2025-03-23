using AuthenticationService.Domain.Errors;
using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Domain.ValueObjects.User
{
    public class Email : ValueObject
    {
        private const string EMAIL_PATTREN = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        public string Value { get; }

        protected Email()
        {

        }

        protected Email(string email)
        {
            Value = email;
        }

        public static IExecutionResult<Email> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return ExecutionResult.Failure<Email>(UserError.EmailNotBeEmpty());

            if (!Regex.IsMatch(email, EMAIL_PATTREN))
                return ExecutionResult.Failure<Email>(UserError.EmailNotValid());

            return ExecutionResult.Success(new Email(email));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
