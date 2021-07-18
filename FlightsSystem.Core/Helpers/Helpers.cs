using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsSystem.Core.Helpers
{
    public static class Helpers
    {
        public static DateTime GetRandomDateTime()
        {
            var rand = new Random();
            var days = rand.Next(1, 366); // only give random dates that are maximum year from now
            var hours = rand.Next(25);
            var minutes = rand.Next(61);
            var date = DateTime.Now;
            return date.AddDays(days).AddHours(hours).AddMinutes(minutes);
        }
    }
}
