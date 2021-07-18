using FlightsSystem.Core.Login;

namespace FlightsSystem.Core.Login
{
    public static class LoginHelpers
    {
        public static bool IsTokenValid<T>(this LoginToken<T> token) where T:IUser
        {
            return token != null && token.User != null;
        }
    }
}