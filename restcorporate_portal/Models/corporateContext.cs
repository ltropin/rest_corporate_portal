using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace restcorporate_portal.Models
{
    public partial class corporateContext : DbContext
    {
        public corporateContext()
        {
        }

        public corporateContext(DbContextOptions<corporateContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Badge> Badges { get; set; }
        public virtual DbSet<BadgesWorker> BadgesWorkers { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Difficulty> Difficulties { get; set; }
        public virtual DbSet<FavoriteProductsWorker> FavoriteProductsWorkers { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<PreviousProductsWorker> PreviousProductsWorkers { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Speciality> Specialities { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Worker> Workers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Encrypt=False;Integrated Security=False;User ID=sa;Password=WDbzs1t6xw42;Database=corporate");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BadgesWorker>(entity =>
            {
                entity.HasKey(e => new { e.BadgeId, e.WorkerId });

                entity.HasOne(d => d.Badge)
                    .WithMany(p => p.BadgesWorkers)
                    .HasForeignKey(d => d.BadgeId)
                    .HasConstraintName("FK_BadgesWorkers_Badge_ID");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.BadgesWorkers)
                    .HasForeignKey(d => d.WorkerId)
                    .HasConstraintName("FK_BadgesWorkers_Worker_ID");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Time).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_Comment_Task_ID");
            });

            modelBuilder.Entity<FavoriteProductsWorker>(entity =>
            {
                entity.HasOne(d => d.FavoriteProduct)
                    .WithMany(p => p.FavoriteProductsWorkers)
                    .HasForeignKey(d => d.FavoriteProductId)
                    .HasConstraintName("FK_FavoriteProductsWorkers_Product_ID");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.FavoriteProductsWorkers)
                    .HasForeignKey(d => d.WorkerId)
                    .HasConstraintName("FK_FavoriteProductsWorkers_Worker_ID");
            });

            modelBuilder.Entity<PreviousProductsWorker>(entity =>
            {
                entity.HasOne(d => d.PreviousProduct)
                    .WithMany(p => p.PreviousProductsWorkers)
                    .HasForeignKey(d => d.PreviousProductId)
                    .HasConstraintName("FK_PreviousProductsWorkers_Product_ID");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.PreviousProductsWorkers)
                    .HasForeignKey(d => d.WorkerId)
                    .HasConstraintName("FK_PreviousProductsWorkers_Worker_ID");
            });

            modelBuilder.Entity<Speciality>(entity =>
            {
                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Specialities)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Speciality_Department_ID");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasOne(d => d.Difficulty)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.DifficultyId)
                    .HasConstraintName("FK_Task_Difficulty_ID");

                entity.HasOne(d => d.Priorirty)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.PriorirtyId)
                    .HasConstraintName("FK_Task_Priority_ID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Task_Status_ID");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.WorkerId)
                    .HasConstraintName("FK_Task_Worker_ID");
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Workers)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Worker_Project_ID");

                entity.HasOne(d => d.Speciality)
                    .WithMany(p => p.Workers)
                    .HasForeignKey(d => d.SpecialityId)
                    .HasConstraintName("FK_Worker_Speciality_ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
