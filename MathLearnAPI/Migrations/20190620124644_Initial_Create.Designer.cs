﻿// <auto-generated />
using System;
using MathLearnAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MathLearnAPI.Migrations
{
    [DbContext(typeof(acquizdbContext))]
    [Migration("20190620124644_Initial_Create")]
    partial class Initial_Create
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MathLearnAPI.Models.Awardplan", b =>
                {
                    b.Property<int>("Planid")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("planid")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Award")
                        .HasColumnName("award");

                    b.Property<string>("Createdby")
                        .HasColumnName("createdby")
                        .HasMaxLength(50);

                    b.Property<int?>("Minavgtime")
                        .HasColumnName("minavgtime");

                    b.Property<int?>("Minscore")
                        .HasColumnName("minscore");

                    b.Property<string>("Quizcontrol")
                        .HasColumnName("quizcontrol")
                        .HasMaxLength(250);

                    b.Property<short>("Quiztype")
                        .HasColumnName("quiztype");

                    b.Property<string>("Tgtuser")
                        .IsRequired()
                        .HasColumnName("tgtuser")
                        .HasMaxLength(50);

                    b.Property<DateTime>("Validfrom")
                        .HasColumnName("validfrom")
                        .HasColumnType("date");

                    b.Property<DateTime>("Validto")
                        .HasColumnName("validto")
                        .HasColumnType("date");

                    b.HasKey("Planid");

                    b.ToTable("awardplan");
                });

            modelBuilder.Entity("MathLearnAPI.Models.Knowledge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Attachment1")
                        .HasColumnName("ATTACHMENT1");

                    b.Property<string>("Attachment2")
                        .HasColumnName("ATTACHMENT2");

                    b.Property<string>("Attachment3")
                        .HasColumnName("ATTACHMENT3");

                    b.Property<bool?>("CanGenerate")
                        .HasColumnName("CAN_GENERATE");

                    b.Property<byte?>("Category")
                        .HasColumnName("CATEGORY");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnName("CONTENT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("NAME")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("knowledge");
                });

            modelBuilder.Entity("MathLearnAPI.Models.Permuser", b =>
                {
                    b.Property<string>("Userid")
                        .HasColumnName("userid")
                        .HasMaxLength(50);

                    b.Property<string>("Monitor")
                        .HasColumnName("monitor")
                        .HasMaxLength(50);

                    b.HasKey("Userid", "Monitor");

                    b.HasAlternateKey("Monitor", "Userid");

                    b.ToTable("permuser");
                });

            modelBuilder.Entity("MathLearnAPI.Models.Qbklink", b =>
                {
                    b.Property<int>("Qbid")
                        .HasColumnName("QBID");

                    b.Property<int>("Kwgid")
                        .HasColumnName("KWGID");

                    b.HasKey("Qbid", "Kwgid");

                    b.HasAlternateKey("Kwgid", "Qbid");

                    b.ToTable("qbklink");
                });

            modelBuilder.Entity("MathLearnAPI.Models.Questionbank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnName("ANSWER");

                    b.Property<string>("Attachment1")
                        .HasColumnName("ATTACHMENT1");

                    b.Property<string>("Attachment2")
                        .HasColumnName("ATTACHMENT2");

                    b.Property<string>("Attachment3")
                        .HasColumnName("ATTACHMENT3");

                    b.Property<string>("Attachment4")
                        .HasColumnName("ATTACHMENT4");

                    b.Property<string>("Attachment5")
                        .HasColumnName("ATTACHMENT5");

                    b.Property<string>("BriefCont")
                        .IsRequired()
                        .HasColumnName("BRIEF_CONT")
                        .HasMaxLength(50);

                    b.Property<byte>("Category")
                        .HasColumnName("CATEGORY");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnName("CONTENT");

                    b.HasKey("Id");

                    b.ToTable("questionbank");
                });

            modelBuilder.Entity("MathLearnAPI.Models.Quiz", b =>
                {
                    b.Property<int>("Quizid")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("quizid")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Attenduser")
                        .IsRequired()
                        .HasColumnName("attenduser")
                        .HasMaxLength(50);

                    b.Property<string>("Basicinfo")
                        .HasColumnName("basicinfo")
                        .HasMaxLength(250);

                    b.Property<short>("Quiztype")
                        .HasColumnName("quiztype");

                    b.Property<DateTime>("Submitdate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("submitdate")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("Quizid");

                    b.ToTable("quiz");
                });

            modelBuilder.Entity("MathLearnAPI.Models.Quizfaillog", b =>
                {
                    b.Property<int>("Quizid")
                        .HasColumnName("quizid");

                    b.Property<int>("Failidx")
                        .HasColumnName("failidx");

                    b.Property<string>("Expected")
                        .IsRequired()
                        .HasColumnName("expected")
                        .HasMaxLength(250);

                    b.Property<string>("Inputted")
                        .IsRequired()
                        .HasColumnName("inputted")
                        .HasMaxLength(250);

                    b.HasKey("Quizid", "Failidx");

                    b.HasAlternateKey("Failidx", "Quizid");

                    b.ToTable("quizfaillog");
                });

            modelBuilder.Entity("MathLearnAPI.Models.Quizsection", b =>
                {
                    b.Property<int>("Quizid")
                        .HasColumnName("quizid");

                    b.Property<int>("Section")
                        .HasColumnName("section");

                    b.Property<int>("Faileditems")
                        .HasColumnName("faileditems");

                    b.Property<int>("Timespent")
                        .HasColumnName("timespent");

                    b.Property<int>("Totalitems")
                        .HasColumnName("totalitems");

                    b.HasKey("Quizid", "Section");

                    b.ToTable("quizsection");
                });

            modelBuilder.Entity("MathLearnAPI.Models.Quizuser", b =>
                {
                    b.Property<string>("Userid")
                        .HasColumnName("userid")
                        .HasMaxLength(50);

                    b.Property<string>("Award")
                        .HasColumnName("award")
                        .HasMaxLength(5);

                    b.Property<string>("Awardplan")
                        .HasColumnName("awardplan")
                        .HasMaxLength(5);

                    b.Property<bool?>("Deletionflag")
                        .HasColumnName("deletionflag");

                    b.Property<string>("Displayas")
                        .IsRequired()
                        .HasColumnName("displayas")
                        .HasMaxLength(50);

                    b.Property<string>("Others")
                        .HasColumnName("others")
                        .HasMaxLength(50);

                    b.HasKey("Userid");

                    b.ToTable("quizuser");
                });

            modelBuilder.Entity("MathLearnAPI.Models.Tag", b =>
                {
                    b.Property<string>("Tag1")
                        .HasColumnName("TAG")
                        .HasMaxLength(50);

                    b.Property<short>("Reftype")
                        .HasColumnName("REFTYPE");

                    b.Property<int>("Refid")
                        .HasColumnName("REFID");

                    b.HasKey("Tag1", "Reftype", "Refid");

                    b.HasAlternateKey("Refid", "Reftype", "Tag1");

                    b.ToTable("tag");
                });

            modelBuilder.Entity("MathLearnAPI.Models.Useraward", b =>
                {
                    b.Property<int>("Aid")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("aid")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Adate")
                        .HasColumnName("adate")
                        .HasColumnType("date");

                    b.Property<int>("Award")
                        .HasColumnName("award");

                    b.Property<int?>("Planid")
                        .HasColumnName("planid");

                    b.Property<bool?>("Publish")
                        .HasColumnName("publish");

                    b.Property<int?>("Qid")
                        .HasColumnName("qid");

                    b.Property<int?>("Quizid");

                    b.Property<string>("Used")
                        .HasColumnName("used")
                        .HasMaxLength(50);

                    b.Property<string>("Userid")
                        .IsRequired()
                        .HasColumnName("userid")
                        .HasMaxLength(50);

                    b.HasKey("Aid");

                    b.HasIndex("Planid");

                    b.HasIndex("Quizid");

                    b.HasIndex("Userid");

                    b.ToTable("useraward");
                });

            modelBuilder.Entity("MathLearnAPI.Models.Permuser", b =>
                {
                    b.HasOne("MathLearnAPI.Models.Quizuser", "MonitorNavigation")
                        .WithMany("PermuserMonitorNavigation")
                        .HasForeignKey("Monitor")
                        .HasConstraintName("FK_permuser_monitor");

                    b.HasOne("MathLearnAPI.Models.Quizuser", "User")
                        .WithMany("PermuserUser")
                        .HasForeignKey("Userid")
                        .HasConstraintName("FK_permuser_user")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MathLearnAPI.Models.Qbklink", b =>
                {
                    b.HasOne("MathLearnAPI.Models.Knowledge", "Kwg")
                        .WithMany("Qbklink")
                        .HasForeignKey("Kwgid")
                        .HasConstraintName("FK_qbklink_kwid")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MathLearnAPI.Models.Questionbank", "Qb")
                        .WithMany("Qbklink")
                        .HasForeignKey("Qbid")
                        .HasConstraintName("FK_qbklink_qbid")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MathLearnAPI.Models.Quizfaillog", b =>
                {
                    b.HasOne("MathLearnAPI.Models.Quiz", "Quiz")
                        .WithMany("Quizfaillog")
                        .HasForeignKey("Quizid")
                        .HasConstraintName("FK_quizfaillog_quiz")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MathLearnAPI.Models.Quizsection", b =>
                {
                    b.HasOne("MathLearnAPI.Models.Quiz", "Quiz")
                        .WithMany("Quizsection")
                        .HasForeignKey("Quizid")
                        .HasConstraintName("FK_quizsection_quiz")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MathLearnAPI.Models.Useraward", b =>
                {
                    b.HasOne("MathLearnAPI.Models.Awardplan", "AwardPlan")
                        .WithMany()
                        .HasForeignKey("Planid");

                    b.HasOne("MathLearnAPI.Models.Quiz", "Quiz")
                        .WithMany()
                        .HasForeignKey("Quizid");

                    b.HasOne("MathLearnAPI.Models.Quizuser", "Awarduser")
                        .WithMany()
                        .HasForeignKey("Userid")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
