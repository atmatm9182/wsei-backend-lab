using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;

namespace WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("/api/v1/quzzes/admin")]
public class AdminQuizController : ControllerBase
{
    private readonly IQuizAdminService _service;
    private readonly IValidator<QuizItem> _validator;

    public AdminQuizController(IQuizAdminService service, IValidator<QuizItem> validator)
    {
        _service = service;
        _validator = validator;
    }

    [HttpPost]
    public IActionResult AddQuiz(LinkGenerator linkGenerator, [FromBody] NewQuizDto quizDto)
    {
        var quiz = _service.AddQuiz(quizDto.Title, new List<QuizItem>());
        return Created(
            linkGenerator.GetPathByAction(
                HttpContext,
                nameof(GetQuiz),
                null,
                new { quizId = quiz.Id }
            )!,
            quiz
        );
    }

    [HttpPatch]
    [Route("{quizId}")]
    [Consumes("application/json-patch+json")]
    public ActionResult<Quiz> AddQuizItem(int quizId, JsonPatchDocument<Quiz>? patchDoc)
    {
        var quiz = _service.FindAllQuizzes().FirstOrDefault(q => q.Id == quizId);
        if (quiz is null || patchDoc is null)
        {
            return NotFound(new { error = $"Quiz width id {quizId} not found" });
        }

        var disablesOperation = patchDoc.Operations.FirstOrDefault(p =>
            p.OperationType == OperationType.Replace && p.path == "id"
        );

        if (disablesOperation is not null)
        {
            return BadRequest(new { error = "Can not replace id!" });
        }

        int previousCount = quiz.Items.Count;
        patchDoc.ApplyTo(quiz, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (previousCount < quiz.Items.Count)
        {
            QuizItem item = quiz.Items[^1];

            var validationResult = _validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            quiz.Items.RemoveAt(quiz.Items.Count - 1);
            _service.AddQuizItemToQuiz(quizId, item);
        }
        return Ok(_service.FindAllQuizzes().FirstOrDefault(q => q.Id == quizId));
    }

    [HttpDelete]
    [Route("{quizId}")]
    public void DeleteQuiz(int quizId)
    {
        _service.DeleteQuiz(quizId);
    }

    [HttpDelete]
    [Route("items/{itemId}")]
    public void DeleteQuizItem(int itemId)
    {
        _service.DeleteQuizItem(itemId);
    }

    [HttpGet]
    [Route("{quizId}")]
    public ActionResult<Quiz> GetQuiz(int quizId)
    {
        var quiz = _service.FindAllQuizzes().Find(q => q.Id == quizId);
        return quiz is not null ? quiz : NotFound();
    }

    [HttpGet]
    [Route("items/{itemId}")]
    public ActionResult<QuizItem> GetQuizItem(int itemId)
    {
        var item = _service.FindAllQuizItems().Find(i => i.Id == itemId);
        return item is not null ? item : NotFound();
    }
}
