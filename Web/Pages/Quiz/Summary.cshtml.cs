using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

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

