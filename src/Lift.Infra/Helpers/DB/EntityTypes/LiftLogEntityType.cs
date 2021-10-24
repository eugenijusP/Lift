using Lift.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lift.Infra.Helpers.DB.EntityTypes {

    internal sealed class LiftLogEntityType : IEntityTypeConfiguration<LiftLog> {
        public void Configure(EntityTypeBuilder<LiftLog> builder) {
            builder.HasKey(a => new { a.LiftId, a.ActionTime });
            builder.Property(a => a.LiftId).IsRequired();
            builder.Property(a => a.ActionTime).IsRequired();
            builder.Property(a => a.Flour).IsRequired();
            builder.Property(a => a.Status).IsRequired();
        }
    }
}
