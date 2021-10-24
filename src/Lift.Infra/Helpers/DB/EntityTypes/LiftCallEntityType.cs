using Lift.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lift.Infra.Helpers.DB.EntityTypes {

    internal sealed class LiftCallEntityType : IEntityTypeConfiguration<LiftCall> {
        public void Configure(EntityTypeBuilder<LiftCall> builder) {
            builder.HasKey(a => new { a.LiftId, a.ActionTime });
            builder.Property(a => a.LiftId).IsRequired();
            builder.Property(a => a.ActionTime).IsRequired();
            builder.Property(a => a.Flour).IsRequired();
            builder.Property(a => a.Active).IsRequired();
        }
    }
}
