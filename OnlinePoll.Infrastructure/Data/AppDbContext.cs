using Microsoft.EntityFrameworkCore;
using OnlinePoll.Infrastructure.Configurations;
using OnlinePoll.Infrastructure.Models;

namespace OnlinePoll.Infrastructure.Data;

public class AppDbContext : DbContext
{
    DbSet<Answer> Answers { get; set; }
    DbSet<Interview> Interviews { get; set; }
    DbSet<Question> Questions { get; set; }
    DbSet<Result> Results { get; set; }
    DbSet<Survey> Surveys { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new SurveyConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionConfiguration());
        modelBuilder.ApplyConfiguration(new AnswerConfiguration());
        modelBuilder.ApplyConfiguration(new InterviewConfiguration());
        modelBuilder.ApplyConfiguration(new ResultConfiguration());
    }
}