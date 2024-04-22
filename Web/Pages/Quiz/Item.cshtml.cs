using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
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
        [BindProperty]
        public int? NextItemIndex { get; set; }
        
        public IActionResult OnGet(int quizId, int itemIndex)
        {
            QuizId = quizId;
            var quiz = _userService.FindQuizById(quizId);
            if (quiz is null)
            {
                return BadRequest();
            }
            var items = quiz.Items;
            if (items.Count <= itemIndex)
            {
                return NotFound();
            }
            var quizItem = quiz.Items[itemIndex];
            ItemId = quizItem.Id;
            NextItemIndex = items.Count > itemIndex + 1 ? itemIndex + 1 : null; 
            Question = quizItem.Question;
            Answers = new List<string>() {quizItem.CorrectAnswer};
            Answers.AddRange(quizItem?.IncorrectAnswers);
            return Page();
        }

        public IActionResult OnPost()
        {
            var quiz = _userService.FindQuizById(QuizId);
            _userService.SaveUserAnswerForQuiz(QuizId,  0, ItemId, UserAnswer);

            if (quiz.Items.Count == ItemId)
            {
                var correctAnswers = _userService.CountCorrectAnswersForQuizFilledByUser(QuizId, 0);
                var result = RedirectToPage("Summary", new { correct = correctAnswers, total = quiz.Items.Count });
                return result;
            }

            return RedirectToPage("Item", new {quizId = QuizId, itemId = ItemId + 1});
        }
    }
}
