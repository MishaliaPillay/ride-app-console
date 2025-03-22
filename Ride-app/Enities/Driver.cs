using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ride_app.Enities;
namespace Ride_app.Enities
{
    internal class Driver : User
    {
        private bool _isAvailable { get; set; }
        private List<Ride> _completedRides { get; set; }
        private float _rating { get; set; }
    }
}
