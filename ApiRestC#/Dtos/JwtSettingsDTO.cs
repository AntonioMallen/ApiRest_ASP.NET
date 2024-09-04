namespace ProyectoJWT.Dtos
{
    public class JwtSettingsDTO
    {
        public string ValidIssuer { get; set; } = string.Empty;
        public string ValidAudience { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
    }
}
