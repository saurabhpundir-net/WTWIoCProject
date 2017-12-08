using System;
using System.Collections.Generic;
using System.Text;

namespace WTW.Models
{
    public class Driver: Idriver
    {
        private ICar _car = null;
        public string Name { get; set; }
        public string LicenseState { get; set; }
        public string LicenseNumber { get; set; }
        public int YearsOfExperience { get; set; }
        public DateTime LicenseIssueDate { get; set; }
        public DateTime LicenseExpiry { get; set; }
        public Driver(ICar car)
        {
            _car = car;
        }

        public void RunCar()
        {
            Console.WriteLine("Running {0} - {1} mile ", _car.GetType().Name, _car.Run());
        }
    }
}
