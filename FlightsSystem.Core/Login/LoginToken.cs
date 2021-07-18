namespace FlightsSystem.Core.Login
{
    public class LoginToken<T> where T: IUser
    {
        public T User { get; set; }
    }
}