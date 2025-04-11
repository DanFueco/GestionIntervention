﻿// <auto-generated />
using System;
using GestionIntervention;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestionIntervention.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250411075833_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GestionIntervention.Models.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("GestionIntervention.Models.Entities.Intervention", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("TypeId");

                    b.ToTable("Interventions");
                });

            modelBuilder.Entity("GestionIntervention.Models.Entities.Technicien", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Techniciens");
                });

            modelBuilder.Entity("GestionIntervention.Models.Entities.TypeIntervention", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("TypeInterventions");
                });

            modelBuilder.Entity("InterventionTechnicien", b =>
                {
                    b.Property<int>("InterventionsId")
                        .HasColumnType("int");

                    b.Property<int>("TechniciensId")
                        .HasColumnType("int");

                    b.HasKey("InterventionsId", "TechniciensId");

                    b.HasIndex("TechniciensId");

                    b.ToTable("InterventionTechnicien", (string)null);
                });

            modelBuilder.Entity("GestionIntervention.Models.Entities.Intervention", b =>
                {
                    b.HasOne("GestionIntervention.Models.Entities.Client", "Client")
                        .WithMany("Interventions")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionIntervention.Models.Entities.TypeIntervention", "Type")
                        .WithMany("Interventions")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("InterventionTechnicien", b =>
                {
                    b.HasOne("GestionIntervention.Models.Entities.Intervention", null)
                        .WithMany()
                        .HasForeignKey("InterventionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionIntervention.Models.Entities.Technicien", null)
                        .WithMany()
                        .HasForeignKey("TechniciensId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GestionIntervention.Models.Entities.Client", b =>
                {
                    b.Navigation("Interventions");
                });

            modelBuilder.Entity("GestionIntervention.Models.Entities.TypeIntervention", b =>
                {
                    b.Navigation("Interventions");
                });
#pragma warning restore 612, 618
        }
    }
}
