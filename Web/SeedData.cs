using ApplicationCore.Interfaces.Repository;
using BackendLab01;

namespace Infrastructure.Memory;
public static class SeedData
{
    public static void Seed(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            var quizRepo = provider.GetService<IGenericRepository<Quiz, int>>();
            var quizItemRepo = provider.GetService<IGenericRepository<QuizItem, int>>();

            var quizItem11 = new QuizItem(1, "How many legs does the spider have",
                new List<string>() { "four", "six", "ten" }, "eight");
            var quizItem12 = new QuizItem(2, "What is the biggest mammal in the world",
                new List<string>() { "elephant", "hippo", "giraffe" }, "blue whale");
            var quizItem13 = new QuizItem(3, "What is the fastest animal in the world",
                new List<string>() { "puma", "leopard", "eagle" }, "falcon");

            var quiz1 = new Quiz(0, new List<QuizItem>() { quizItem11, quizItem12, quizItem13 }, "Animal quiz");
            quizItemRepo.Add(quizItem11);
            quizItemRepo.Add(quizItem12);
            quizItemRepo.Add(quizItem13);
            quizRepo.Add(quiz1);

            var quizItem21 = new QuizItem(1, "In what year was the computer invented",
                new List<string>() { "1930", "1839", "1873" }, "1837");
            var quizItem22 = new QuizItem(2, "What is the name of the first programmer",
                new List<string>() { "Charles Babbage", "William King", "Alan Turing" }, "Ada Lovelace");
            var quizItem23 = new QuizItem(3, "What programming language powers moder web pages",
                new List<string>() { "python", "web assembly", "java" }, "javascript");
            var quiz2 = new Quiz(0, new List<QuizItem>() { quizItem21, quizItem22, quizItem23 }, "Programming quiz");
            quizItemRepo.Add(quizItem21);
            quizItemRepo.Add(quizItem22);
            quizItemRepo.Add(quizItem23);
            quizRepo.Add(quiz2);
        }
    }
}