using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<RequestInformation> RequestsInfo { get; set; }
        public DbSet<StudentRequest> StudentRequests { get; set; }

        /// <summary>
        /// Явно настраиваем EF Core-модель, чтобы БД отражала доменные требования.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureRequestInformation(modelBuilder.Entity<RequestInformation>());
            ConfigureStudentRequest(modelBuilder.Entity<StudentRequest>());
        }

        private static void ConfigureRequestInformation(EntityTypeBuilder<RequestInformation> entity)
        {
            entity.ToTable("RequestInformation");

            entity.HasKey(r => r.Id);

            entity.Property(r => r.Name)
                  .HasMaxLength(256)
                  .IsRequired();

            entity.Property(r => r.FilePath)
                  .HasMaxLength(512)
                  .IsRequired();

            entity.Property(r => r.FullRequestStatus)
                  .HasConversion<string>()
                  .HasMaxLength(32)
                  .IsRequired();

            entity.Property(r => r.Date)
                  .IsRequired();

            entity.Property(r => r.receivingFormat)
                  .HasConversion<string>()
                  .HasMaxLength(32)
                  .IsRequired();
        }

        private static void ConfigureStudentRequest(EntityTypeBuilder<StudentRequest> entity)
        {
            entity.ToTable("StudentRequests");

            entity.HasKey(sr => new { sr.StudentId, sr.RequestId });

            entity.Property(sr => sr.StudentId)
                  .IsRequired();

            entity.Property(sr => sr.RequestId)
                  .IsRequired();

            entity.HasOne<RequestInformation>()
                  .WithMany()
                  .HasForeignKey(sr => sr.RequestId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
