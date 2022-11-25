using Core.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsSqlWebApi.Services.DbConfigs;

namespace MsSqlWebApi;

public sealed class ApplicationContext : DbContext
{
    public DbSet<Battle> Battles => Set<Battle>();
    public DbSet<Blacksmith> Blacksmiths => Set<Blacksmith>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<Man> Men => Set<Man>();
    public DbSet<Warrior> Warriors => Set<Warrior>();
    public DbSet<Weapon> Weapons => Set<Weapon>();

    /*public ApplicationContext()
    {
        
    }*/
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // вариант 1
        modelBuilder.ApplyConfiguration(new ManConfiguration());
        modelBuilder.ApplyConfiguration(new WarriorConfiguration());
        // вариант 2
        modelBuilder.Entity<Blacksmith>(BuildAction);
        MockDataBase(modelBuilder);
    }

    private static void MockDataBase(ModelBuilder modelBuilder)
    {
        // will be factory

        var country = new Country
        {
            Id = 1,
            Name = "ru"
        };
        var country2 = new Country
        {
            Id = 2,
            Name = "belaRu"
        };

        var weapon1 = new Weapon
        {
            Id = 1,
            AttackRange = 20,
            Damage = 100,
            Name = "bow"
        };

        var weapon2 = new Weapon
        {
            Id = 2,
            AttackRange = 400,
            Damage = 20,
            Name = "sword"
        };
        modelBuilder.Entity<Country>().HasData(country, country2);
        modelBuilder.Entity<Weapon>().HasData(weapon1, weapon2);

        var man1 = new Warrior
        {
            Id = 1,
            Level = 1,
            Name = "war1",
            LastName = "wawa1",
            DateOfBirth = DateTime.Now,
            CountryId = country.Id,
            WeaponId = weapon1.Id
        };
        var man2 = new Warrior
        {
            Id = 2,
            Level = 1,
            Name = "war2",
            LastName = "wawa2",
            DateOfBirth = DateTime.Now,
            CountryId = country2.Id,
            WeaponId = weapon2.Id
        };
        var man3 = new Blacksmith()
        {
            Id = 3,
            Name = "Black1",
            LastName = "wawa1",
            DateOfBirth = DateTime.Now,
            CountryId = country2.Id,
            WeaponId = weapon2.Id
        };
        var man4 = new Blacksmith
        {
            Id = 4,
            Name = "Black2",
            LastName = "wawa2",
            DateOfBirth = DateTime.Now,
            CountryId = country.Id,
            WeaponId = weapon1.Id
        };
        modelBuilder.Entity<Warrior>().HasData(man1, man2);
        modelBuilder.Entity<Blacksmith>().HasData(man3, man4);
    }

    private void BuildAction(EntityTypeBuilder<Blacksmith> typeBuilder)
    {
        typeBuilder.ToTable(nameof(Blacksmiths));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");
    }
}