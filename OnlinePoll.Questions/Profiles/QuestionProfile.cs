using AutoMapper;
using OnlinePoll.Infrastructure.Models;
using OnlinePoll.Questions.Queries.GetQuestionQuery;

namespace OnlinePoll.Questions.Profiles;

public class QuestionProfile : Profile
{
    public QuestionProfile()
    {
        CreateMap<Answer, AnswerDto>();
        CreateMap<Question, GetQuestionQueryResult>()
            .ForMember(dest => dest.Answers,
                opt => opt.MapFrom(src => src.Answers));
    }
}