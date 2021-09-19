using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using WebApp.DbCtx;
using WebApp.Models;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    public class FeeCalculationController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _ctx;

        public FeeCalculationController(ILogger<HomeController> logger, DataContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            PartnerVM model = new PartnerVM();
            List<Partner> dbPartners = _ctx.Partners.Select(i => new Partner()
            {
                Id = i.Id,
                Childrens = i.Childrens,
                FeePercent = i.FeePercent,
                Name = i.Name,
                PartnerParent = i.PartnerParent,
                PartnerParentId = i.PartnerParentId,
                FinancialItems = _ctx.FinancialItems.Where(x => x.PartnerId == i.Id).ToList().Count > 0 ? _ctx.FinancialItems.Where(x => x.PartnerId == i.Id).ToList() : null
            }
            ).ToList();
           

            model.treePartners = dbPartners
                .Select(x => new PartnerVM.PartnerInfo()
                {
                    Id = x.Id,
                    Level = getLevelPartner(x),
                    Name = x.Name,
                    PartnerParentId = x.PartnerParentId,
                    TotalShoppingAmount = calculateTotalShoppingAmount(x),
                    TotalTeamAmount = calculateTotalTeamShoppingAmount(x.Childrens),
                    TotalCommision = calculateChildresnTotalCommision(x,x) + calculateTotalCommision(x)

                }).ToList();
            

            return View(model);
        }

        public static decimal calculateTotalCommision(Partner partner)
        {
            decimal sum = 0;

            if (partner.FinancialItems != null)
                sum = partner.FinancialItems.Sum(x => x.Amount) * partner.FeePercent;

            return sum;
        }

        public static decimal calculateChildresnTotalCommision(Partner partner, Partner nextChild)
        {
            decimal sum = 0;
            List<Partner> childs = null;
            if (nextChild.Childrens != null && nextChild.Childrens.ToList().Count > 0)
                childs = nextChild.Childrens;
            else
                return 0;

            foreach (Partner child in childs)
            {
                if (child.FinancialItems != null && partner.FeePercent > child.FeePercent)
                    sum += child.FinancialItems.Sum(x => x.Amount) * (partner.FeePercent - child.FeePercent);//+ calculateChildresnTotalCommision(partner,child)

                sum += calculateChildresnTotalCommision(partner, child);
            }

            return sum;
        }

        public static decimal calculateTotalTeamShoppingAmount(List<Partner> childrens)
        {
            decimal sum = 0;

            if (childrens == null || childrens.Count <= 0)
                return 0;

            foreach (Partner child in childrens)
            {
                if (child.FinancialItems == null)
                    sum += 0 + calculateTotalTeamShoppingAmount(child.Childrens);
                else
                    sum += child.FinancialItems.Sum(x => x.Amount) + calculateTotalTeamShoppingAmount(child.Childrens);
            }

            return sum;
        }

        public static decimal calculateTotalShoppingAmount(Partner partner)
        {
            decimal sum = 0;
            if (partner.FinancialItems != null)
                sum += partner.FinancialItems.Sum(x => x.Amount);

            return sum;
        }

        public static int getLevelPartner(Partner partner)
        {
            int count = 1;

            while (partner.PartnerParent != null)
            {
                count++;
                partner = partner.PartnerParent;
            }
            return count;
        }

    }
}
