using ApplicationCore.Models;
using Infrastructure.Entities;

namespace Infrastructure.Mappers;

public static class QuizItemUserAnswerMapper
{
    public static QuizItemUserAnswer FromEntity(QuizItemUserAnswerEntity entity)
    {
        return new QuizItemUserAnswer
        {
            QuizId = entity.QuizId,
            QuizItem = QuizItemMapper.FromEntityToQuizItem(entity.QuizItem),
            UserId = entity.UserId,
            Answer = entity.UserAnswer,
        };
    }

    public static QuizItemUserAnswerEntity FromQuizItemUserAnswer(QuizItemUserAnswer answer)
    {
        return new QuizItemUserAnswerEntity
        {
            QuizId = answer.QuizId,
            QuizItem = QuizItemMapper.FromQuizItemToEntity(answer.QuizItem),
            UserAnswer = answer.Answer,
            QuizItemId = answer.QuizItem.Id,
            UserId = answer.UserId,
        };
    }
}