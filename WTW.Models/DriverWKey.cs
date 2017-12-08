using System;
using System.Collections.Generic;
using System.Text;

namespace WTW.Models
{
    public class DriverWKey: Idriver
    {
        private ICar _car = null;
        private ICarKey _key = null;
        public string Name { get; set; }
        public string LicenseState { get; set; }
        public string LicenseNumber { get; set; }
        public int YearsOfExperience { get; set; }
        public DateTime LicenseIssueDate { get; set; }
        public DateTime LicenseExpiry { get; set; }
        public DriverWKey(ICar car, ICarKey key)
        {
            _car = car;
            _key = key;
        }

        public void RunCar()
        {
            Console.WriteLine("Running {0} with {1} - {2} mile ", _car.GetType().Name, _key.GetType().Name, _car.Run());
        }
    }
}
