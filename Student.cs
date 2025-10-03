using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07_Mini_Project
{
    internal class Student
    {
        public string Name { get; set; }

        public void Notify(string message)
        {
            Console.WriteLine($"!!!!!!!!!!!!!!!!!!!! Notification about exam: {message} !!!!!!!!!!!!!!!!!!!!");
        }
    }
}
