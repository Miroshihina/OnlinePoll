namespace OnlinePoll.Questions.Queries.GetQuestionQuery;

public class GetQuestionQueryResult
{
    public string Text { get; set; }
    public IEnumerable<AnswerDto> Answers { get; set; }
}