using System.ComponentModel;
using System.Runtime.CompilerServices;
using FlightsSystem.DBGenerator.Annotations;

namespace FlightsSystem.DBGenerator.Models
{
    public class RandomDataSpec : INotifyPropertyChanged
    {
        private int _airlineCompanyCount;
        private int _minCustomerCount;
        private int _maxCustomerCount;
        private int _administratorCount;
        private int _flightCountPerCompany;
        private int _minTicketPerCustomer;
        private int _maxTicketPerCustomer;
        private int _countryCount;

        public int AirlineCompanyCount
        {
            get => _airlineCompanyCount;
            set
            {
                _airlineCompanyCount = value; 
                OnPropertyChanged(nameof(AirlineCompanyCount));
            }
        }

        public int MinCustomerCount
        {
            get => _minCustomerCount;
            set
            {
                _minCustomerCount = value;
                OnPropertyChanged(nameof(MinCustomerCount));
            }
        }

        public int MaxCustomerCount
        {
            get => _maxCustomerCount;
            set
            {
                _maxCustomerCount = value;
                OnPropertyChanged(nameof(MaxCustomerCount));
            }
        }

        public int AdministratorCount
        {
            get => _administratorCount;
            set
            {
                _administratorCount = value; 
                OnPropertyChanged(nameof(AdministratorCount));
            }
        }

        public int FlightCountPerCompany
        {
            get => _flightCountPerCompany;
            set
            {
                _flightCountPerCompany = value;
                OnPropertyChanged(nameof(FlightCountPerCompany));
            }
        }

        public int MinTicketPerCustomer
        {
            get => _minTicketPerCustomer;
            set
            {
                _minTicketPerCustomer = value;
                OnPropertyChanged(nameof(MinTicketPerCustomer));
            }
        }

        public int MaxTicketPerCustomer
        {
            get => _maxTicketPerCustomer;
            set
            {
                _maxTicketPerCustomer = value; 
                OnPropertyChanged(nameof(MaxTicketPerCustomer));
            }
        }

        public int CountryCount
        {
            get => _countryCount;
            set
            {
                _countryCount = value;
                OnPropertyChanged(nameof(CountryCount));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}