namespace OnlinePoll.Questions.Commands.CreateResultCommand;

public class CreateResultCommand
{
    public int QuestionId { get; set; }
    public int InterviewId { get; set; }
    public IEnumerable<AnswerDto> Answers { get; set; }
}