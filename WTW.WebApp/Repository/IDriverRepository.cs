using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WTW.WebApp.ViewModels;

namespace WTW.WebApp.Repository
{
    public interface IDriverRepository
    {
        DriverViewModel GetById(int id);
        void Delete(int id);
        IEnumerable<DriverViewModel> Get();
    }
}
