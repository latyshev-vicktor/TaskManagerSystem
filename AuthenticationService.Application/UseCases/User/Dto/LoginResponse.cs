namespace AuthenticationService.Application.UseCases.User.Dto
{
    public record LoginResponse(string AccessToken, 
                                long UserId, 
                                string UserName, 
                                string Email,
                                string FirstName,
                                string LastName);
}
