using ApplicationCore.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BackendLab01.Pages.Quiz;

public class IndexModel : PageModel
{
    private readonly IGenericRepository<BackendLab01.Quiz, int> _quizRepo;

    public IndexModel(IGenericRepository<BackendLab01.Quiz, int> quizRepo)
    {
        _quizRepo = quizRepo;
    }

    public List<BackendLab01.Quiz> Quizzes { get; set; } = new();

    public void OnGet()
    {
        Quizzes = _quizRepo.FindAll();
    }
}
