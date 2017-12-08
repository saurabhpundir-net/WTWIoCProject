using System;
using System.Collections.Generic;
using System.Text;
using WTW.IoC.LifeCycleFactories;
using WTW.IoC.Enums;

namespace WTW.IoC
{
    internal class Registrations
    {
        /// <summary>
        /// Unique key made of Type and instance name
        /// </summary>
        public RegistrationKey Key
        {
            get;
            protected set;
        }
        /// <summary>
        /// Delegate for Instance 
        /// </summary>
        public Func<object> InstanceDelegate
        {
            get;
            protected set;
        }
        /// <summary>
        /// Life Cycle type of Registered object
        /// </summary>
        public LifeCycleType RegisteredLifeCycleType
        {
            get;
            protected set;
        }
        /// <summary>
        /// Life cycle factory set by enum RegisteredLifeCycleType
        /// </summary>
        public ILifeCycleFactory LifeCycleFactory
        {
            get;
            protected set;
        }
        /// <summary>
        /// Creates a new instance of <see cref="Registrations"/>
        /// </summary>
        /// <param name="key">RegistrationKey</param>
        /// <param name="instancedelegate">delegate for instance </param>
        /// <param name="registeredlifeCycletype">LifeCycleType for registration</param>
        public Registrations(RegistrationKey key, Func<object> instancedelegate, LifeCycleType registeredlifeCycletype = LifeCycleType.Transient)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            RegisteredLifeCycleType = registeredlifeCycletype;
            Key = key;
            InstanceDelegate = instancedelegate;
            LifeCycleFactory = GetInstanceDelegate();
            Key = key;

        }
        public override string ToString()
        {
            return Key.ToString();
        }
        /// <summary>
        /// Set the instance of object based on Lifecycle Type
        /// </summary>
        /// <returns></returns>
        private ILifeCycleFactory GetInstanceDelegate()
        {
            ILifeCycleFactory factoryProvider = null;
            switch (RegisteredLifeCycleType)
            {
                case LifeCycleType.Singleton:
                    factoryProvider = new SingletonFactory(InstanceDelegate);
                    break;
                case LifeCycleType.Transient:
                default:
                    factoryProvider = new TransientFactory(InstanceDelegate);
                    break;
            }
            return factoryProvider;
        }

    }
}
