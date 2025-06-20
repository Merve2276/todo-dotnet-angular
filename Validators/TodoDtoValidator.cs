using FluentValidation;
using todo_dotnet_api.DTOs;

namespace todo_dotnet_api.Validators
{
    public class TodoDtoValidator : AbstractValidator<TodoDto>
    {
        public TodoDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş olamaz.")
                .MaximumLength(100).WithMessage("Başlık 100 karakterden uzun olamaz.");
        }
    }
}
