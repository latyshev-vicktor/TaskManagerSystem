using AuthenticationService.Domain.Errors;
using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Domain.ValueObjects.User
{
    public class Phone : ValueObject
    {
        private const string PATTERN = @"^\+7\d{10}$";
        public const int MAX_LENGHT_PHONE_NUMBER = 12;

        public string PhoneNumber { get; }

        protected Phone()
        {

        }

        protected Phone(string phoneNumber)
            => PhoneNumber = phoneNumber;

        public static IExecutionResult<Phone> Create(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return ExecutionResult.Failure<Phone>(UserError.PhoneNotBeEmpty());

            if (!Regex.IsMatch(phoneNumber, PATTERN))
                return ExecutionResult.Failure<Phone>(UserError.IncorrectPhone());

            if (phoneNumber.Length > MAX_LENGHT_PHONE_NUMBER)
                return ExecutionResult.Failure<Phone>(UserError.PhoneMaxLenght());

            return ExecutionResult.Success(new Phone(phoneNumber));
        }

        protected override IEnumerable<string> GetEqualityComponents()
        {
            yield return PhoneNumber;
        }
    }
}
