using AuthenticationService.Application.UseCases.User.Dto;
using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Application.UseCases.User.Commands
{
    public record CreateUserRequest(CreateUserDto Dto) : IRequest<IExecutionResult>, ILockableRequest
    {
        public string GetLockKey()
            => "create_user_request_" + Dto.Email;

        public string GetOperationName()
            => "Регистрация нового пользователя";
    }
}
