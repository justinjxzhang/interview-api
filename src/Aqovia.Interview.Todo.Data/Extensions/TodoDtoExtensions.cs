using Aqovia.Interview.Todo.Data.DTO;
using Aqovia.Interview.Todo.Data.Models;

namespace Aqovia.Interview.Todo.Data.Extensions
{
    public static class TodoDtoExtensions {
        public static TodoModel ToTodoModel(this TodoDto dto) {
            return new TodoModel() {
                Id = dto.Id,
                Completed = dto.Completed,
                Description = dto.Description
            };
        }
    }
}