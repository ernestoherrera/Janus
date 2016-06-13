using JanusCore.Injection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanusData.Factories
{
    public class GenericFactory
    {
        private IInjectionContainer container;

        public GenericFactory(IInjectionContainer container)
        {
            this.container = container;
        }

        public T Get<T>() where T : class
        {
            return container.Get<T>();
        }

    }
}
