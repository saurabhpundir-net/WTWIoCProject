using System;

namespace WTW.IoC.LifeCycleFactories
{
    /// <summary>
    /// Transient obect factory which returns a new instance 
    /// when Create() method is called
    /// </summary>
    public class TransientFactory : ILifeCycleFactory
    {
        private Delegate objectCreator;

        public TransientFactory(Delegate objectCreator)
        {
            this.objectCreator = objectCreator;
        }

        public object Create()
        {
            return objectCreator.DynamicInvoke();
        }
    }
}
