using Microsoft.EntityFrameworkCore;
using Mooditor.Api.Models;

namespace Mooditor.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<MoodEntry> MoodEntries { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(u => u.Id);

                entity.Property(u => u.Username)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.Email)
                      .HasMaxLength(255);

                entity.HasIndex(u => u.Username);
            });

            modelBuilder.Entity<MoodEntry>(entity =>
            {
                entity.ToTable("MoodEntries");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Score)
                      .IsRequired();

                entity.Property(e => e.Note)
                      .HasMaxLength(1000);

                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("GETUTCDATE()")
                      .ValueGeneratedOnAdd();

                entity.HasIndex(e => e.UserId);

                entity.HasOne(e => e.User)
                      .WithMany(u => u.MoodEntries)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
