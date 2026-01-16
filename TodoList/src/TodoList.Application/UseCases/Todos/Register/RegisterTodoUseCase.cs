using AutoMapper;
using TodoList.Communication.Requests;
using TodoList.Communication.Responses;
using TodoList.Domain.Entities;
using TodoList.Domain.Repositories;
using TodoList.Exception.ExceptionBase;

namespace TodoList.Application.UseCases.Todos.Register
{
    public class RegisterTodoUseCase : IRegisterTodoUseCase
    {
        private readonly ITodoRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private const int MaxIncompleteTodos = 5;

        public RegisterTodoUseCase(
            ITodoRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseRegisterTodoJson> Execute(RequestRegisterTodoJson request)
        {
            Validate(request);

            var totalIncomplete = await _repository.CountIncompleteByUser(request.UserId);

            if(totalIncomplete >= MaxIncompleteTodos)
            {
                throw new BusinessLogicException("Limite de 5 tarefas incompletas atingido para este usuário.");
            }

            var todo = _mapper.Map<Todo>(request);
            await _repository.Add(todo);
            await _unitOfWork.Commit();

            return _mapper.Map<ResponseRegisterTodoJson>(todo);
        }

        private void Validate(RequestRegisterTodoJson request)
        {
            var validator = new TodoValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
