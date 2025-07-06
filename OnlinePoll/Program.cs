using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using OnlinePoll.Application.Interfaces;
using OnlinePoll.Infrastructure.Data;
using OnlinePoll.Middleware;
using OnlinePoll.Questions.Commands.CreateResultCommand;
using OnlinePoll.Questions.Profiles;
using OnlinePoll.Questions.Queries.GetQuestionQuery;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<DbContext, AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddAutoMapper(typeof(QuestionProfile));

builder.Services.AddScoped<IAsyncCommandHandler<CreateResultCommand, CreateResultCommandResult>, CreateResultCommandHandler>();
builder.Services.AddScoped<IAsyncQueryHandler<GetQuestionQuery, GetQuestionQueryResult>, GetQuestionQueryHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();
app.Run();