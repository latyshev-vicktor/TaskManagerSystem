namespace AuthenticationService.Domain.Entities
{
    public class RoleUserEntity
    {
        public long RoleId { get; set; }
        public RoleEntity? Role { get; set; }
        public long UserId { get; set; }
        public UserEntity? User { get; set; }
    }
}
