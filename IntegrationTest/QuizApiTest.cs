using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTest;

public class QuizApiTest
{
    [Fact]
    public async void GetShouldReturnQuiz()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();
        
        
    }
}