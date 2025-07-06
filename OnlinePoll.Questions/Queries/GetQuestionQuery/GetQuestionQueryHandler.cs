using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlinePoll.Application.Interfaces;
using OnlinePoll.Application.Services.ExceptionHandling;
using Question = OnlinePoll.Infrastructure.Models.Question;

namespace OnlinePoll.Questions.Queries.GetQuestionQuery;

public class GetQuestionQueryHandler : IAsyncQueryHandler<GetQuestionQuery, GetQuestionQueryResult>
{
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;

    public GetQuestionQueryHandler(DbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<GetQuestionQueryResult> Handle(GetQuestionQuery query, CancellationToken cancellationToken)
    {
        var question = await _dbContext
            .Set<Question>()
            .AsNoTracking()
            .Include(x => x.Answers)
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);
        
        if (question == null) 
            throw new NotFoundException("Вопрос не найден");
        
        if (question.Answers is null || !question.Answers.Any()) 
            throw new NotFoundException("Отсутствует ответ на вопрос");
        
        var response = _mapper.Map<GetQuestionQueryResult>(question);
        
        return response;
    }
}