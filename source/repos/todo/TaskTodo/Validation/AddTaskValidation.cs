using TaskTodo.Model.DTO;

namespace TaskTodo.Validation
{
    public class AddTaskValidation
    {
        public static Dictionary<string, string> Validate(AddTaskRequest req)
        {
         var err = new Dictionary<string, string>();
            if (string.IsNullOrWhiteSpace(req.Title))
                err["Title"] = "Title is required";
            if (string.IsNullOrWhiteSpace(req.Description))
                err["Description"] = "Description is required";
            return err;

        }
    }
}
