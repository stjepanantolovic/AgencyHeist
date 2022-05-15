﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoneyHeist2.Data;

#nullable disable

namespace MoneyHeist2.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220515141118_test1")]
    partial class test1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MemberSkill", b =>
                {
                    b.Property<Guid>("MembersID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SkillsID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MembersID", "SkillsID");

                    b.HasIndex("SkillsID");

                    b.ToTable("MemberSkill");
                });

            modelBuilder.Entity("MoneyHeist2.Entities.Member", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MainSkillID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SexID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StatusID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("MainSkillID");

                    b.HasIndex("SexID");

                    b.HasIndex("StatusID");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("MoneyHeist2.Entities.MemberStatus", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("MemberStatus");
                });

            modelBuilder.Entity("MoneyHeist2.Entities.Sex", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

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
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Level")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Skill");
                });

            modelBuilder.Entity("MemberSkill", b =>
                {
                    b.HasOne("MoneyHeist2.Entities.Member", null)
                        .WithMany()
                        .HasForeignKey("MembersID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoneyHeist2.Entities.Skill", null)
                        .WithMany()
                        .HasForeignKey("SkillsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
#pragma warning restore 612, 618
        }
    }
}
