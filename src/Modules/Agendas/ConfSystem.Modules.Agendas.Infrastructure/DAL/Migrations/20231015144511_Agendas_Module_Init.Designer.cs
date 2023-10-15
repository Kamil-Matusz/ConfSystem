﻿// <auto-generated />
using System;
using ConfSystem.Modules.Agendas.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConfSystem.Modules.Agendas.Infrastructure.DAL.Migrations
{
    [DbContext(typeof(AgendasDbContext))]
    [Migration("20231015144511_Agendas_Module_Init")]
    partial class Agendas_Module_Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("agendas")
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ConfSystem.Modules.Agendas.Domain.Submissions.Entities.Speaker", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Speakers", "agendas");
                });

            modelBuilder.Entity("ConfSystem.Modules.Agendas.Domain.Submissions.Entities.Submission", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConferenceId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Submissions", "agendas");
                });

            modelBuilder.Entity("SpeakerSubmission", b =>
                {
                    b.Property<Guid>("SpeakersId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SubmissionsId")
                        .HasColumnType("uuid");

                    b.HasKey("SpeakersId", "SubmissionsId");

                    b.HasIndex("SubmissionsId");

                    b.ToTable("SpeakerSubmission", "agendas");
                });

            modelBuilder.Entity("SpeakerSubmission", b =>
                {
                    b.HasOne("ConfSystem.Modules.Agendas.Domain.Submissions.Entities.Speaker", null)
                        .WithMany()
                        .HasForeignKey("SpeakersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConfSystem.Modules.Agendas.Domain.Submissions.Entities.Submission", null)
                        .WithMany()
                        .HasForeignKey("SubmissionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
