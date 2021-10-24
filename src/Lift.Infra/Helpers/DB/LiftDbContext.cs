using Lift.Domain.Models;
using Lift.Infra.Helpers.DB.EntityTypes;
using Lift.Infra.Helpers.ExceptionClasses;
using Microsoft.EntityFrameworkCore;
using System;

namespace Lift.Infra.Helpers.DB {
    public class LiftDbContext : DbContext {
        public LiftDbContext(DbContextOptions<LiftDbContext> options)
            : base(options) {
        }

        public DbSet<Building> Buildings { get; set; } = default!;
        public DbSet<LiftModel> Lifts { get; set; } = default!;
        public DbSet<LiftCall> LiftCalls { get; set; } = default!;
        public DbSet<LiftLog> LiftLog { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new BuidingEntityType());
            modelBuilder.ApplyConfiguration(new LiftEntityType());
            modelBuilder.ApplyConfiguration(new LiftCallEntityType());
            modelBuilder.ApplyConfiguration(new LiftLogEntityType());
        }

        public void Save() {
            try {
                this.SaveChanges();
            }
            catch (Exception e) {
                if (e.InnerException == null) {
                    throw new SqlInfraException($"Error saving DB. {e.Message}", e);
                }
                else {
                    throw new SqlInfraException($"Error saving DB. {e.InnerException.Message}", e.InnerException);
                }
            }
        }
    }
}
