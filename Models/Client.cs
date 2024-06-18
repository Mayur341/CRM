using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Authorization;

namespace CRM.Models
{
    [Authorize]
    public class Client
    {
        [Key]
        public int ClientID { get; set; }

        public string ClientName { get; set; }
        public DateTime Date { get; set; }
        public string Subject { get; set; }
        public string OrganizationName { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be 10 digits.")]
        public long MobileNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [ForeignKey("Source")]
        public int SourceID { get; set; }

        [ForeignKey("ClientStatus")]
        public int ClientStatusID { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }
        

        public virtual Address Address { get; set; }
        public virtual ICollection<ClientActivity> ClientActivity { get; set; }
        public virtual FinancialDetails FinancialDetails { get; set; }
        public virtual ICollection<Lead> Lead { get; set; }
        public virtual Deal Deal { get; set; }
        public virtual Source Source { get; set; }
        public virtual ClientStage ClientStatus { get; set; }

        public virtual User User { get; set; }
    }
}
