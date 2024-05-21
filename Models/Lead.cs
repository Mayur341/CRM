using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authorization;

namespace CRM.Models
{
    
    public class Lead
    {
        [Key]
        public int LeadID { get; set; } // Primary key

        [ForeignKey("Client")]
        public int ClientID { get; set; } // Foreign key

        public string LeadName { get; set; }
        public DateTime LeadDate { get; set; }
        public string LeadDescription { get; set; }

        [ForeignKey("LeadStage")]
        public int LeadStageID { get; set; } // Foreign key

        public virtual ICollection<Communication> Communications { get; set; } // Navigation property
        public virtual Client Client { get; set; } // Navigation property
        public virtual LeadStage LeadStage { get; set; } // Navigation property
    }
}
