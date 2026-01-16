using Bogus;
using TodoList.Communication.Requests;

namespace CommonTestUtilities.Requests
{
    public class RequestRegisterTodoJsonBuilder
    {
        public static RequestRegisterTodoJson Build()
        {
            return new Faker<RequestRegisterTodoJson>()
                .RuleFor(x => x.Title, f => f.Lorem.Sentence(3, 3).TrimEnd('.'))
                .RuleFor(x => x.UserId, f => f.Random.Int(1,1000));
        }
    }
}
