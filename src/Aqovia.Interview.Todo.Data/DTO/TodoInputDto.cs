namespace Aqovia.Interview.Todo.Data.DTO
{
    public class TodoInputDto {
        public bool Completed { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}