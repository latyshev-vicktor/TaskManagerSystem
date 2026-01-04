using AuthenticationService.Domain.DomainEvents;
using AuthenticationService.Domain.Errors;
using AuthenticationService.Domain.SeedWork;
using AuthenticationService.Domain.ValueObjects.User;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public UserName UserName { get; private set; }
        public Phone Phone { get; private set; }
        public FullName FullName { get; private set; }
        public Email Email { get; private set; }
        public DateTimeOffset BirthDay { get; private set; }
        public string PasswordHash { get; private set; }

        public List<RoleEntity> Roles { get; set; } = [];

        #region Конструкторы
        protected UserEntity()
        {

        }

        protected UserEntity(
            UserName userName,
            FullName fullName,
            Email email,
            Phone phone,
            string passwordHash,
            DateTime birthDay)
        {
            UserName = userName;
            Email = email;
            Phone = phone;
            BirthDay = birthDay;
            PasswordHash = passwordHash;
            FullName = fullName;
        }
        #endregion

        #region DDD-методы
        public static IExecutionResult<UserEntity> Create(
            string userName,
            string firstName,
            string lastName,
            string email,
            string phone,
            string passwordHash,
            DateTime birthDay)
        {

            var fullNameResult = FullName.Create(firstName, lastName);
            if (fullNameResult.IsFailure)
                return ExecutionResult.Failure<UserEntity>(fullNameResult.Error);

            var phoneResult = Phone.Create(phone);
            if (phoneResult.IsFailure)
                return ExecutionResult.Failure<UserEntity>(phoneResult.Error);

            var userNameResult = UserName.Create(userName);
            if (userNameResult.IsFailure)
                return ExecutionResult.Failure<UserEntity>(userNameResult.Error);

            var emailResult = Email.Create(email);
            if (emailResult.IsFailure)
                return ExecutionResult.Failure<UserEntity>(emailResult.Error);

            if (string.IsNullOrWhiteSpace(passwordHash))
                return ExecutionResult.Failure<UserEntity>(UserError.PasswordNotBeEmpty());

            return ExecutionResult.Success(new UserEntity(userNameResult.Value, fullNameResult.Value, emailResult.Value, phoneResult.Value, passwordHash, birthDay));
        }

        public IExecutionResult SetFullName(string firstName, string lastNmae)
        {
            var fullNameResult = FullName.Create(firstName, lastNmae);
            if (fullNameResult.IsFailure)
                return ExecutionResult.Failure(fullNameResult.Error);

            FullName = fullNameResult.Value;
            return ExecutionResult.Success();
        }

        public IExecutionResult SetPhone(string phone)
        {
            var phoneResult = Phone.Create(phone);
            if (phoneResult.IsFailure)
                return ExecutionResult.Failure(phoneResult.Error);

            Phone = phoneResult.Value;
            return ExecutionResult.Success();
        }

        public IExecutionResult SetUserName(string userName)
        {
            var userNameResult = UserName.Create(userName);
            if (userNameResult.IsFailure)
                return ExecutionResult.Failure(userNameResult.Error);

            UserName = userNameResult.Value;
            return ExecutionResult.Success();
        }

        public IExecutionResult SetEmail(string email)
        {
            var emailResult = Email.Create(email);
            if (emailResult.IsFailure)
                return ExecutionResult.Failure(emailResult.Error);

            if(Email != emailResult.Value)
                RiseDomainEvents(new UserUpdatedEmailEvent(Id, emailResult.Value.Value));

            Email = emailResult.Value;
            return ExecutionResult.Success();
        }

        public void SetBirthDay(DateTimeOffset birthDay)
        {
            BirthDay = birthDay;
        }

        #endregion
    }
}
