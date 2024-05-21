using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CRM.Models
{
    public class FinancialDetails
    {
        [Key]
        public int FID { get; set; }

        [ForeignKey("Client")]
        public int ClientID { get; set; }

        public int AnnualIncome { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Income source must contain only letters.")]
        public string IncomeSource { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Occupation must contain only letters.")]
        public string Occupation { get; set; }

        public virtual Client Client { get; set; }
    }
}
