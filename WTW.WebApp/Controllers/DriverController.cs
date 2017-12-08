using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WTW.WebApp.ViewModels;
using WTW.IoC;
using WTW.Models;
using WTW.WebApp.Repository;

namespace WTW.WebApp.Controllers
{
    public class DriverController : Controller
    {
        public IDriverRepository _repositry;


        public IDriverRepository Repository
        {
            get { return _repositry; }
        }


        public DriverController(IDriverRepository repositry)
        {
            _repositry = repositry;
        }
        // GET: Driver
        public ActionResult Index()
        {
            var model = _repositry.Get();
            return View(model);
        }

        // GET: Driver/Details/5
        public ActionResult Details(int id)
        {
            var model = _repositry.GetById(id);
            return View(model);
        }
        // GET: Driver/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = _repositry.GetById(id);
            return View(model);
        }

        // POST: Driver/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _repositry.Delete(id); 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
      
    }
}