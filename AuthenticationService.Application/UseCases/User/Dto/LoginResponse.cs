namespace AuthenticationService.Application.UseCases.User.Dto
{
    public record LoginResponse(string AccessToken, 
                                string RefreshToken,
                                long UserId, 
                                string UserName, 
                                string Email,
                                string FirstName,
                                string LastName);
}
