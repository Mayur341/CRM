using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Models
{
    public class ClientActivity
    {
        [Key]
        public int clientActivityID { get; set; }

        [ForeignKey("Client")]
        public int ClientID { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Activity type must contain only letters.")]
        public string Activity_Type { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Description must contain only letters.")]
        public string Activity_Description { get; set; }

        public DateTime Activity_Date { get; set; }

        public string? Added_By_SalesExe { get; set; }

        public virtual Client Client { get; set; }
    }
}
