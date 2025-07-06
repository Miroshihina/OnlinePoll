namespace OnlinePoll.Infrastructure.Models;

public class Interview
{
    public int Id { get; set; }
    public int SurveyId { get; set; }
    public string IntervieweeName { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime FinishedAt { get; set; }
    public Survey Survey { get; set; }
    public IEnumerable<Result> Results { get; set; }

    public Interview()
    {
        Results = new HashSet<Result>();
    }
}