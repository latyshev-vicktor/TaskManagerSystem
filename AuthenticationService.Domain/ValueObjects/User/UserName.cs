using AuthenticationService.Domain.Errors;
using CSharpFunctionalExtensions;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Domain.ValueObjects.User
{
    public class UserName : ValueObject
    {
        public const int USER_NAME_MAX_LENGHT = 20;
        public const int USER_NAME_MIN_LENGHT = 5;

        public string Value { get; }

        protected UserName()
        {

        }

        protected UserName(string userName)
            => Value = userName;

        public static IExecutionResult<UserName> Create(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return ExecutionResult.Failure<UserName>(UserError.UserNameNotBeEmpty());

            if (userName.Length > USER_NAME_MAX_LENGHT)
                return ExecutionResult.Failure<UserName>(UserError.UserNameMaxLenght());

            if (userName.Length < USER_NAME_MIN_LENGHT)
                return ExecutionResult.Failure<UserName>(UserError.UserNameMinLenght());

            return ExecutionResult.Success(new UserName(userName));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
