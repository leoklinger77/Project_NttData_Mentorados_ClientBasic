using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Valhalla.Dominio.Models;

namespace Valhalla.Infra.Data
{
    public class ValhallaContext : DbContext
    {
        public ValhallaContext(DbContextOptions<ValhallaContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Phone> Phones { get; set; }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker.Entries().Where(x => x.Entity.GetType().GetProperty("InsertDate") != null &&
                                                                      x.Entity.GetType().GetProperty("UpdateDate") != null))
            {
                if(entity.State == EntityState.Added)
                {
                    entity.Property("InsertDate").CurrentValue = DateTime.Now;
                    entity.Property("UpdateDate").IsModified = false;
                }

                if (entity.State == EntityState.Modified)
                {
                    entity.Property("UpdateDate").CurrentValue = DateTime.Now;
                    entity.Property("InsertDate").IsModified = false;
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
