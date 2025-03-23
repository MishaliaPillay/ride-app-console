using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ride_app.Enities;
namespace Ride_app.Enities
{
    public class Location
    {
        public float _latitude { get; set; }
        public float _longitude { get; set; }

        public Location(float latitude, float longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
        }
    }
}
