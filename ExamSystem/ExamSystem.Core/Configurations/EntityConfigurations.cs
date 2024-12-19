using ExamSystem.Core.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ExamSystem.Core.Configurations
{
    internal class EntityConfigurations
    {
    }
    public class ExamEntityTypeConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder
                .HasKey(e => e.ExamId);

            builder
                .HasOne(e => e.Subject)
                .WithMany()
                .HasForeignKey(e => e.SubjectId)
                .OnDelete(DeleteBehavior.Restrict); //adjust the delete behavior

            builder
                .HasMany(e => e.ExamQuestions)
                .WithOne(eq => eq.Exam)
                .HasForeignKey(eq => eq.ExamId);
        }
    }

    public class ExamResultEntityTypeConfiguration : IEntityTypeConfiguration<ExamResult>
    {
        public void Configure(EntityTypeBuilder<ExamResult> builder)
        {
            builder
                .HasKey(er => er.ExamResultId);

            builder
                .HasOne(er => er.Student)
                .WithMany()
                .HasForeignKey(er => er.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(er => er.Exam)
                .WithMany()
                .HasForeignKey(er => er.ExamId);
        }
    }

    public class ExamQuestionEntityTypeConfiguration : IEntityTypeConfiguration<ExamQuestion>
    {
        public void Configure(EntityTypeBuilder<ExamQuestion> builder)
        {
            builder
                .HasKey(eq => eq.Id);

            builder
                .HasOne(eq => eq.Exam)
                .WithMany(e => e.ExamQuestions)
                .HasForeignKey(eq => eq.ExamId);

            builder
                .HasOne(eq => eq.Question)
                .WithMany(q => q.ExamQuestions)
                .HasForeignKey(eq => eq.QuestionId);
        }
    }

    public class QuestionEntityTypeConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder
                .HasKey(q => q.QuestionId);

            builder
                .HasMany(q => q.Answers)
                .WithOne();
                //.HasForeignKey(a => a.QuestionId);

            builder
                .HasMany(q => q.ExamQuestions)
                .WithOne(eq => eq.Question)
                .HasForeignKey(eq => eq.QuestionId);
        }
    }

    public class AnswerEntityTypeConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder
                .HasKey(a => a.AnswerId);

            builder
                .Property(a => a.Text)
                .IsRequired();

            builder
                .Property(a => a.IsCorrect)
                .IsRequired();
        }
    }

    public class SubjectEntityTypeConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder
                .HasKey(s => s.SubjectId);

            builder
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(s => s.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder
                .Property(s => s.QuestionsNumber)
                .IsRequired();

            builder
                .Property(s => s.Duration)
                .IsRequired();

            builder
                .Property(s => s.total)
                .IsRequired();
        }
    }

}
