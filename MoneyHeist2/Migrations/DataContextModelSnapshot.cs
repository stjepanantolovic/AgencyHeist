﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoneyHeist2.Data;

#nullable disable

namespace MoneyHeist2.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HeistMember", b =>
                {
                    b.Property<Guid>("HeistsID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MembersID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("HeistsID", "MembersID");

                    b.HasIndex("MembersID");

                    b.ToTable("HeistMember");
                });

            modelBuilder.Entity("MemberSkillLevel", b =>
                {
                    b.Property<Guid>("MembersID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SkillLevelsID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MembersID", "SkillLevelsID");

                    b.HasIndex("SkillLevelsID");

                    b.ToTable("MemberSkillLevel");
                });

            modelBuilder.Entity("MoneyHeist2.Entities.Heist", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Heists");
                });

            modelBuilder.Entity("MoneyHeist2.Entities.HeistSkillLevel", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("HeistID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Members")
                        .HasColumnType("int");

                    b.Property<Guid?>("SkillLevelID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("HeistID");

                    b.HasIndex("SkillLevelID");

                    b.ToTable("HeistSkillLevels");
                });

            modelBuilder.Entity("MoneyHeist2.Entities.Level", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("Value")
                        .IsUnique();

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("MoneyHeist2.Entities.Member", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid?>("MainSkillID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SexID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StatusID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("MainSkillID");

                    b.HasIndex("SexID");

                    b.HasIndex("StatusID");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("MoneyHeist2.Entities.MemberStatus", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("MemberStatus");
                });

            modelBuilder.Entity("MoneyHeist2.Entities.Sex", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Sex");
                });

            modelBuilder.Entity("MoneyHeist2.Entities.Skill", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Skill");
                });

            modelBuilder.Entity("MoneyHeist2.Entities.SkillLevel", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LevelID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SkillID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("LevelID");

                    b.HasIndex("SkillID", "LevelID")
                        .IsUnique();

                    b.ToTable("SkillLevels");
                });

            modelBuilder.Entity("HeistMember", b =>
                {
                    b.HasOne("MoneyHeist2.Entities.Heist", null)
                        .WithMany()
                        .HasForeignKey("HeistsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoneyHeist2.Entities.Member", null)
                        .WithMany()
                        .HasForeignKey("MembersID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MemberSkillLevel", b =>
                {
                    b.HasOne("MoneyHeist2.Entities.Member", null)
                        .WithMany()
                        .HasForeignKey("MembersID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoneyHeist2.Entities.SkillLevel", null)
                        .WithMany()
                        .HasForeignKey("SkillLevelsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MoneyHeist2.Entities.HeistSkillLevel", b =>
                {
                    b.HasOne("MoneyHeist2.Entities.Heist", null)
                        .WithMany("HeistSkillLevels")
                        .HasForeignKey("HeistID");

                    b.HasOne("MoneyHeist2.Entities.SkillLevel", "SkillLevel")
                        .WithMany()
                        .HasForeignKey("SkillLevelID");

                    b.Navigation("SkillLevel");
                });

            modelBuilder.Entity("MoneyHeist2.Entities.Member", b =>
                {
                    b.HasOne("MoneyHeist2.Entities.Skill", "MainSkill")
                        .WithMany()
                        .HasForeignKey("MainSkillID");

                    b.HasOne("MoneyHeist2.Entities.Sex", "Sex")
                        .WithMany()
                        .HasForeignKey("SexID");

                    b.HasOne("MoneyHeist2.Entities.MemberStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusID");

                    b.Navigation("MainSkill");

                    b.Navigation("Sex");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("MoneyHeist2.Entities.SkillLevel", b =>
                {
                    b.HasOne("MoneyHeist2.Entities.Level", "Level")
                        .WithMany()
                        .HasForeignKey("LevelID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoneyHeist2.Entities.Skill", "Skill")
                        .WithMany()
                        .HasForeignKey("SkillID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Level");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("MoneyHeist2.Entities.Heist", b =>
                {
                    b.Navigation("HeistSkillLevels");
                });
#pragma warning restore 612, 618
        }
    }
}
