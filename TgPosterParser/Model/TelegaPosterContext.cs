using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TgPosterParser.DB
{
    public  partial class TelegaPosterContext : DbContext
    {
        public TelegaPosterContext()
        {
        }

        public TelegaPosterContext(DbContextOptions<TelegaPosterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accaunt> Accaunts { get; set; }
        public virtual DbSet<Channel> Channels { get; set; }
        public virtual DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TelegaPoster;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accaunt>(entity =>
            {
                entity.Property(e => e.ClientHash)
                    .HasMaxLength(100)
                    .IsFixedLength(true);

                entity.Property(e => e.ClientId)
                    .HasMaxLength(100)
                    .IsFixedLength(true);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Channel>(entity =>
            {
                entity.Property(e => e.ChannelsId).HasColumnName("ChannelsID");

                entity.Property(e => e.Folder)
                    .HasMaxLength(300)
                    .IsFixedLength(true);

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsFixedLength(true);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Channels)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Channels_Accaunts");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.ForwardFrom).HasColumnName("Forward_from");

                entity.Property(e => e.Text).HasColumnType("ntext");

                entity.HasOne(d => d.Channel)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ChannelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Messages_Channels");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
