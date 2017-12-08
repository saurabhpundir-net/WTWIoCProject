using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using WTW.IoC.LifeCycleFactories;
using WTW.IoC.Enums;

namespace WTW.IoC
{
    public class Container : IContainer
    {
        /// <summary>
        /// List of all registrations in container
        /// It contains, key ,instance, lifecycle type
        /// </summary>
        private readonly List<Registrations> ContainerRegistrations;
        /// <summary>
        /// Creates a new instance of <see cref="Container"/>
        /// </summary>
        public Container(LifeCycleType containerlifecycletype = LifeCycleType.Transient)
        {
            ContainerRegistrations = new List<Registrations>();
        }

        #region [Register Section]
        /// <summary>
        /// Register a type mapping
        /// </summary>
        /// <typeparam name="TFrom">Type that will be requested</typeparam>
        /// <typeparam name="TTo">Type that will actually be returned</typeparam>
        /// <param name="registeredName">Instance name (Not Mandatory)</param>
        public void Register<TFrom, TTo>(string registeredName = null) where TTo : TFrom
        {
            LifeCycleType containerlifeCycletype = LifeCycleType.Transient;
            Register(typeof(TFrom), typeof(TTo), containerlifeCycletype, registeredName);
        }

        /// <summary>
        /// Register a type mapping
        /// </summary>
        /// <typeparam name="TFrom">Type that will be requested</typeparam>
        /// <typeparam name="TTo">Type that will actually be returned</typeparam>
        /// <param name="registeredName">Instance name (Not Mandatory)</param>
        public void Register<TFrom, TTo>(LifeCycleType containerlifeCycletype, string registeredName = null) where TTo : TFrom
        {
            Register(typeof(TFrom), typeof(TTo), containerlifeCycletype, registeredName);
        }

        /// <summary>
        /// Register a type mapping
        /// </summary>
        /// <typeparam name="T">Type that will be requested</typeparam>
        /// <param name="createInstanceDelegate">A delegate that will be used to 
        /// create an instance of the requested object</param>
        /// <param name="containerlifeCycletype">LifeCycleType enum for life cycle (Default Transient)</param>
        /// <param name="registeredName">Instance name (Not Mandatory)</param>
        public void Register<T>(Func<T> createInstanceDelegate, LifeCycleType containerlifeCycletype = LifeCycleType.Transient, string registeredName = null)
        {
            if (createInstanceDelegate == null)
                throw new ArgumentNullException("createInstanceDelegate");

            Register(typeof(T), createInstanceDelegate as Func<object>, containerlifeCycletype, registeredName);
        }

        /// <summary>
        /// Register a type mapping
        /// </summary>
        /// <param name="fromType">Type that will be requested</param>
        /// <param name="toType">Type that will actually be returned</param>
        /// <param name="containerlifeCycletype">LifeCycleType enum for life cycle (Default Transient)</param>
        /// <param name="registeredName">Instance name (Not Mandatory)</param>
        private void Register(Type fromType, Type toType, LifeCycleType containerlifeCycletype = LifeCycleType.Transient, string registeredName = null)
        {
            //if (from == null)
            //	throw new ArgumentNullException("from");

            if (toType == null)
                throw new ArgumentNullException("toType");

            if (!fromType.IsAssignableFrom(toType))
            {
                throw new InvalidOperationException($"Error trying to register the instance: " +
                    $"{fromType.FullName} is not assignable from {toType.FullName}");
            }

            Register(fromType, () => Activator.CreateInstance(toType), containerlifeCycletype, registeredName);
        }
        /// <summary>
        /// Register a type mapping
        /// </summary>
        /// <param name="type">Type that will be requested</param>
        /// <param name="createInstanceDelegate">A delegate that will be used to 
        /// create an instance of the requested object</param>
        /// <param name="containerlifeCycletype">LifeCycleType enum for life cycle (Default Transient)</param>
        /// <param name="registeredName">Instance name (Not Mandatory)</param>
        public void Register(Type type, Func<object> createInstanceDelegate, LifeCycleType containerlifeCycletype = LifeCycleType.Transient, string registeredName = null)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (createInstanceDelegate == null)
                throw new ArgumentNullException("createInstanceDelegate");

            var key = new RegistrationKey(type, registeredName);

            if (ContainerRegistrations.Exists(k => k.Key.Equals(key)))
            {
                throw new InvalidOperationException($"The requested registered already exists - {key.ToString()}");
            }
            ContainerRegistrations.Add(new Registrations(key, createInstanceDelegate, containerlifeCycletype));
        }

        #endregion

        #region [IsRegistered Section]
        /// <summary>
        /// Check if a particular type/instance name has been registered with the container
        /// </summary>
        /// <typeparam name="T">Type to check registration for</typeparam>
        /// <param name="registeredName">Instance name (Not Mandatory)</param>
        /// <returns><c>true</c>if the type/instance name has been registered 
        /// with the container; otherwise <c>false</c></returns>
        public bool IsRegistered<T>(string registeredName = null)
        {
            return IsRegistered(typeof(T), registeredName);
        }

        /// <summary>
        /// Check if a particular type/instance name has been registered with the container
        /// </summary>
        /// <param name="type">Type to check registration for</param>
        /// <param name="registeredName">Instance name (Not Mandatory)</param>
        /// <returns><c>true</c>if the type/instance name has been registered 
        /// with the container; otherwise <c>false</c></returns>
        private bool IsRegistered(Type type, string registeredName = null)
        {
            if (type == null)
                throw new ArgumentNullException("type");


            var key = new RegistrationKey(type, registeredName);

            return ContainerRegistrations.Exists(k => k.Key.Equals(key));
        }

        #endregion

        #region [Resolve Section]
        /// <summary>
        /// Resolve an instance of the requested type from the container.
        /// </summary>
        /// <typeparam name="T">Requested type</typeparam>
        /// <param name="registeredName">Instance name (Not Mandatory)</param>
        /// <returns>The retrieved object</returns>
        public T Resolve<T>(string registeredName = null)
        {
            Type type = typeof(T);
            object instance = null;
            if (IsRegistered(type, registeredName))
            {
                instance = Resolve(type, registeredName);
            }
            else
            {
                instance = CreateConstructorInstance(type);
            }

            return (T)instance;
        }
        /// <summary>
        /// Resolve an instance of the requested type from the container.
        /// </summary>
        /// <param name="type">Requested type</param>
        /// <param name="registeredName">Instance name (Not Mandatory)</param>
        /// <returns>The retrieved object</returns>
        private object Resolve(Type type, string registeredName = null)
        {
            var key = new RegistrationKey(type, registeredName);


            var registration = ContainerRegistrations.Find(f => f.Key.Equals(key));
            if (registration != null)
            {
                return GetInstanceDelegate(registration);
            }

            const string errorMessageFormat = "Could not find mapping for type '{0}'";
            throw new InvalidOperationException(string.Format(errorMessageFormat, type.FullName));
        }
        /// <summary>
        /// Create object instance for resolve for looking into constructor and filling registerd objects
        /// </summary>
        /// <param name="type"> Requested type</param>
        /// <returns></returns>
        private object CreateConstructorInstance(Type type)
        {
            object createdInstance = null;

            ConstructorInfo[] constructors = type.GetConstructors();
            //todo: get only 1 constructor
            foreach (var construct in constructors)
            {
                ParameterInfo[] parameters = construct.GetParameters();
                createdInstance = construct.Invoke(
                       ResolveParameters(parameters)
                           .ToArray());
            }

            return createdInstance;
        }
        /// <summary>
        /// Resolve all parameters in the constrcuctor
        /// </summary>
        /// <param name="parameters">All  params</param>
        /// <returns></returns>
        private IEnumerable<object> ResolveParameters(
               IEnumerable<ParameterInfo> parameters)
        {
            return parameters
                .Select(p => Resolve(p.ParameterType))
                .ToList();
        }

        /// <summary>
        /// Get the instance of obbject based on Lifecycle factory set at register
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        private object GetInstanceDelegate(Registrations registration)
        {
            ILifeCycleFactory factoryProvider = registration.LifeCycleFactory;
            var instance = factoryProvider.Create();
            return instance;
        }
        #endregion

        /// <summary>
        /// For testing & debugging purposes only
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (ContainerRegistrations == null)
                return "No mappings";

            return string.Join(Environment.NewLine, ContainerRegistrations.Select(c => c.ToString()));
        }
    }
}
