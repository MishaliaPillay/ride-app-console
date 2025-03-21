using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Entities
{
    internal class Driver : User
    {
        private bool isAvailable { get; set; }
        private List<Ride> completedRides { get; set; }
        private float Rating { get; set; }
    }
}
