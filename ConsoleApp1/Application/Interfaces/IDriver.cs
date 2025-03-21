using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Entities;

namespace ConsoleApp1.Application.Interfaces
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
