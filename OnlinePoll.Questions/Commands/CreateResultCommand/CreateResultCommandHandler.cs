using Microsoft.EntityFrameworkCore;
using OnlinePoll.Application.Interfaces;
using OnlinePoll.Application.Services.ExceptionHandling;
using OnlinePoll.Infrastructure.Models;

namespace OnlinePoll.Questions.Commands.CreateResultCommand;

public class CreateResultCommandHandler : IAsyncCommandHandler<CreateResultCommand, CreateResultCommandResult>
{
    private readonly DbContext _dbContext;

    public CreateResultCommandHandler(DbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<CreateResultCommandResult> Handle(CreateResultCommand request, CancellationToken cancellationToken)
    {
        var question = await _dbContext
            .Set<Question>()
            .Include(x => x
                .Answers
                .Where(answer => request
                    .Answers
                    .Select(dto => dto.Id)
                    .Contains(answer.Id)))
            .FirstOrDefaultAsync(x => x.Id == request.QuestionId, cancellationToken);
        
        var interview = await _dbContext
            .Set<Interview>()
            .FirstOrDefaultAsync(x => x.Id == request.InterviewId, cancellationToken);
        
        if (question == null) 
            throw new NotFoundException("Вопрос не найден");
        
        if (question.Answers is null || !question.Answers.Any()) 
            throw new NotFoundException("Отсутствует ответ на вопрос");
        
        if (interview == null) 
            throw new NotFoundException("Данные о сессии не найдены");

        await CreateResultsAsync(cancellationToken, question, interview);

        var nextQuestion = await _dbContext.Set<Question>().FirstOrDefaultAsync(x => x.Id > question.Id, cancellationToken);
        
        var nextQuestionId = nextQuestion?.Id ?? question.Id;
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        var response = new CreateResultCommandResult () {NextQuestionId = nextQuestionId};
        
        return response;
    }

    private async Task CreateResultsAsync(CancellationToken cancellationToken, Question question, Interview interview)
    {
        foreach (var answer in question.Answers)
        {
            var result = new Result
            {
                AnswerId = answer.Id,
                QuestionId = question.Id,
                InterviewId = interview.Id,
            };
            await _dbContext.Set<Result>().AddAsync(result, cancellationToken);
        }
    }
}