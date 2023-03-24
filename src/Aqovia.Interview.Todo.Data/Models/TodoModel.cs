using Aqovia.Interview.Data.Models;

namespace Aqovia.Interview.Todo.Data.Models {
    public class TodoModel: IBasicModel<Guid> {
        public Guid Id { get; set; }
        public bool Completed { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}