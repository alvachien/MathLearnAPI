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

        public virtual DbSet<Knowledge> Knowledge { get; set; }
        public virtual DbSet<Qbklink> Qbklink { get; set; }
        public virtual DbSet<Questionbank> Questionbank { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Knowledge>(entity =>
            {
                entity.ToTable("knowledge");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("CONTENT");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);
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
                    .HasConstraintName("FK_QstnBkKwdg_KWID");

                entity.HasOne(d => d.Qb)
                    .WithMany(p => p.Qbklink)
                    .HasForeignKey(d => d.Qbid)
                    .HasConstraintName("FK_QstnBkKwdg_QBID");
            });

            modelBuilder.Entity<Questionbank>(entity =>
            {
                entity.ToTable("questionbank");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Attachment1).HasColumnName("ATTACHMENT1");

                entity.Property(e => e.Attachment2).HasColumnName("ATTACHMENT2");

                entity.Property(e => e.Attachment3).HasColumnName("ATTACHMENT3");

                entity.Property(e => e.Attachment4).HasColumnName("ATTACHMENT4");

                entity.Property(e => e.Attachment5).HasColumnName("ATTACHMENT5");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("CONTENT");
            });
        }
    }
}
