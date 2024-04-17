using ApplicationCore.Models;
using Infrastructure.Entities;

namespace Infrastructure.Mappers;

public static class QuizMapper
{
    public static QuizEntity FromQuizToEntity(Quiz quiz)
    {
        return new QuizEntity()
        {
            Id = quiz.Id,
            Items = quiz.Items.Select(QuizItemMapper.FromQuizItemToEntity).ToHashSet(),
            Title = quiz.Title
        };
    }
    
    public static Quiz FromEntityToQuiz(QuizEntity quizEntity)
    {
        return new Quiz(quizEntity.Id, quizEntity.Items.Select(QuizItemMapper.FromEntityToQuizItem).ToList(),
            quizEntity.Title);
    } 
}