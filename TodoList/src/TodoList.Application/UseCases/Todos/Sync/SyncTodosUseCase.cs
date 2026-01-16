using AutoMapper;
using TodoList.Application.Interfaces;
using TodoList.Communication.Requests;
using TodoList.Communication.Responses;
using TodoList.Domain.Entities;
using TodoList.Domain.Repositories;
using TodoList.Exception.ExceptionBase;

namespace TodoList.Application.UseCases.Todos.Sync
{
    public class SyncTodosUseCase : ISyncTodosUseCase
    {
        private readonly IExternalTodoService _externalService;
        private readonly ITodoRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SyncTodosUseCase(
            IExternalTodoService externalService,
            ITodoRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _externalService = externalService;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseSyncTodosJson> Execute(RequestSyncTodosJson request)
        {
            try
            {
                var externalTodos = await _externalService.FetchTodosFromExternalApi();

                int syncedCount = 0;
                int skippedCount = 0;

                foreach (var externalTodo in externalTodos)
                {
                    bool exists = await _repository.ExistsById(externalTodo.Id);

                    if (!exists)
                    {
                        var todo = _mapper.Map<Todo>(externalTodo);

                        await _repository.Add(todo);
                        syncedCount++;
                    }
                    else
                    {
                        skippedCount++;
                    }
                }

                await _unitOfWork.Commit();

                return new ResponseSyncTodosJson
                {
                    Message = $"Sincronização concluída com sucesso",
                    SyncedCount = syncedCount,
                    SkippedCount = skippedCount
                };
            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
            catch (BusinessLogicException ex)
            {
                throw new BusinessLogicException($"Erro ao sincronizar tarefas: {ex.Message}");
            }
        }
    }
}
