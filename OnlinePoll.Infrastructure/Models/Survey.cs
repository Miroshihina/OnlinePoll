namespace OnlinePoll.Infrastructure.Models;

public class Survey
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<Question> Questions { get; set; }
    public IEnumerable<Interview> Interviews { get; set; }

    public Survey()
    {
        Questions = new HashSet<Question>();
        Interviews = new HashSet<Interview>();
    }
}