namespace AuthenticationService.Application.UseCases.User.Dto
{
    public record LoginResponse(string AccessToken, string RefreshToken);
}
