namespace FlightsSystem.Core.DAL
{
    public interface ICustomerDAO : IBasicDB<Customer>
    {
        Customer GetCustomerByUsername(string username);
    }
}