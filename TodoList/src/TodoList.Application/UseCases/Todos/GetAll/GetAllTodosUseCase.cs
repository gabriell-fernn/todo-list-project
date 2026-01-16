using AutoMapper;
using TodoList.Communication.Requests;
using TodoList.Communication.Responses;
using TodoList.Domain.Repositories;
using TodoList.Exception.ExceptionBase;

namespace TodoList.Application.UseCases.Todos.GetAll
{
    public class GetAllTodosUseCase : IGetAllTodosUseCase
    {
        private readonly ITodoRepository _repository;
        private readonly IMapper _mapper;

        public GetAllTodosUseCase(ITodoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseGetTodosJson> Execute(RequestGetTodosJson request)
        {
            Validate(request);

            var todos = await _repository.GetFiltered(
                request.Title,
                request.Sort,
                request.Order,
                request.Page,
                request.PageSize
            );

            var totalCount = await _repository.GetCountByFilter(request.Title);

            var responseTodos = _mapper.Map<List<ResponseTodoJson>>(todos);

            return new ResponseGetTodosJson
            {
                Data = responseTodos,
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize
            };
        }

        private void Validate(RequestGetTodosJson request)
        {
            if (request.Page < 1)
                throw new ErrorOnValidationException(new List<string> { "Página deve ser maior que 0" });

            if (request.PageSize < 1 || request.PageSize > 100)
                throw new ErrorOnValidationException(new List<string> { "PageSize deve estar entre 1 e 100" });
        }
    }
}
