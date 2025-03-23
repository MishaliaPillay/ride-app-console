using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ride_app.Enities;

namespace Ride_app.Application.Interfacse
{
    internal interface IAdmin
    {
        List<Ride> ViewTotalRides(Driver driver);
        decimal ViewTotalEarnings();
        float ViewAverageRating(Driver driver);
        void FlagDriver(Driver driver);
    }
}
