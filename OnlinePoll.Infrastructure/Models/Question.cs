namespace OnlinePoll.Infrastructure.Models;

public class Question
{
    public int Id { get; set; }
    public int SurveyId { get; set; }
    public string Text { get; set; }
    public Survey? Survey { get; set; }
    public IEnumerable<Answer> Answers { get; set; }
    public IEnumerable<Result> Results { get; set; }

    public Question()
    {
        Answers = new HashSet<Answer>();
        Results = new HashSet<Result>();
    }
}