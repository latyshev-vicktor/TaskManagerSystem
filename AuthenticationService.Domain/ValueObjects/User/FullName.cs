using AuthenticationService.Domain.Errors;
using CSharpFunctionalExtensions;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Domain.ValueObjects.User
{
    public class FullName : ValueObject
    {
        public const int MAX_LENGHT_FIRST_NAME = 20;
        public const int MAX_LENGHT_LAST_NAME = 50;

        public string FirstName { get; }
        public string LastName { get; }

        protected FullName()
        {

        }

        protected FullName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public static IExecutionResult<FullName> Create(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return ExecutionResult.Failure<FullName>(UserError.FirstNameNotBeEmpty());

            if (string.IsNullOrWhiteSpace(lastName))
                return ExecutionResult.Failure<FullName>(UserError.LastNameNotBeEmpty());

            if (firstName.Length > MAX_LENGHT_FIRST_NAME)
                return ExecutionResult.Failure<FullName>(UserError.FirstNameMaxLenght());

            if (lastName.Length > MAX_LENGHT_LAST_NAME)
                return ExecutionResult.Failure<FullName>(UserError.LastNameMaxLenght());

            return ExecutionResult.Success(new FullName(firstName, lastName));
        }

        protected override IEnumerable<string> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
