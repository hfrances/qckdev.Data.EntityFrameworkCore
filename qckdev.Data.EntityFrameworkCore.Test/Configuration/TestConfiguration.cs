using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace qckdev.Data.EntityFrameworkCore.Test.Configuration
{
    
    sealed class TestConfiguration : IEntityTypeConfiguration<Entities.Test>
    {
        public void Configure(EntityTypeBuilder<Entities.Test> builder)
        {
            builder.HasKey(x => x.TestId);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Factor).HasDefaultValue(1);
            builder.Property(x => x.Spaced).HasMaxLength(20).IsFixedLength().TrimEnd();
            builder.Property(x => x.SpacedRaw).HasMaxLength(20).IsFixedLength();
        }
    }
}
