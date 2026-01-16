using Microsoft.Extensions.DependencyInjection;
using TodoList.Application.AutoMapper;
using TodoList.Application.UseCases.Todos.GetAll;
using TodoList.Application.UseCases.Todos.GetById;
using TodoList.Application.UseCases.Todos.Register;
using TodoList.Application.UseCases.Todos.Sync;
using TodoList.Application.UseCases.Todos.Update;

namespace TodoList.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection service)
        {
            AddAutoMapper(service);
            AddUseCase(service);
        }

        private static void AddAutoMapper(IServiceCollection service)
        {
            service.AddAutoMapper(typeof(AutoMapping));
        }

        private static void AddUseCase(IServiceCollection service)
        {
            service.AddScoped<IRegisterTodoUseCase, RegisterTodoUseCase>();
            service.AddScoped<IUpdateTodoUseCase, UpdateTodoUseCase>();
            service.AddScoped<IGetAllTodosUseCase, GetAllTodosUseCase>();
            service.AddScoped<IGetTodoByIdUseCase, GetTodoByIdUseCase>();
            service.AddScoped<ISyncTodosUseCase, SyncTodosUseCase>();
        }
    }
}
