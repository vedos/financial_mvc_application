using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public class FinancialEditVM
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int PartnerId { get; set; }
        public DateTime Date { get; set; }

        public List<SelectListItem> partners { get; set; }

    }
}
