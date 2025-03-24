using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ride_app.Enities;

namespace Ride_app.Application.Interfacse
{
    internal interface IPassenger
    {
        void RateDriver(int Rating, Driver driver);
        void ViewWallet();
        void FundWallet(decimal Amount);
        bool RequestRide(Location PickUp, Location DropOff);
        List<Ride> Rides();
    }
}
