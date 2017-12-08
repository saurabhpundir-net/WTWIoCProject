using System;
using WTW.WebApp.Controllers;
using Xunit;
using WTW.IoC;
using WTW.Models;
using WTW.WebApp;
using Microsoft.AspNetCore.Mvc;
using WTW.WebApp.Repository;
using WTW.WebApp.ViewModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using WTW.IoC.Enums;

namespace WTW.WebApp.Tests
{
    public class DriverControllerScenariors
    {
        [Fact]
        public void When_Index_is_having_valid_viewresult_should_match()
        {
            var container = new Container();
            container.Register<IDriverRepository, DriverRepository>();

            //Arrange
            var controller = container.Resolve<DriverController>();

            // Act
            var result = controller.Index() as ViewResult;


            //// Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void When_Index_is_having_validresult_count_rows_should_match()
        {
            var container = new Container();
            container.Register<IDriverRepository, DriverRepository>();

            //Arrange
            var controller = container.Resolve<DriverController>();


            // Act
            var result = controller.Index() as ViewResult;
            var drivermodelcount = ((List<DriverViewModel>)result.Model).Count;

            var drivercollectioncount = container.Resolve<DriverRepository>().Drivers.Count;


            //// Assert
            Assert.Equal(drivermodelcount, drivercollectioncount);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void When_Details_is_having_valid_viewresult_should_match(int id)
        {
            var container = new Container();
            container.Register<IDriverRepository, DriverRepository>();

            //Arrange
            var controller = container.Resolve<DriverController>();

            // Act
            var result = controller.Details(id) as ViewResult;


            //// Assert
            Assert.IsType<ViewResult>(result);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void When_Details_is_returning_same_driver_id_should_match(int id)
        {
            var container = new Container();
            container.Register<IDriverRepository, DriverRepository>(LifeCycleType.Singleton);

            //Arrange
            var controller = container.Resolve<DriverController>();


            // Act
            controller.Index();
            var result = controller.Details(id) as ViewResult;
            var driveridmodel = ((DriverViewModel)(result.Model)).DriverId;

            var driveridcollection = container.Resolve<DriverRepository>().Drivers.Find(f => f.DriverId == id).DriverId;


            //// Assert
            Assert.Equal(driveridmodel, driveridcollection);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        public void When_Details_is_returning_no_driver_when_driver_id_not_match(int id)
        {
            var container = new Container();
            container.Register<IDriverRepository, DriverRepository>(LifeCycleType.Singleton);

            //Arrange
            var controller = container.Resolve<DriverController>();

            // Act
            var result = controller.Details(id) as ViewResult;
            var driveridmodel = ((DriverViewModel)result.Model);
            //// Assert
            Assert.Null(driveridmodel);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void When_Delete_is_having_valid_viewresult_should_match(int id)
        {
            var container = new Container();
            container.Register<IDriverRepository, DriverRepository>();

            //Arrange
            var controller = container.Resolve<DriverController>();

            // Act
            var result = controller.Delete(id) as ViewResult;


            //// Assert
            Assert.IsType<ViewResult>(result);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        public void When_Delete_is_returning_no_driver_when_driver_id_not_match(int id)
        {
            var container = new Container();
            container.Register<IDriverRepository, DriverRepository>();

            //Arrange
            var controller = container.Resolve<DriverController>();


            // Act
            var result = controller.Delete(id) as ViewResult;
            var driveridmodel = ((DriverViewModel)result.Model);


            //// Assert
            Assert.Null(driveridmodel);
        }
        [Theory]
        [InlineData(1, null)]
        [InlineData(2, null)]
        public void When_Delete_is_deleting_driver_when_driver_id_match(int id, IFormCollection collection)
        {
            var container = new Container();
            container.Register<IDriverRepository, DriverRepository>(LifeCycleType.Singleton);

            //Arrange
            var controller = container.Resolve<DriverController>();


            // Act
            controller.Index();
            var result = controller.Delete(id, collection) as ViewResult;

            var driveridcollection = ((DriverRepository)controller.Repository).Drivers.Find(f => f.DriverId == id);


            //// Assert
            Assert.Null(driveridcollection);
        }
    }
}
