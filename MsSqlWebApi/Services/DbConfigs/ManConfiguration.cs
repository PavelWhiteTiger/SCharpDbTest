using Core.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MsSqlWebApi.Services.DbConfigs;

public class ManConfiguration : IEntityTypeConfiguration<Man>
{
    public void Configure(EntityTypeBuilder<Man> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(25);
        builder.Property(x => x.LastName).HasMaxLength(25);
        //  builder.HasCheckConstraint("Age", "Age > 0 AND Age < 100");
        //  builder.Property(x => x.FullName).HasComputedColumnSql("Name + ' ' +  LastName");
    }
}