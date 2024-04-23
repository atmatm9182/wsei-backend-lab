using ApplicationCore.Interfaces.Repository;
using ApplicationCore.Models;

namespace Web;
public static class SeedData
{
    public static void Seed(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            var quizRepo = provider.GetService<IGenericRepository<Quiz, int>>();
            var quizItemRepo = provider.GetService<IGenericRepository<QuizItem, int>>();
            List<QuizItem> quizItems1 = new List<QuizItem>();
            quizItems1.Add(quizItemRepo.Add(new QuizItem(id: 1, correctAnswer: "5", question: "3 + 2",
                incorrectAnswers: new List<string>() {"2", "3", "4"})));
            quizItems1.Add(quizItemRepo.Add(new QuizItem(id: 2, correctAnswer: "6", question: "3 * 2",
                incorrectAnswers: new List<string>() {"2", "3", "7"})));
            quizItems1.Add(quizItemRepo.Add(new QuizItem(id: 3, correctAnswer: "1", question: "3 - 2",
                incorrectAnswers: new List<string>() {"2", "3", "6"})));

            quizRepo.Add(new Quiz(id: 1, items: quizItems1, title: "Matematyka 1"));

            var quizItems2 = new List<QuizItem>();
            quizItems2.Add(quizItemRepo.Add(new QuizItem(id: 4, correctAnswer: "5", question: "7 - 2",
                incorrectAnswers: new List<string>() {"2", "3", "6"})));
            quizItems2.Add(quizItemRepo.Add(new QuizItem(id: 5, correctAnswer: "8", question: "2 * 4",
                incorrectAnswers: new List<string>() {"2", "3", "6"})));
            quizItems2.Add(quizItemRepo.Add(new QuizItem(id: 6, correctAnswer: "10", question: "8 + 2",
                incorrectAnswers: new List<string>() {"2", "3", "6"})));
            quizRepo.Add(new Quiz(id: 2, items: quizItems2, title: "Matematyka 2"));
        }
    }
}
