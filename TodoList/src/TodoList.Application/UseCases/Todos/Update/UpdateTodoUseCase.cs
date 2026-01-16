using TodoList.Communication.Requests;
using TodoList.Domain.Repositories;
using TodoList.Exception.ExceptionBase;

namespace TodoList.Application.UseCases.Todos.Update
{
    public class UpdateTodoUseCase : IUpdateTodoUseCase
    {
        private readonly ITodoRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private const int MaxIncompleteTodos = 5;

        public UpdateTodoUseCase(ITodoRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(int id, RequestUpdateTodoJson request)
        {
            var todo = await _repository.GetById(id);

            if (todo == null)
                throw new NotFoundException("Tarefa não encontrada");

            if (todo.Completed && !request.Completed)
            {
                var incompleteCount = await _repository.CountIncompleteByUser(todo.UserId);

                if (incompleteCount >= MaxIncompleteTodos)
                    throw new BusinessLogicException("Limite de 5 tarefas incompletas atingido para este usuário.");
            }

            todo.Completed = request.Completed;
            await _repository.Update(todo);
            await _unitOfWork.Commit();
        }
    }
}
