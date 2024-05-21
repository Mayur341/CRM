using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CRM.Models
{
    public class Communication
    {
        [Key]
        public int CommunicationID { get; set; }
        public string Details { get; set; }

        [ForeignKey("Lead")]
        public int LeadID { get; set; } // Foreign key

        public DateTime Date { get; set; }

        public virtual Lead Lead { get; set; } // Navigation property
    }
}