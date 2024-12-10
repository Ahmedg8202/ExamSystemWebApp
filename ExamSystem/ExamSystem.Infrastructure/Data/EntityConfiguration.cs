using ExamSystem.Core.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Infrastructure.Data
{
    internal class EntityConfiguration
    {
        /*
        public class QuestionConfiguration : IEntityTypeConfiguration<Question>
        {
            public void Configure(EntityTypeBuilder<Question> builder)
            {
                //builder.ToTable("Questions");

                builder.HasKey(q => q.QuestionId);

                builder.Property(q => q.QuestionId)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(q => q.Text)
                    .IsRequired()
                    .HasMaxLength(500);

                builder.Property(q => q.Options)
                    .IsRequired()
                    .HasMaxLength(1000);

                builder.Property(q => q.CorrectAnswer)
                    .IsRequired()
                    .HasMaxLength(500);
            }
        }

        public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
        {
            public void Configure(EntityTypeBuilder<Subject> builder)
            {
                //builder.ToTable("Subjects");

                builder.HasKey(s => s.SubjectId);

                builder.Property(s => s.SubjectId)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                builder.Property(s => s.Description)
                    .HasMaxLength(1000);

                builder.Property(s => s.QuestionsNumber)
                    .IsRequired();

                builder.Property(s => s.Duration)
                    .IsRequired();

                builder.Property(s => s.total)
                    .IsRequired();
            }
        }

        public class ExamConfiguration : IEntityTypeConfiguration<Exam>
        {
            public void Configure(EntityTypeBuilder<Exam> builder)
            {
                //builder.ToTable("Exams");

                builder.HasKey(e => e.ExamId);

                builder.Property(e => e.ExamId)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(e => e.SubjectId)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.HasOne(e => e.Subject)
                    .WithMany()
                    .HasForeignKey(e => e.SubjectId);

                builder.HasMany(e => e.Questions)
                    .WithOne()
                    .HasForeignKey(q => q.QuestionId);
            }
        }

        public class ExamResultConfiguration : IEntityTypeConfiguration<ExamResult>
        {
            public void Configure(EntityTypeBuilder<ExamResult> builder)
            {
                //builder.ToTable("ExamResults");

                builder.HasKey(er => er.ExamResultId);

                builder.Property(er => er.ExamResultId)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(er => er.StudentId)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(er => er.ExamId)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(er => er.Score)
                    .IsRequired();

                builder.Property(er => er.Status)
                    .IsRequired();

                builder.HasOne<Exam>()
                    .WithMany()
                    .HasForeignKey(er => er.ExamId);
            }
        }*/
    }

}

