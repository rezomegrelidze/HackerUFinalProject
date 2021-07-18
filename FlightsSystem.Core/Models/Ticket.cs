using System.ComponentModel.DataAnnotations.Schema;

namespace FlightsSystem.Core
{
    public class Ticket : IPoco
    {
        protected bool Equals(Ticket other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Ticket) obj);
        }

        public long Id { get; set; }
        [ForeignKey("Flight")]
        public long FlightId { get; set; }
        [ForeignKey("Customer")]
        public long CustomerId { get; set; }

        public Flight Flight { get; set; }
        public Customer Customer { get; set; }
        
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Ticket a, Ticket b)
        {
            if (a is null || b is null) return false;
            return a.Id == b.Id;
        }

        public static bool operator !=(Ticket a, Ticket b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(FlightId)}: {FlightId}, {nameof(CustomerId)}:" +
                   $" {CustomerId}, {nameof(Flight)}: {Flight}, {nameof(Customer)}: {Customer}";
        }
    }
}