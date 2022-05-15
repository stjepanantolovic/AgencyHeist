using Microsoft.EntityFrameworkCore;
using MoneyHeist2.Entities;

namespace MoneyHeist2.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Member> Members { get; set; }
        public DbSet<Sex> Sex { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<MemberStatus> MemberStatus { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("Default");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Member>()
                .HasOne<MemberStatus>(m => m.Status);
            builder.Entity<Member>()
                .HasOne<Skill>(m => m.MainSkill);
            builder.Entity<Member>()
                .HasOne<Sex>(m => m.Sex);

            builder.Entity<Member>()
               .HasMany<Skill>(m => m.Skills)
               .WithMany(s => s.Members);
        }
    }
}
