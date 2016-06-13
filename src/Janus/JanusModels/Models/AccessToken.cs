using JanusModels.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanusModels.Models
{
    public class AccessToken : IEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Token { get; set; }

        public DateTime Expires { get; set; }

        public bool IsRevoked { get; set; }

        public bool IsValid { get { return DateTime.Now <= Expires && !IsRevoked; } }
    }
}
