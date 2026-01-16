using AutoMapper;
using TodoList.Communication.ExternalApis;
using TodoList.Communication.Requests;
using TodoList.Communication.Responses;
using TodoList.Domain.Entities;

namespace TodoList.Application.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<RequestRegisterTodoJson, Todo>();
            CreateMap<Todo, ResponseRegisterTodoJson>();
            CreateMap<Todo, ResponseTodoJson>();
            CreateMap<TodoExternalJson, Todo>();
        }
    }
}
