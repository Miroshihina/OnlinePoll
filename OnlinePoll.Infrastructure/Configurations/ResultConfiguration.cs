using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlinePoll.Infrastructure.Models;

namespace OnlinePoll.Infrastructure.Configurations;

public class ResultConfiguration : IEntityTypeConfiguration<Result>
{
    public void Configure(EntityTypeBuilder<Result> builder)
    {
        builder.HasOne(r => r.Answer)
            .WithMany(a => a.Results)
            .HasForeignKey(r => r.AnswerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Question)
            .WithMany(q => q.Results)
            .HasForeignKey(r => r.QuestionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Interview)
            .WithMany(i => i.Results)
            .HasForeignKey(r => r.InterviewId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}