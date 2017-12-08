using System;
using System.Collections.Generic;
using System.Text;

namespace WTW.Models
{
    public class BMW : ICar
    {
        private int _miles = 0;

        public int Run()
        {
            return ++_miles;
        }
        public string Name()
        {
            return this.GetType().Name;
        }
    }
}
