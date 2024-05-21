using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Models
{
    public class Deal
    {
        [Key]
        public int deal_id { get; set; }

        [ForeignKey("Client")]
        public int ClientID { get; set; } // Foreign key

        public string deal_name { get; set; }
        public string deal_description { get; set; }
        public DateTime deal_date { get; set; }

        public int Total_Amount { get; set; }

        public int amount_received {  get; set; }

        public int pending_amount { get; set; }

        public DateTime closing_date { get; set; }

        public string description { get; set; }

        public string product_name { get;set; }

        public virtual Client Client { get; set; } // Navigation property

        public virtual ICollection<Transaction> Transactions { get; set; }



    }
}
