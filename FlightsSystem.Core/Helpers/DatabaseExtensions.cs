using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightsSystem.Core.DAL;

namespace FlightsSystem.Core.Helpers
{
    public static class DatabaseExtensions
    {
        public static void Clear<T>(this IBasicDB<T> db) where T : IPoco
        {
            foreach(var item in db.GetAll())
                db.Remove(item);
        }
    }
}
