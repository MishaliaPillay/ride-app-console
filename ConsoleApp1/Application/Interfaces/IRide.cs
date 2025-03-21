using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Entities;

namespace ConsoleApp1.Application.Interfaces
{
    internal interface IRide
    {
        float GetDistance(Location Start, Location End);
        decimal CalculateRate(float Distance);
    }
}
