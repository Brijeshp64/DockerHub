namespace TaskTodo.Model.DTO
{
    public class GetData
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool IsCompleted { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime DueDate { get; set; }

    }
}
