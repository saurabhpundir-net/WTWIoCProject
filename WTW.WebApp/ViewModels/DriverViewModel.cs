using System;
using System.Collections.Generic;
using System.Linq;
using WTW.Models;

namespace WTW.WebApp.ViewModels
{
    public class DriverViewModel
    {

        public Idriver DriverModel { get; set; }
        public DriverViewModel(Idriver driver,int id)
        {
            DriverModel = driver;
            DriverId = id;
        }
        public int DriverId { get; private set; }
        public string Name
        {
            get { return DriverModel.Name; }
            set { DriverModel.Name = value; }
        }
        public string LicenseState
        {
            get { return DriverModel.LicenseState; }
            set { DriverModel.LicenseState = value; }
        }
        public string LicenseNumber
        {
            get { return DriverModel.LicenseNumber; }
            set { DriverModel.LicenseNumber = value; }
        }
        public int YearsOfExperience
        {
            get { return DriverModel.YearsOfExperience; }
            set { DriverModel.YearsOfExperience = value; }
        }
        public DateTime LicenseIssueDate
        {
            get { return DriverModel.LicenseIssueDate; }
            set { DriverModel.LicenseIssueDate = value; }
        }
        public DateTime LicenseExpiry
        {
            get { return DriverModel.LicenseExpiry; }
            set { DriverModel.LicenseExpiry = value; }
        }



    }
}