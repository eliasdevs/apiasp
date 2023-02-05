namespace universityApiBackend.Models.Jwt
{
    public class JwtSettings
    {
        public bool ValidateIssuerSiningKey { get; set; }
        public string? IssuerSiningKey { get; set; }=string.Empty;
        public bool? ValidateIssuer { get; set; } = true;
        public string? ValidIssuer { get; set; }
        public bool ValidateAudience { get; set; } = true;
        public string? ValidAudience { get; set; }

        public bool RequireExpirationTime { get; set; }
        public bool ValidateLifeTime { get; set; } = true;





    }
}
