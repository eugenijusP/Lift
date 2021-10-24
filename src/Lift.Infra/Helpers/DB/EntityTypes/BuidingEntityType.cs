using Lift.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lift.Infra.Helpers.DB.EntityTypes {

    internal sealed class BuidingEntityType : IEntityTypeConfiguration<Building>{
        public void Configure(EntityTypeBuilder<Building> builder) {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired();
            builder.Property(a => a.LiftCount).IsRequired();
            builder.Property(a => a.FlourCount).IsRequired();
        }
    }
}
