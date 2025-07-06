using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlinePoll.Infrastructure.Models;

namespace OnlinePoll.Infrastructure.Configurations;

public class SurveyConfiguration : IEntityTypeConfiguration<Survey>
{
    public void Configure(EntityTypeBuilder<Survey> builder)
    {
        builder.HasMany(s => s.Questions)
            .WithOne(q => q.Survey)
            .HasForeignKey(q => q.SurveyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.Interviews)
            .WithOne(i => i.Survey)
            .HasForeignKey(i => i.SurveyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}