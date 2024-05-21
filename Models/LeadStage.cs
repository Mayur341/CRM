using System.ComponentModel.DataAnnotations;

namespace CRM.Models
{
    public class LeadStage
    {
        [Key]
        public int LeadStageID { get; set; } // Primary key
        public string LeadStageName { get; set; }
    }
}
