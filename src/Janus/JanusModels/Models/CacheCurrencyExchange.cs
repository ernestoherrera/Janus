using JanusModels.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanusModels.Models
{
    public class CacheCurrencyExchange : IEntity
    {
        public int CycleToProcessId { get; set; }
        public int FromCurrencyId { get; set; }
        public int ToCurrencyId { get; set; }
    }
}
