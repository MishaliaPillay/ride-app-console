using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ride_app.Enities;

namespace Ride_app.Enities
{
    public class Passenger : User
    {
        public List<Ride> _rides { get; set; }

        public Passenger(User user)
            : base(user._wallet, user._location, user._name, user._password, user._id, user._isDriver)
        {
            _rides = new List<Ride>();
        }
    }
}
