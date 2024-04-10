using ApplicationCore.Models;
using Infrastructure.Entities;

namespace Infrastructure.Mappers;

public class QuizItemMapper
{
    public static QuizItem FromEntityToQuizItem(QuizItemEntity entity)
    {
        return new QuizItem(
            entity.Id,
            entity.Question,
            entity.IncorrectAnswers.Select(e => e.Answer).ToList(),
            entity.CorrectAnswer);
    }

    public static QuizItemEntity FromQuizItemToEntity(QuizItem item)
    {
        return new QuizItemEntity()
        {
            Id = item.Id,
            Question = item.Question,
            CorrectAnswer = item.CorrectAnswer,
        };
    }
}