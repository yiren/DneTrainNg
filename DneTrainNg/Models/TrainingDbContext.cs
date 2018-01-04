using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DneTrainNg.Models
{
    public class TrainingDbContext : DbContext
    {
        public TrainingDbContext(DbContextOptions<TrainingDbContext> options)
            :base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Section> Sections { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfiguration(new CourseEntityConfiguration());
            builder.ApplyConfiguration(new StudentEntityConfiguration());
            builder.ApplyConfiguration(new SectionEntityConfiguration());
            builder.ApplyConfiguration(new StudentCourseEntityConfiguration());

        }

    }

    class SectionEntityConfiguration : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder
                .HasKey(t => t.SectionId);

            builder
                .Property(t => t.SectionId).ValueGeneratedOnAdd() ;

            builder
                   .Property(t => t.SectionId)
                   .ValueGeneratedOnAdd();

            builder
                   .Property(t => t.SectionName)
                   .HasMaxLength(20)
                   .IsRequired();

            builder
                   .Property(t => t.SectionCode)
                   .HasMaxLength(10)
                   .IsRequired();

            //builder.HasMany(t => t.Students)
            //       .WithOne(s => s.Section)
            //       .HasForeignKey(t => t.StudentId);
                   
        }
    }

    class StudentCourseEntityConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder
                .HasKey(sc => sc.StudentCourseId);

            builder
                .Property(sc => sc.StudentCourseId).ValueGeneratedOnAdd();

            builder
                .Property(sc => sc.Score).HasDefaultValue("N/A");
        }
    }

    class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.CourseId);

            //builder.Property(c => c.CourseId).ValueGeneratedOnAdd();

            builder
                .Property(c => c.CourseName)
                .IsRequired();

            builder
                .Property(c => c.CourseStartDate)
                .HasMaxLength(50);

            builder
                .Property(c => c.CourseEndDate)
                .HasMaxLength(50);

            builder
                .Property(c => c.TrainHours);

            builder
                .HasMany(c => c.StudentCourses)
                .WithOne(c => c.Course)
                .HasForeignKey(c => c.CourseId);
        }
    }

    class StudentEntityConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder
                .HasKey(s => s.StudentId);

            builder.Property(s => s.StudentId).ValueGeneratedOnAdd();

            builder
                .Property(s => s.StudentName)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .HasMany(s => s.StudentCourses)
                .WithOne(s => s.Student)
                .HasForeignKey(s => s.StudentId);
            builder
                   .HasOne(s => s.Section)
                   .WithMany(t => t.Students)
                   .HasForeignKey(t => t.StudentId);
        }
    }
}
