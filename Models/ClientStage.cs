using System.ComponentModel.DataAnnotations;

namespace CRM.Models
{
    public class ClientStage
    {
        [Key]
        public int ClientStatusID { get; set; } // Primary key
        public string StageName { get; set; }
    }
}
