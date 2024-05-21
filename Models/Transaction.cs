using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Models
{
    public class Transaction
    {
        [Key] 
        public int Transaction_id {  get; set; }
        public int Amount { get; set; }

        [ForeignKey("Deal")]
        public int deal_id { get; set; }

       

        public DateTime Transaction_Date { get; set; }


        
        public virtual Deal Deal { get; set; } //navigation property
    }
}
