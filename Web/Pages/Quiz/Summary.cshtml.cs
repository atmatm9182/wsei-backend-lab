using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BackendLab01.Pages;

public class Summary : PageModel
{
    [BindProperty]
    public int CorrectAnswersCount { get; set; }
    
    [BindProperty]
    public int TotalAnswersCount { get; set; }
    
    public void OnGet(int correct, int total)
    {
        CorrectAnswersCount = correct;
        TotalAnswersCount = total;
    }
}