using FluentValidation;
using TodoList.Communication.Requests;

namespace TodoList.Application.UseCases.Todos
{
    public class TodoValidator : AbstractValidator<RequestRegisterTodoJson>
    {
        public TodoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Título é obrigatório");
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserId deve ser maior que 0");
        }
    }
}
