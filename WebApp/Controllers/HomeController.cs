using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebApp.DbCtx;
using WebApp.Models;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _ctx;

        public HomeController(ILogger<HomeController> logger, DataContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            FinancialVM model = new FinancialVM
            {
                financialItems = _ctx.FinancialItems.Include(x => x.Partner).Select(x => new FinancialVM.FinancialItemVM
                {
                    Amount = x.Amount,
                    Date = x.Date,
                    Id = x.Id,
                    Partner = x.Partner,
                    PartnerId = x.PartnerId
                }).ToList(),
                financialEdit = new FinancialEditVM()
                {
                    Date = DateTime.Now,
                    partners = LoadPartners()
                }
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult Delete(int ID)
        {
            FinancialItem f = _ctx.FinancialItems.Find(ID);
            _ctx.FinancialItems.Remove(f);
            _ctx.SaveChanges();


            return RedirectToAction("Index");
        }

        public ActionResult DeleteAll()
        {
            _ctx.FinancialItems.RemoveRange(_ctx.FinancialItems);
            _ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Save(FinancialEditVM model)
        {
            if (model.Id == 0)
            {
                _ctx.FinancialItems.Add(new FinancialItem
                {
                    Amount = model.Amount,
                    Date = model.Date,
                    Partner = _ctx.Partners.Find(model.PartnerId),
                    PartnerId = model.PartnerId
                });
            }
            else
            {
                var itemDb = _ctx.FinancialItems.Find(model.Id);
                itemDb.PartnerId = model.PartnerId;
                itemDb.Amount = model.Amount;
                itemDb.Date = model.Date;
            }

            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string Id)
        {
            if (0 == Convert.ToInt32(Id))
                return PartialView("Add", new FinancialEditVM()
                {
                    Date = DateTime.Now,
                    partners = LoadPartners()
                });


            var f = _ctx.FinancialItems.Where(s => s.Id == Convert.ToInt32(Id)).Select(x => new FinancialEditVM
            {
                Amount = x.Amount,
                Date = x.Date,
                PartnerId = x.PartnerId,
                partners = LoadPartners()
            }).FirstOrDefault();

            return PartialView("Add", f);
        }

        public List<SelectListItem> LoadPartners()
        {
            List<SelectListItem> partners = new List<SelectListItem>();
            partners.Add(new SelectListItem { Value = "", Text = "Choose partner" });
            partners.AddRange(_ctx.Partners.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList());
            return partners;
        }
    }
}
