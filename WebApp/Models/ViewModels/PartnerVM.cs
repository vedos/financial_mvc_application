using System.Collections.Generic;

namespace WebApp.Models.ViewModels
{
    public class PartnerVM
    {
        public class PartnerInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal TotalTeamAmount { get; set; }
            public decimal TotalShoppingAmount { get; set; }
            public decimal TotalCommision { get; set; }
            public int? PartnerParentId { get; set; }
            public int Level { get; set; }
            public int LocationLevel { get; set; }

        }

        public List<PartnerInfo> treePartners { get; set; }
    }
}
