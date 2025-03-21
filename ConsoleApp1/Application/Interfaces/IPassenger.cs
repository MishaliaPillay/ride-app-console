using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Entities;

namespace ConsoleApp1.Application.Interfaces
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
