using Lift.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lift.Infra.Helpers.DB.EntityTypes {

    internal sealed class LiftEntityType : IEntityTypeConfiguration<LiftModel> {
        public void Configure(EntityTypeBuilder<LiftModel> builder) {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired();
            builder.Property(a => a.Flour).IsRequired();
            builder.Property(a => a.Status).IsRequired();
        }
    }
}
