using AuthenticationService.Domain.ValueObjects.User;
using TaskManagerSystem.Common.Enums;
using TaskManagerSystem.Common.Errors;

namespace AuthenticationService.Domain.Errors
{
    public class UserError
    {
        public static Error UserNameNotBeEmpty()
            => new(ResultCode.BadRequest, "UserName пользователя не может быть пустым");

        public static Error UserNameMaxLenght()
            => new(ResultCode.BadRequest, "Превышена максимальная длина UserName пользователя");
        public static Error UserNameMinLenght()
            => new(ResultCode.BadRequest, "Минимальная длина UserName пользователя " + UserName.USER_NAME_MIN_LENGHT);

        public static Error FirstNameNotBeEmpty()
            => new(ResultCode.BadRequest, "Имя пользователя не может быть пустым");

        public static Error LastNameNotBeEmpty()
            => new(ResultCode.BadRequest, "Фамилия пользователя не может быть пустой");

        public static Error FirstNameMaxLenght()
            => new(ResultCode.BadRequest, "Превышена максимальная длина имени");

        public static Error LastNameMaxLenght()
            => new(ResultCode.BadRequest, "Превышена максимальная длина фамилии");

        public static Error PhoneNotBeEmpty()
            => new(ResultCode.BadRequest, "Номер телефона не может быть пустым");
        public static Error IncorrectPhone()
            => new(ResultCode.BadRequest, "Введенное значение не является номером телефона");
        public static Error PhoneMaxLenght()
            => new(ResultCode.BadRequest, "Превышена допустимая длина номера телефона");

        public static Error EmailNotValid()
            => new(ResultCode.BadRequest, "Введенное значение не является Email");

        public static Error EmailNotBeEmpty()
            => new(ResultCode.BadRequest, "Email не может быть пустым");

        public static Error DublicateEmailUser()
            => new(ResultCode.BadRequest, "Пользователь с данным email уже существует");

        public static Error DublicateUserName()
            => new(ResultCode.BadRequest, "Пользователь с данным user name уже существует");

        public static Error PasswordNotBeEmpty()
            => new(ResultCode.BadRequest, "Пароль не может быть пустым");

        public static Error NotFoundByEmailOrPassword()
            => new(ResultCode.BadRequest, "Неправильные email или пароль");

        public static Error UserByRefreshTokenNotFound()
            => new(ResultCode.UnAuthorize, "Пользователь по переданному refresh токену не найден");

        public static Error UserByIdNotFound()
            => new(ResultCode.BadRequest, "Пользователь по переданному идентификатору не найден");

        public static Error UserIdNotNull()
            => new(ResultCode.BadRequest, "Идентификатор пользователя не может быть пустым");
    }
}
