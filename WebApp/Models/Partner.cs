using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class Partner: IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Partner PartnerParent { get; set; }

        public decimal PartnerParentId { get; set; }

        public decimal FeePercent { get; set; }
        public string Name { get; set; }

    }
}
