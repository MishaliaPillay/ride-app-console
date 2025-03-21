using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Application.Interfaces
{
    internal interface IUser
    {
        void Register(string username, string password);
        void Login(
            string username, string password);
        void Logout();
        void Exit();


    }
}
