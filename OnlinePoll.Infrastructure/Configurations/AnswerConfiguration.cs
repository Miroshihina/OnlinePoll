using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlinePoll.Infrastructure.Models;

namespace OnlinePoll.Infrastructure.Configurations;

public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.HasMany(a => a.Results)
            .WithOne(r => r.Answer)
            .HasForeignKey(r => r.AnswerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}