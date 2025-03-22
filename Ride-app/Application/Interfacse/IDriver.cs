using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ride_app.Enities;

namespace Ride_app.Application.Interfacse
{
    internal interface IDriver
    {
        void AccecptRide(Ride ride);
        List<Ride> ViewRideRequests();
        void CompleteRide(Ride ride);
        decimal ViewEarnings();
        float ViewAverageRating();
    }
}
