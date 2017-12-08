using System;
using Xunit;
using WTW.IOC;
using WTW.IoC.LifeCycleFactories;
using WTW.IoC.Enums;
using WTW.Models;
using WTW.IoC;

namespace WTW.IOC.Tests
{

    public class ResolveScenarios
    {
        [Fact]
        public void When_registering_a_BMWCar_using_a_Icar_then_Resolve_should_return_Icar_with_expected_Name_BWM()
        {
            //arrange 
            string carname = "BMW";

            var container = new Container();
            container.Register<ICar, BMW>();

            // act
            var bwmcar = container.Resolve<ICar>();

            //Assert
            Assert.Equal(carname, bwmcar.Name());
        }
        [Fact]
        public void When_registering_a_simple_object_with_instance_name_then_Resolve_should_return_object_of_expected_type()
        {
            // arrange
            const string bmwinstance = "bmw";
            const string fordinstance = "ford";

            var container = new Container();
            container.Register<ICar, BMW>(bmwinstance);
            container.Register<ICar, Ford>(fordinstance);


            // act
            ICar bwm = container.Resolve<ICar>(bmwinstance);


            // assert
            Assert.IsType<BMW>(bwm);
        }

        [Fact]
        public void When_resolving_a_object_with_Constructor_then_Resolve_should_resolve_registered_object_of_expected_type()
        {
            // arrange

            var container = new Container();
            container.Register<ICar, BMW>();
            container.Register<ICarKey, BMWKey>();


            // act
            var driver = container.Resolve<Driver>();


            // assert
            Assert.IsType<Driver>(driver);
        }
        [Fact]
        public void When_resolving_a_object_with_Constructor_with_Transient_then_Resolve_should_return_different_object_of_expected_type()
        {
            // arrange

            var container = new Container();
            container.Register<ICar, BMW>(LifeCycleType.Transient);


            // act
            ICar bmw = container.Resolve<ICar>();
            ICar ford = container.Resolve<ICar>();
            int bmwrun = bmw.Run();
            int fordrun = ford.Run();

            // assert
            Assert.Equal(bmwrun, fordrun);
        }

        [Fact]
        public void When_resolving_a_object_with_Constructor_with_Singelton_then_Resolve_should_return_same_object_of_expected_type()
        {
            // arrange

            var container = new Container();
            container.Register<ICar, BMW>(LifeCycleType.Singleton);


            // act
            ICar bmw = container.Resolve<ICar>();
            ICar ford = container.Resolve<ICar>();
            int bmwrun = bmw.Run();
            int fordrun = ford.Run();

            // assert
            Assert.Equal(bmwrun + 1, fordrun);
        }

        [Fact]
        public void When_resolving_a_object_with_Constructor_default_lifecycle_then_Resolve_should_return_transient_object_of_expected_type()
        {
            // arrange

            var container = new Container();
            container.Register<ICar, BMW>();


            // act
            ICar bmw = container.Resolve<ICar>();
            ICar ford = container.Resolve<ICar>();
            int bmwrun = bmw.Run();
            int fordrun = ford.Run();

            // assert
            Assert.Equal(bmwrun, fordrun);
        }
    }
}
