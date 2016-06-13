using JanusData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanusData.Repositories
{
    public class RepositoryProvider
    {
        private static ICacheRepository cacheRepository;

        public static object GetRepositoryInstance(Type provider, Type cacheProvider, string callingMethodName)
        {
            object resultingObject = null;

            if (typeof(ICacheRepository).GetMethods().Where(m => m.Name == callingMethodName).FirstOrDefault() != null)
            {
                resultingObject = Activator.CreateInstance(cacheProvider);
            }
            else
            {
                resultingObject = Activator.CreateInstance(provider);
            }

            return resultingObject;
        }
    }
}
