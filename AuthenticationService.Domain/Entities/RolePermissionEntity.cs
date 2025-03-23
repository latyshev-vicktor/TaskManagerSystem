namespace AuthenticationService.Domain.Entities
{
    public class RolePermissionEntity
    {
        public long RoleId { get; set; }
        public RoleEntity? Role { get; set; }
        public long PermissionId {  get; set; }
        public PermissionEntity? Permission { get; set; }
    }
}
