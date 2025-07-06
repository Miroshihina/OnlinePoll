using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using OnlinePoll.Application.Interfaces;
using OnlinePoll.Questions.Commands.CreateResultCommand;
using OnlinePoll.Questions.Queries.GetQuestionQuery;

namespace OnlinePoll.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly IAsyncCommandHandler<CreateResultCommand, CreateResultCommandResult> _createResultCommandHandler;
    private readonly IAsyncQueryHandler<GetQuestionQuery, GetQuestionQueryResult> _getQuestionQueryHandler;

    public QuestionsController(IAsyncCommandHandler<CreateResultCommand, CreateResultCommandResult> createResultCommandHandler, 
        IAsyncQueryHandler<GetQuestionQuery, GetQuestionQueryResult> getQuestionQueryHandler)
    {
        _createResultCommandHandler = createResultCommandHandler;
        _getQuestionQueryHandler = getQuestionQueryHandler;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuestion(int id, CancellationToken token)
    {
        var request = await _getQuestionQueryHandler.Handle(new GetQuestionQuery() {Id = id}, token);
        
        return Ok(request);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateResult(CreateResultCommand command, CancellationToken token)
    {
        var request = await _createResultCommandHandler.Handle(command, token);
        
        return Ok(request);
    }
}