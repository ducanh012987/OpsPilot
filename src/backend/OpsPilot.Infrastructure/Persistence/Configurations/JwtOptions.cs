namespace OpsPilot.Infrastructure.Persistence.Configurations
{
    public class JwtOptions
    {
        public const string SectionName = "Jwt";

        public string SecretKey { get; set; } = string.Empty;

        public string Issuer { get; set; } = string.Empty;

        public string Audience { get; set; } = string.Empty;

        public int AccessTokenMinutes { get; set; } = 30;

        public int RefreshTokenDays { get; set; } = 7;
    }
}
