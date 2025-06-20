namespace TaskTodo.Model.DTO
{
    public class AddTaskRequest
    {
        public string Title { get; set; } 

        public string Description { get; set; } 

        public DateTime? DueDate { get; set; }

    }
}
