using System;
using Xunit;
using WTW.IOC;
using WTW.IoC.LifeCycleFactories;
using WTW.IoC.Enums;
using WTW.Models;
using WTW.IoC;

namespace WTW.IOC.Tests
{

    public class IsRegisteredScenarios
    {
        [Fact]
        public void When_checking_for_a_non_registered_type_with_generic_method_thenIsRegistered_should_return_false()
        {
            // arrange
            var container = new Container();
            container.Register<ICar, Ford>();


            // act
            bool isRegistered = container.IsRegistered<string>();


            // assert
            Assert.False(isRegistered);
        }
        [Fact]
        public void When_checking_for_a_non_registered_type_thenIsRegistered_should_return_false()
        {
            // arrange
            var container = new Container();
            container.Register<ICar, BMW>();


            // act
            bool isRegistered = container.IsRegistered<Ford>();


            // assert
            Assert.False(isRegistered);
        }
        [Fact]
        public void When_registering_in_container_with_generic_method_then_IsRegistered_should_return_true()
        {
            // arrange
            var container = new Container();
            container.Register<ICar, BMW>();


            // act
            bool isRegistered = container.IsRegistered<ICar>();


            // assert
            Assert.True(isRegistered);
        }

        [Fact]
        public void When_registering_named_instances_in_container_notexists_then_IsRegistered_should_return_false()
        {
            // arrange
            // arrange
            const string bmwinstance = "bmw";
            const string fordinstance = "ford";

            var container = new Container();
            container.Register<ICar, BMW>(bmwinstance);
            
            // act
            ICar bwm = container.Resolve<ICar>(bmwinstance);


            // act
            bool isRegistered = container.IsRegistered<ICar> (fordinstance);


            // assert
            Assert.False(isRegistered);
        }
        [Fact]
        public void When_registering_named_instances_in_container_exists_then_IsRegistered_should_return_true()
        {
            // arrange
            // arrange
            const string bmwinstance = "bmw";
            var container = new Container();
            container.Register<ICar, BMW>(bmwinstance);
            // act
            ICar bwm = container.Resolve<ICar>(bmwinstance);


            // act
            bool isRegistered = container.IsRegistered<ICar>(bmwinstance);


            // assert
            Assert.True(isRegistered);
        }
    }
}
