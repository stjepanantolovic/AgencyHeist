﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<Level> Levels { get; set; }
        public DbSet<SkillLevel> SkillLevels { get; set; }
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

            builder.Entity<SkillLevel>().HasIndex(entity => new { entity.SkillID, entity.LevelID }).IsUnique(true);
            builder.Entity<Member>()
               .HasMany<SkillLevel>(m => m.SkillLevels)
               .WithMany(s => s.Members);
               

            //builder.Entity<Skill>()
            //   .HasMany<SkillLevel>(s => s.SkillLevels)
            //   .WithMany(sl => sl.Skills);

            //builder.Entity<Member>().Property(x => x.ID).HasDefaultValueSql("NEWID()");
            //builder.Entity<MemberStatus>().Property(x => x.ID).HasDefaultValueSql("NEWID()");
            //builder.Entity<Sex>().Property(x => x.ID).HasDefaultValueSql("NEWID()");
            //builder.Entity<Skill>().Property(x => x.ID).HasDefaultValueSql("NEWID()");
            //builder.Entity<SkillLevel>().Property(x => x.ID).HasDefaultValueSql("NEWID()");
        }
    }
}
