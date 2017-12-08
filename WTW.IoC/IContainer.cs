using System;
using System.Collections.Generic;
using System.Text;
using WTW.IoC.Enums;

namespace WTW.IoC
{
    /// <summary>
    ///  Interface defining the behavior of the Ioc container.
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Register a type mapping with name
        /// </summary>
        /// <typeparam name="TFrom">Type that will be requested</typeparam>
        /// <typeparam name="TTo">Type that will actually be returned</typeparam>
        /// <param name="registeredName">Instance name (Not Mandatory)</param>
        void Register<TFrom, TTo>(string registeredName = null) where TTo : TFrom;
        /// <summary>
        /// Register a type mapping with container life cycle
        /// </summary>
        /// <typeparam name="TFrom">Type that will be requested</typeparam>
        /// <typeparam name="TTo">Type that will actually be returned</typeparam>
        /// <param name="containerlifeCycletype">LifeCycleType enum</param>
        /// <param name="registeredName">Instance name (Not Mandatory)</param>
        void Register<TFrom, TTo>(LifeCycleType containerlifeCycletype, string registeredName = null) where TTo : TFrom;

        /// <summary>
        /// Register a type mapping
        /// </summary>
        /// <typeparam name="T">Type that will be requested</typeparam>
        /// <param name="createInstanceDelegate">A delegate that will be used to 
        /// create an instance of the requested object</param>
        /// <param name="registeredName">Instance name (Not Mandatory)</param>
        void Register<T>(Func<T> createInstanceDelegate, LifeCycleType containerlifeCycletype,string registeredName = null);


        /// <summary>
        /// Check if a particular type/instance name has been registered with the container
        /// </summary>
        /// <typeparam name="T">Type to check registration for</typeparam>
        /// <param name="registeredName">Instance name (Not Mandatory)</param>
        /// <returns><c>true</c>if the type/instance name has been registered 
        /// with the container; otherwise <c>false</c></returns>
        bool IsRegistered<T>(string registeredName = null);


         /// <summary>
        /// Resolve an instance of the requested type from the container.
        /// </summary>
        /// <typeparam name="T">Requested type</typeparam>
        /// <param name="registeredName">Instance name (Not Mandatory)</param>
        /// <returns>The retrieved object</returns>
        T Resolve<T>(string registeredName = null);
    }
}
