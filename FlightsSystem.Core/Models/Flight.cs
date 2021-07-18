using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightsSystem.Core
{
    public class Flight : IPoco
    {
        protected bool Equals(Flight other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Flight) obj);
        }

        public long Id { get; set; }
        [ForeignKey("AirlineCompany")]
        public long AirlineCompanyId { get; set; }
        [ForeignKey("OriginCountry")]
        public long? OriginCountryCode { get; set; }
        [ForeignKey("DestinationCountry")]
        public long? DestinationCountryCode { get; set; }
        public virtual Country OriginCountry { get; set; }
        public virtual Country DestinationCountry { get; set; }
        public AirlineCompany AirlineCompany { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime LandingTime { get; set; }
        public int RemainingTickets { get; set; }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Flight a, Flight b)
        {
            if (a is null || b is null) return false;
            return a.Id == b.Id;
        }

        public static bool operator !=(Flight a, Flight b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(AirlineCompanyId)}: {AirlineCompanyId}, {nameof(OriginCountryCode)}:" +
                   $" {OriginCountryCode}, {nameof(DestinationCountryCode)}: {DestinationCountryCode}," +
                   $" {nameof(OriginCountry)}: {OriginCountry}, {nameof(DestinationCountry)}: " +
                   $"{DestinationCountry}, {nameof(AirlineCompany)}: {AirlineCompany}, {nameof(DepartureTime)}:" +
                   $" {DepartureTime}, {nameof(LandingTime)}: {LandingTime}, {nameof(RemainingTickets)}: {RemainingTickets}";
        }
    }
}