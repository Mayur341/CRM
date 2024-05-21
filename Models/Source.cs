using System.ComponentModel.DataAnnotations;

namespace CRM.Models
{
    public class Source
    {
        [Key]
        public int SourceID { get; set; } // Primary key
        public string SourceName { get; set; }
    }
}
