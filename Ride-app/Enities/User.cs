using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ride_app.Enities
{
    public class User
    {

        public Location _location { get; set; }
        public string _name { get; set; }
        public string _password { get; set; }
        public decimal _wallet { get; set; }

        public User(decimal wallet, Location location, string name, string password)
        {
            _wallet = wallet;
            _location = location;
            _name = name;
            _password = password;
        }
    }
}
