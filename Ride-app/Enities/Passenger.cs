using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ride_app.Enities;

namespace Ride_app.Enities
{
    internal class Passenger : User
    {
        private List<Ride> _rides { get; set; }
    }
}
