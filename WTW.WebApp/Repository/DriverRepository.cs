using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WTW.IoC;
using WTW.Models;
using WTW.WebApp.ViewModels;

namespace WTW.WebApp.Repository
{
    public class DriverRepository : IDriverRepository
    {
        private List<DriverViewModel> _drivers = new List<DriverViewModel>()
        {
          GetDriverModel(1, "BMW", "jack", "AZ", "CD00001", 10, new DateTime(1999, 10, 12), new DateTime(2025, 10, 12)),
          GetDriverModel(2, "AUDI", "mack", "CA", "CD01251", 9, new DateTime(2001, 01, 11), new DateTime(2025, 10, 12)),
          GetDriverModel(3, "FORD", "Andrew", "NY", "ED07851", 20, new DateTime(2014, 06, 06), new DateTime(2025, 10, 12)),
          GetDriverModel(4, "BMW", "Tesla", "TX", "TR054501", 23, new DateTime(1999, 06, 05), new DateTime(2025, 10, 12)),
          GetDriverModel(5, "AUDI", "jack", "AZ", "Az078901", 33, new DateTime(1998, 11, 11), new DateTime(2025, 10, 12)),
          GetDriverModel(6, "FORD", "jack", "FL", "FV4564501", 41, new DateTime(2014, 08, 14), new DateTime(2025, 10, 12))
        };

        public List<DriverViewModel> Drivers
        {
            get { return _drivers; }
        }

        public void Delete(int id)
        {
            RemoveDriver(id);
        }

        public IEnumerable<DriverViewModel> Get()
        {
            return _drivers;// GenerateDriversList();
        }

        public DriverViewModel GetById(int id)
        {
            return GetDriver(id);
        }

        private DriverViewModel GetDriver(int driverid)
        {
            return _drivers.FirstOrDefault(s => s.DriverId == driverid);
        }

        private bool RemoveDriver(int driverid)
        {
            var item = _drivers.SingleOrDefault(s => s.DriverId == driverid);
            if (item != null)
                return _drivers.Remove(item);

            return false;
        }
        private static DriverViewModel GetDriverModel(int driverid, string carname, string name, string licensestate, string licensenumber,
        int yearsofexperience, DateTime licenseissueDate, DateTime licenseExpiry)
        {
            var container = new Container();
            switch (carname)
            {
                case "BMW":
                    container.Register<ICar, BMW>();
                    break;
                case "AUDI":
                    container.Register<ICar, Audi>();
                    break;
                case "FORD":
                    container.Register<ICar, Ford>();
                    break;
                default:
                    break;
            }


            Idriver driver = container.Resolve<Driver>();
            driver.Name = name;
            driver.LicenseState = licensestate;
            driver.LicenseNumber = licensenumber;
            driver.YearsOfExperience = yearsofexperience;
            driver.LicenseIssueDate = licenseissueDate;
            driver.LicenseExpiry = licenseExpiry;

            return new DriverViewModel(driver, driverid);

        }
    }
}
