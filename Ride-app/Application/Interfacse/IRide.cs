using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ride_app.Enities;

namespace Ride_app.Application.Interfacse
{
    internal interface IRide
    {
        float GetDistance(Location Start, Location End);
        decimal CalculateRate(float Distance);
    }
}
