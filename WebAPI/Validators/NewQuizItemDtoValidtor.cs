using FluentValidation;
using WebAPI.Dto;

namespace WebAPI.Validators;

public class NewQuizItemDtoValidator : AbstractValidator<NewQuizItemDto>
{
    public NewQuizItemDtoValidator()
    {
        RuleFor(dto => dto.CorrectOptionsIndex).Must((dto, idx, ctx) => idx < dto.Options.Count());
    }
}
