using Core.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MsSqlWebApi.Services.DbConfigs;

public class WarriorConfiguration : IEntityTypeConfiguration<Warrior>
{
    public void Configure(EntityTypeBuilder<Warrior> builder)
    {
        builder.Property(x => x.Level).HasDefaultValue(1);
        builder.ToTable("Warriors");
    }
}