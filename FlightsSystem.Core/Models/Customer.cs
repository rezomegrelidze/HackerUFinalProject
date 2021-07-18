namespace FlightsSystem.Core
{
    public class Customer : IPoco, IUser
    {
        protected bool Equals(Customer other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Customer) obj);
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string CreditCardNumber { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Customer a, Customer b)
        {
            if (a is null || b is null) return false;
            return a.Id == b.Id;
        }

        public static bool operator !=(Customer a, Customer b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}," +
                   $" {nameof(UserName)}: {UserName}, {nameof(Password)}: {Password}," +
                   $" {nameof(Address)}: {Address}, {nameof(PhoneNumber)}: {PhoneNumber}, " +
                   $"{nameof(CreditCardNumber)}: {CreditCardNumber}";
        }
    }
}