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
                Children = i.Children,
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
                    TotalTeamAmount = calculateTotalShoppingAmount(x.Children)
                }).ToList();
            return View(model);
        }

        public static decimal calculateTotalShoppingAmount(Partner partner)
        {
            decimal sum = 0;

            while (partner != null) 
            {
                if(partner.FinancialItems != null)
                    sum += partner.FinancialItems.Sum(x=>x.Amount);
                partner = partner.Children;
            }
            
            return sum;
        }

        public static int getLevelPartner(Partner partner)
        {
            int count = 1;
            while (partner?.PartnerParentId != null)
            {
                count++;
                partner = partner.PartnerParent;
            }
            return count;
        }

    }
}
