using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathLearnAPI.Models
{
    public partial class acquizdbContext : DbContext
    {
        public acquizdbContext()
        {
        }

        public acquizdbContext(DbContextOptions<acquizdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Awardplan> Awardplan { get; set; }
        public virtual DbSet<Knowledge> Knowledge { get; set; }
        public virtual DbSet<Permuser> Permuser { get; set; }
        public virtual DbSet<Qbklink> Qbklink { get; set; }
        public virtual DbSet<Questionbank> Questionbank { get; set; }
        public virtual DbSet<Quiz> Quiz { get; set; }
        public virtual DbSet<Quizfaillog> Quizfaillog { get; set; }
        public virtual DbSet<Quizsection> Quizsection { get; set; }
        public virtual DbSet<Quizuser> Quizuser { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<Useraward> Useraward { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Awardplan>(entity =>
            {
                entity.HasKey(e => e.Planid);

                entity.ToTable("awardplan");

                entity.Property(e => e.Planid).HasColumnName("planid");

                entity.Property(e => e.Award).HasColumnName("award");

                entity.Property(e => e.Createdby)
                    .HasColumnName("createdby")
                    .HasMaxLength(50);

                entity.Property(e => e.Minavgtime).HasColumnName("minavgtime");

                entity.Property(e => e.Minscore).HasColumnName("minscore");

                entity.Property(e => e.Quizcontrol)
                    .HasColumnName("quizcontrol")
                    .HasMaxLength(250);

                entity.Property(e => e.Quiztype).HasColumnName("quiztype");

                entity.Property(e => e.Tgtuser)
                    .IsRequired()
                    .HasColumnName("tgtuser")
                    .HasMaxLength(50);

                entity.Property(e => e.Validfrom)
                    .HasColumnName("validfrom")
                    .HasColumnType("date");

                entity.Property(e => e.Validto)
                    .HasColumnName("validto")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<Knowledge>(entity =>
            {
                entity.ToTable("knowledge");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Category).HasColumnName("CATEGORY");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("CONTENT");

                entity.Property(e => e.CanGenerate).HasColumnName("CAN_GENERATE");
            });

            modelBuilder.Entity<Permuser>(entity =>
            {
                entity.HasKey(e => new { e.Userid, e.Monitor });

                entity.ToTable("permuser");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(50);

                entity.Property(e => e.Monitor)
                    .HasColumnName("monitor")
                    .HasMaxLength(50);

                entity.HasOne(d => d.MonitorNavigation)
                    .WithMany(p => p.PermuserMonitorNavigation)
                    .HasForeignKey(d => d.Monitor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_permuser_monitor");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PermuserUser)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK_permuser_user");
            });

            modelBuilder.Entity<Qbklink>(entity =>
            {
                entity.HasKey(e => new { e.Qbid, e.Kwgid });

                entity.ToTable("qbklink");

                entity.Property(e => e.Qbid).HasColumnName("QBID");

                entity.Property(e => e.Kwgid).HasColumnName("KWGID");

                entity.HasOne(d => d.Kwg)
                    .WithMany(p => p.Qbklink)
                    .HasForeignKey(d => d.Kwgid)
                    .HasConstraintName("FK_qbklink_kwid");

                entity.HasOne(d => d.Qb)
                    .WithMany(p => p.Qbklink)
                    .HasForeignKey(d => d.Qbid)
                    .HasConstraintName("FK_qbklink_qbid");
            });

            modelBuilder.Entity<Questionbank>(entity =>
            {
                entity.ToTable("questionbank");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Category).HasColumnName("CATEGORY");

                entity.Property(e => e.BriefCont)
                    .IsRequired()
                    .HasColumnName("BRIEF_CONT")
                    .HasMaxLength(50);

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("CONTENT");

                entity.Property(e => e.Answer)
                    // .IsRequired()
                    .HasColumnName("ANSWER");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.ToTable("quiz");

                entity.Property(e => e.Quizid).HasColumnName("quizid");

                entity.Property(e => e.Attenduser)
                    .IsRequired()
                    .HasColumnName("attenduser")
                    .HasMaxLength(50);

                entity.Property(e => e.Basicinfo)
                    .HasColumnName("basicinfo")
                    .HasMaxLength(250);

                entity.Property(e => e.Quiztype).HasColumnName("quiztype");

                entity.Property(e => e.Submitdate)
                    .HasColumnName("submitdate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Quizfaillog>(entity =>
            {
                entity.HasKey(e => new { e.Quizid, e.Failidx });

                entity.ToTable("quizfaillog");

                entity.Property(e => e.Quizid).HasColumnName("quizid");

                entity.Property(e => e.Failidx).HasColumnName("failidx");

                entity.Property(e => e.Expected)
                    .IsRequired()
                    .HasColumnName("expected")
                    .HasMaxLength(250);

                entity.Property(e => e.Inputted)
                    .IsRequired()
                    .HasColumnName("inputted")
                    .HasMaxLength(250);

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.Quizfaillog)
                    .HasForeignKey(d => d.Quizid)
                    .HasConstraintName("FK_quizfaillog_quiz");
            });

            modelBuilder.Entity<Quizsection>(entity =>
            {
                entity.HasKey(e => new { e.Quizid, e.Section });

                entity.ToTable("quizsection");

                entity.Property(e => e.Quizid).HasColumnName("quizid");

                entity.Property(e => e.Section).HasColumnName("section");

                entity.Property(e => e.Faileditems).HasColumnName("faileditems");

                entity.Property(e => e.Timespent).HasColumnName("timespent");

                entity.Property(e => e.Totalitems).HasColumnName("totalitems");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.Quizsection)
                    .HasForeignKey(d => d.Quizid)
                    .HasConstraintName("FK_quizsection_quiz");
            });

            modelBuilder.Entity<Quizuser>(entity =>
            {
                entity.HasKey(e => e.Userid);

                entity.ToTable("quizuser");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Award)
                    .HasColumnName("award")
                    .HasMaxLength(5);

                entity.Property(e => e.Awardplan)
                    .HasColumnName("awardplan")
                    .HasMaxLength(5);

                entity.Property(e => e.Deletionflag).HasColumnName("deletionflag");

                entity.Property(e => e.Displayas)
                    .IsRequired()
                    .HasColumnName("displayas")
                    .HasMaxLength(50);

                entity.Property(e => e.Others)
                    .HasColumnName("others")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => new { e.Tag1, e.Reftype, e.Refid });

                entity.ToTable("tag");

                entity.Property(e => e.Tag1)
                    .HasColumnName("TAG")
                    .HasMaxLength(50);

                entity.Property(e => e.Reftype).HasColumnName("REFTYPE");

                entity.Property(e => e.Refid).HasColumnName("REFID");
            });

            modelBuilder.Entity<Useraward>(entity =>
            {
                entity.HasKey(e => e.Aid);

                entity.ToTable("useraward");

                entity.Property(e => e.Aid).HasColumnName("aid");

                entity.Property(e => e.Adate)
                    .HasColumnName("adate")
                    .HasColumnType("date");

                entity.Property(e => e.Award).HasColumnName("award");

                entity.Property(e => e.Planid).HasColumnName("planid");

                entity.Property(e => e.Publish).HasColumnName("publish");

                entity.Property(e => e.Qid).HasColumnName("qid");

                entity.Property(e => e.Used)
                    .HasColumnName("used")
                    .HasMaxLength(50);

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasColumnName("userid")
                    .HasMaxLength(50);
            });
        }
    }
}
