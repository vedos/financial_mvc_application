using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.DbCtx
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Partner>().HasOne(p => p.PartnerParent).WithMany(p=>p.Childrens);
            modelBuilder.Entity<FinancialItem>();
        }

        public DbSet<FinancialItem> FinancialItems { get; set; }

        public DbSet<Partner> Partners { get; set; }

        #region avoid referrence loop
        /*
        /// <summary>  
        /// Overriding Save Changes  
        /// </summary>  
        /// <returns></returns>  
        public override int SaveChanges()
        {
            var selectedEntityList = ChangeTracker.Entries()
                                    .Where(x => x.Entity is Partner &&
                                    (x.State == EntityState.Added || x.State == EntityState.Modified));


            foreach (var entity in selectedEntityList)
            {
                ((Partner)entity.Entity).PartnerParent = avoidReferrencinLoop(((Partner)entity.Entity), ((Partner)entity.Entity).PartnerParent);                
            }

            return base.SaveChanges();
        }

        public static Partner avoidReferrencinLoop(Partner partner, Partner partnerParent)
        {
            if (partnerParent == null)
                return partner.PartnerParent;

            if (partner.Id == partnerParent.PartnerParentId)
                return null;

            if (partner.PartnerParentId == partnerParent.PartnerParentId)
                return null;


            return avoidReferrencinLoop(partner, partnerParent.PartnerParent == null ? partnerParent.PartnerParent : null);
        }*/
        #endregion
    }
}
