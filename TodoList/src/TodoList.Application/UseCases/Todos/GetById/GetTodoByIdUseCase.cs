using AutoMapper;
using TodoList.Communication.Responses;
using TodoList.Domain.Repositories;
using TodoList.Exception.ExceptionBase;

namespace TodoList.Application.UseCases.Todos.GetById
{
    public class GetTodoByIdUseCase : IGetTodoByIdUseCase
    {
        private readonly ITodoRepository _repository;
        private readonly IMapper _mapper;

        public GetTodoByIdUseCase(ITodoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseTodoJson> Execute(int id)
        {
            var todo = await _repository.GetById(id);

            if (todo == null)
                throw new NotFoundException("Tarefa não encontrada");

            return _mapper.Map<ResponseTodoJson>(todo);
        }
    }
}
