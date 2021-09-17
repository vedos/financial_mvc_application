using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class FinancialItem: IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int PartnerId { get; set; }
        public Partner Partner { get; set; }
    }
}
