using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public class FinancialVM
    {
        public class FinancialItemVM 
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public decimal Amount { get; set; }
            public decimal PartnerId { get; set; }
            public Partner Partner { get; set; }
        }

        public FinancialEditVM financialEdit { get; set; }

        public List<FinancialItemVM> financialItems { get; set; }

    }
}
