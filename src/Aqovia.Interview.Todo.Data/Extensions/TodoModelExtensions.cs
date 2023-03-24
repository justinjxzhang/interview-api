using Aqovia.Interview.Todo.Data.DTO;
using Aqovia.Interview.Todo.Data.Models;

namespace Aqovia.Interview.Todo.Data.Extensions
{
    public static class TodoModelExtensions {
        public static TodoDto ToTodoDto(this TodoModel model) {
            return new TodoDto() {
                Id = model.Id,
                Completed = model.Completed,
                Description = model.Description
            };
        }
    }
}