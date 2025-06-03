namespace TaskManagerSystem.Common.Options
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessTokenExpiredMinute { get; set; }
        public int RefreshTokenExpiredMinute { get; set; }
    }
}
