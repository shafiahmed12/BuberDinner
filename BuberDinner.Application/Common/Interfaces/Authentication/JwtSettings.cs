namespace BuberDinner.Application.Common.Interfaces.Authentication
{
    public class JwtSettings
    {
        public string SectionName = "JwtSettings";
        public string Secret { get; init; } = null!;
        public string Audience { get; init; } = null!;
        public string Issuer { get; init; } = null!;
        public int ExpiryInMinutes { get; init; }
    }
}