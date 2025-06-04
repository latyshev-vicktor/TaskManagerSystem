namespace AuthenticationService.Application.UseCases.User.Dto
{
    public class UserShortDto
    {
        public long UserId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
