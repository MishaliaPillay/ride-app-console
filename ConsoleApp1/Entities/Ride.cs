using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Entities
{
    internal class Ride
    {
        private Location PickUp { get; set; }
        private Location DropOff { get; set; }
        private Passenger Passenger { get; set; }
        private Driver Driver { get; set; }
        private int Rating { get; set; }
        private decimal Rate { get; set; }
    }
}
