namespace FlightsSystem.Web.Models
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, UserModel user);
        bool IsTokenValid(string key, string issuer, string token);
    }
}