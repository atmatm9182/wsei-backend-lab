using System.Net.Http.Json;
using Infrastructure;
using Infrastructure.Entities;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Controllers;

namespace IntegrationTest;

public class QuizTestUserApi : IClassFixture<QuizAppTestFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly QuizAppTestFactory<Program> _app;
    private readonly QuizDbContext _dbContext;

    public QuizTestUserApi(HttpClient httpClient, QuizAppTestFactory<Program> app)
    {
        _httpClient = httpClient;
        _app = app;
        using (var scope = _app.Services.CreateScope())
        {
            _dbContext = scope.ServiceProvider.GetService<QuizDbContext>()!;
            var items = new List<QuizItemEntity>()
            {
                new()
                {
                    Id = 1,
                    Question = "21 + 37",
                    CorrectAnswer = "58",
                    IncorrectAnswers = "1 2 3 4",
                }
            };

            if (!_dbContext.Quizzes.Any())
            {
                var quiz = new QuizEntity()
                {
                    Id = 100,
                    Title = "Matematyka",
                    Items = items.ToHashSet()
                };

                _dbContext.Quizzes.Add(quiz);
                _dbContext.SaveChanges();

            }
        }
    }

    [Fact]
    public void GetShouldReturnQuizWithId100()
    {
        var result = _httpClient.GetFromJsonAsync<QuizDto>("/api/v1/quizzes/100");
        Assert.NotNull(result);
        Assert.Equal(2, result.Result.Items.Count);
    }
}