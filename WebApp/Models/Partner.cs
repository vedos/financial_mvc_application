namespace WebApp.Models
{
    public class Partner
    {
        public decimal Id { get; set; }
        public Partner PartnerParent { get; set; }

        public decimal PartnerParentId { get; set; }

        public decimal FeePercent { get; set; }
        public string Name { get; set; }

    }
}
