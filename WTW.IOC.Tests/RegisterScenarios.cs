using System;
using Xunit;
using WTW.IOC;
using WTW.IoC.LifeCycleFactories;
using WTW.IoC.Enums;
using WTW.Models;
using WTW.IoC;

namespace WTW.IOC.Tests
{

    public class RegisterScenarios
    {
        [Fact]
        public void Given_a_container_when_registering_with_correct_instance_should_Pass()
        {
            // arrange
            var container = new Container();
            container.Register<ICar, Ford>();

            // act/assert
            Assert.Equal(((Ford)container.Resolve<ICar>()).Name(), new Ford().Name());
        }

        [Fact]
         public void Given_a_container_when_registering_with_generic_using_null_delegate_then_should_throw_ArgumentNullException()
        {
            // arrange
            var container = new Container();
            Func<ICar> createInstanceDelegate = null;


            // act/assert
            Assert.Throws<ArgumentNullException>(() => {
                container.Register<ICar>(createInstanceDelegate);
            });
        }
        [Theory]
        [InlineData("car")]
        [InlineData("")]
        public void Given_a_container_when_registering_with_generic_same_instance_more_than_once_then_should_throw_InvalidOperationException(string instanceName)
        {
            // arrange
            var container = new Container();
            container.Register<ICar, Ford>(instanceName);


            // act/assert
            Assert.Throws<InvalidOperationException>(() => {
                container.Register<ICar, Ford>(instanceName);
            });
        }
    }
}
