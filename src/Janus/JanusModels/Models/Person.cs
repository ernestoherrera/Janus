using JanusModels.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanusModels.Models
{
    public class Person : IEntity
    {
        public int Id { get; set; } 

        public string ExternalKey { get; set; } 

        public string FirstName { get; set; } 

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Suffix { get; set; }

        public string UserName { get; set; }

        public string Title { get; set; }

        public string Company { get; set; }

        public string Tags { get; set; }

        public string ExtraInfo { get; set; }

        public bool Active { get; set; }

        public int Type { get; set; }

        public bool Enabled { get; set; }

        public string ApiKey { get; set; }

        public string Email { get; set; }

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

        public string Password { get; set; }

        public DateTime? PasswordExpiration { get; set; }

        public int? PasswordExpirationInterval { get; set; }

        public bool PreventEmulation { get; set; }

        public int? CardLastFour { get; set; }

        public string FacebookUserId { get; set; }

        public string LinkedInUserId { get; set; }

        public DateTime? Birthday { get; set; }

        public DateTime? Anniversary { get; set; }

        public string TwitterUserId { get; set; } 

        public bool OkayToContact { get; set; } 

        public int? ModifiedBy { get; set; } 

        public DateTime? Modified { get; set; }

        public int? CreatedBy { get; set; } 

        public DateTime? Created { get; set; } 

        public Person()
        {
            Active = true;
            Type = 0;
            Enabled = true;
            OkayToContact = true;
            Modified = DateTime.Now;
            Created = DateTime.Now;
        }
    }
}
