using Aqovia.Interview.Data.Repositories;
using Aqovia.Interview.Todo.Data.Models;

namespace Aqovia.Interview.Todo.Data.Services
{
    public class TodoService {
        private readonly IBasicRepository<Guid, TodoModel> _todoRepository;
        public TodoService(
            IBasicRepository<Guid, TodoModel> todoRepository
        ) {
            this._todoRepository = todoRepository;
        }

        public bool TryGetTodoById(Guid id, out TodoModel? model) {
            return this._todoRepository.TryGetById(id, out model);
        }

        public IEnumerable<TodoModel> GetTodos(Func<TodoModel, bool> filterFunc) {
            return this._todoRepository.Get().Where(filterFunc);
        }

        public TodoModel AddTodo(bool completed, string description) {
            var newTodo = this._todoRepository.Create(new TodoModel() {
                Id = default(Guid),
                Completed = completed,
                Description = description
            });
            return newTodo;
        }

        public bool UpdateTodo(Guid id, bool completed, string description) {
            var result = false;
            try {
                this._todoRepository.Update(id, new TodoModel() {
                    Id = id,
                    Completed = completed,
                    Description = description
                });
                result = true;
            } catch (KeyNotFoundException) {
                // do something meaningful
                throw;
            }
            return result;
        }

        public void DeleteTodo(Guid id) {
            this._todoRepository.Delete(id);
        }
    }
}