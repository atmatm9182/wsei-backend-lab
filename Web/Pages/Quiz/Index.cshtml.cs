using ApplicationCore.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BackendLab01.Pages.Quiz;

public class IndexModel : PageModel
{
    private readonly IGenericRepository<ApplicationCore.Models.Quiz, int> _quizRepo;

    public IndexModel(IGenericRepository<ApplicationCore.Models.Quiz, int> quizRepo)
    {
        _quizRepo = quizRepo;
    }

    public List<ApplicationCore.Models.Quiz> Quizzes { get; set; } = new();

    public void OnGet()
    {
        Quizzes = _quizRepo.FindAll();
    }
}
