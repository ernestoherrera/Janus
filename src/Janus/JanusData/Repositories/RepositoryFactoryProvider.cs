using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanusData.Repositories
{
    public class RepositoryFactoryProvider<TProvider, TInterface> : RepositoryProvider
    {
        private static TInterface current;

        public static TInterface Current
        {
            get
            {
                if (current == null)
                    current = (TInterface)GetRepositoryInstance(typeof(TProvider), typeof(RepositoryCacheProvider), "myMethod");

                return current;
            }
        }
    }
}
