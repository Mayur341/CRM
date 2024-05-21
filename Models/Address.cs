using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CRM.Models
{
    public class Address
    {
        [Key]
        public int AddressID { get; set; }

        [ForeignKey("Client")]
        public int ClientID { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "City must contain only letters.")]
        public string City { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "State must contain only letters.")]
        public string State { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Country must contain only letters.")]
        public string Country { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int Pincode { get; set; }

        public virtual Client Client { get; set; }
    }
}
