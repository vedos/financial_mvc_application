using System;

namespace WebApp.Models
{
    public class FinancialItem
    {
        public decimal Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public decimal PartnerId { get; set; }
        public Partner Partner { get; set; }
    }
}
