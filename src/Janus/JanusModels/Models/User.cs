using JanusModels.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanusModels.Models
{
    public class User : IEntity
    {
        public int Id { get; set; } 

        public int PersonId { get; set; } 

        public string ExternalKey { get; set; }
         
        public string UserName { get; set; } 

        public string Password { get; set; } 

        public string ApiKey { get; set; } 

        public string Email { get; set; } 

        public bool? Enabled { get; set; }

        public DateTime? LastLogin { get; set; } 

        public bool ForceReset { get; set; } 

        public string IpSecurityMask { get; set; } 

        public long? RoleMask { get; set; }

        public bool Deleted { get; set; }

        public DateTime? DeletedDate { get; set; } 

        public DateTime? LastLogout { get; set; } 

        public bool IsRestricted { get; set; } 

        public string AccessKey { get; set; } 

        public string SecretKey { get; set; } 

        public DateTime? PasswordExpiration { get; set; } 

        public int? PasswordExpirationInterval { get; set; }

        public bool PreventEmulation { get; set; } 

        public int? CardLastFour { get; set; } 

        public string StripeCustomerId { get; set; }

        public int? ModifiedBy { get; set; } 

        public DateTime? Modified { get; set; } 

        public int? CreatedBy { get; set; } 

        public DateTime? Created { get; set; }

        public User()
        {
            LastLogin = DateTime.Now;
            ForceReset = false;
            Deleted = false;
            IsRestricted = false;
            PasswordExpirationInterval = 60;
            PreventEmulation = false;
            Created = DateTime.Now;
        }

        public Person Person { get; set; }
    }
}
