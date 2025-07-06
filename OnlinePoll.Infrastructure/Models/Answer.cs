namespace OnlinePoll.Infrastructure.Models;

public class Answer
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string Text { get; set; }
    public Question Question { get; set; }
    public IEnumerable<Result> Results { get; set; }

    public Answer()
    {
        Results = new HashSet<Result>();
    }
}