﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ride_app.Enities;
namespace Ride_app.Enities
{
    public class Driver : User
    {
        public bool _isAvailable { get; set; }
        public List<int> rideIDs = new List<int>();
        public List<int> _rating { get; set; }

        public Driver(User user)
            : base(user._wallet, user._location, user._name, user._password, user._id, user._isDriver)
        {
            _isAvailable = true;
            _rating = new List<int>();
            rideIDs = new List<int>();
        }
    }
}
