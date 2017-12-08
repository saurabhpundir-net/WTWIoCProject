using System;
using System.Collections.Generic;
using System.Text;

namespace WTW.Models
{
    public interface Idriver
    {
        string Name { get; set; }
        string LicenseState { get; set; }
        string LicenseNumber { get; set; }
        int YearsOfExperience { get; set; }
        DateTime LicenseIssueDate { get; set; }
        DateTime LicenseExpiry { get; set; }
        void RunCar();
    }
}
