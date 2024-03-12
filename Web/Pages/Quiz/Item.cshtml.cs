using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BackendLab01.Pages
{
    
    public class QuizModel : PageModel
    {
        private readonly IQuizUserService _userService;

        private readonly ILogger _logger;
        public QuizModel(IQuizUserService userService, ILogger<QuizModel> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [BindProperty]
        public string Question { get; set; }
        [BindProperty]
        public List<string> Answers { get; set; }
        
        [BindProperty]
        public String UserAnswer { get; set; }
        
        [BindProperty]
        public int QuizId { get; set; }
        
        [BindProperty]
        public int ItemId { get; set; }
        
        public void OnGet(int quizId, int itemId)
        {
            QuizId = quizId;
            ItemId = itemId;
            var quiz = _userService.FindQuizById(quizId);
            var quizItem = quiz?.Items[itemId - 1];
            Question = quizItem?.Question;
            Answers = new List<string>();
            if (quizItem is not null)
            {
                Answers.AddRange(quizItem?.IncorrectAnswers);
                Answers.Add(quizItem?.CorrectAnswer);
            }
        }

        public IActionResult OnPost()
        {
            var quiz = _userService.FindQuizById(QuizId);
            
            var item = quiz.Items[ItemId - 1];
            Console.WriteLine("{0} {1}", item.CorrectAnswer, UserAnswer);
            _userService.SaveUserAnswerForQuiz(QuizId,  0, ItemId, UserAnswer);

            if (quiz.Items.Count == ItemId)
            {
                var answers = _userService.GetUserAnswersForQuiz(QuizId, 0);
                var correctAnswers = answers.Where(answer => answer.IsCorrect());
                var result =  RedirectToPage("Summary", new { correct = correctAnswers.Count(), total = quiz.Items.Count });
                return result;
            }

            return RedirectToPage("Item", new {quizId = QuizId, itemId = ItemId + 1});
        }
    }
}
