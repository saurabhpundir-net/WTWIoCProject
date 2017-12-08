using System;
using System.Collections.Generic;
using System.Text;
using WTW.IoC.LifeCycleFactories;

namespace WTW.IoC
{
    internal class RegistrationKey
    {
        /// <summary>
		/// Type of the registered dependency
		/// </summary>
		public Type Type
        {
            get;
            protected set;
        }

        /// <summary>
		/// Name of the instance (optional)
		/// </summary>
		public string InstanceName
        {
            get;
            protected set;
        }

        /// <summary>
        /// Creates a new instance of <see cref="RegistrationKey"/>
        /// </summary>
        /// <param name="type">Type of the dependency</param>
        /// <param name="instanceName">Name of the instance</param>
        /// <exception cref="ArgumentNullException">type</exception>
        public RegistrationKey(Type type, string instanceName)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            Type = type;
            InstanceName = instanceName;
        }

        public override string ToString()
        {
            const string format = "{0} ({1}) - hash code: {2}";
            return string.Format(format, InstanceName ?? "[null]",
                  Type.FullName,
                  GetHashCode()
              );
        }
        public override int GetHashCode()
        {
            unchecked
            {
                const int multiplier = 31;
                int hash = GetType().GetHashCode();

                hash = hash * multiplier + Type.GetHashCode();
                hash = hash * multiplier + (InstanceName == null ? 0 : InstanceName.GetHashCode());

                return hash;
            }
        }


        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object</param>
        /// <returns>
        /// <c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var compareTo = obj as RegistrationKey;

            if (ReferenceEquals(this, compareTo))
                return true;

            if (compareTo == null)
                return false;

            return Type == compareTo.Type &&
                string.Equals(InstanceName, compareTo.InstanceName, StringComparison.CurrentCultureIgnoreCase);
        }
        /// <summary>
        /// Get the string value t0 show in error
        /// </summary>
        /// <returns></returns>
        public string GetErrorValue()
        {
            return $"Instance Name: {InstanceName ?? "[null]"} ({Type.FullName})";
        }
    }
}
