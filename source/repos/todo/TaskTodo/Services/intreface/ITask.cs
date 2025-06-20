using TaskTodo.Model.DTO;
using TaskTodo.Model.Entity;

namespace TaskTodo.Services.intreface
{
    public interface ITask
    {
        public Task<(int StatusCode,IEnumerable<TaskData> data ,bool Result)> GetAllTask();
        public Task<(int StatusCode, bool Result)> AddTask(AddTaskRequest req);
        public Task<(int StatusCode,List<TaskData> data ,bool Result)> GetTaskById(int id);
        public Task<(int StatusCode, bool Result)> TaskUpdate(int id,AddTaskRequest req);
        public Task<(int StatusCode, bool Result)> TaskDelete(int id);
        public Task<(int StatusCode, bool Result)> TaskRestore(int id);
        public Task<(int StatusCode, bool Result)> TaskComplete(int id);
        public Task<(int StatusCode, IEnumerable<TaskData> data, bool Result)> GetAllDeletedTask();
        public Task<(int StatusCode,bool Result)> DeleteParmenetly(int id);
        public Task<(int StatusCode, bool Result)> massUpload(IFormFile file);
        public Task<(int StatusCode, IEnumerable<TaskData> data, bool Result)> GetExcel();

    }
}
