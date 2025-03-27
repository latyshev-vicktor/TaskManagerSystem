namespace TaskManagerSystem.Common.Options
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int DaysToExpirationAccessToken { get; set; }
        public TimeSpan ExpireAccessToken => TimeSpan.FromMinutes(DaysToExpirationAccessToken);
    }
}
