using System.ComponentModel.DataAnnotations;

namespace TaskTodo.Model.Entity
{
    public class TaskData
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool IsCompleted { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DueDate { get; set; }

        public bool Flag { get; set; }
    }
}
