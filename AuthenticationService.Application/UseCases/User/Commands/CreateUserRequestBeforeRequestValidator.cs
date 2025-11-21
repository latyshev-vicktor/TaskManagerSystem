using Microsoft.Extensions.Logging;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Application.UseCases.User.Commands
{
    public class CreateUserRequestBeforeRequestValidator(ILogger<CreateUserRequestBeforeRequestValidator> logger) : BeforeRequestValidator<CreateUserRequest>
    {
        public override Task<IExecutionResult> Execution(CreateUserRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Вызов перед вызовом основного апи {request.Dto.ToString()}");
            return Task.FromResult(ExecutionResult.Success());
        }
    }
}
