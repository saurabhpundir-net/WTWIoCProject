using System;

namespace WTW.IoC.LifeCycleFactories
{
    /// <summary>
    /// Singleton obect factory which returns the same singleton instance 
    /// when Create() method is called
    /// </summary>
    public class SingletonFactory : ILifeCycleFactory
    {
        private Lazy<object> singletonCreator;

        public SingletonFactory(Delegate objectCreator)
        {
            singletonCreator = new Lazy<object>(() => objectCreator.DynamicInvoke());
        }

        public object Create()
        {
            return singletonCreator.Value;
        }
    }
}
