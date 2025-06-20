using ExcelDataReader;
using TaskTodo.DAL.AccessService;
using TaskTodo.Data;
using TaskTodo.Model.DTO;
using TaskTodo.Model.Entity;
using TaskTodo.Services.intreface;

namespace TaskTodo.Services.Repo
{
    public class TaskService : ITask
    {
        private readonly ITaskAuth _context;
        private readonly ApplicationDbContext _applicationDb;
        public TaskService(ITaskAuth context, ApplicationDbContext applicationDb)
        {
            _context = context;
            _applicationDb = applicationDb;
        }



        public async Task<(int StatusCode, IEnumerable<TaskData> data, bool Result)> GetAllTask()
        {
            try
            {
                var result = await _context.GetAllAsync();

                var RES = result
                          .Where(c => !c.IsCompleted)
                          .Where(c => !c.Flag)
                          .ToList();
                return (200, RES, true);
            }
            catch (Exception ex)
            {
                return (500, null, false);

            }

        }
        public async Task<(int StatusCode, bool Result)> AddTask(AddTaskRequest req)
        {
            try
            {
                var data = new TaskData
                {
                    Title = req.Title,
                    Description = req.Description,
                    IsCompleted = false,
                    DueDate = req.DueDate,
                    Flag = false,
                };
                await _context.AddData(data);
                return (200, true);
            }
            catch (Exception ex)
            {
                return (500, false);
            }
        }

        public async Task<(int StatusCode, List<TaskData> data, bool Result)> GetTaskById(int id)
        {
            try
            {
                var result = await _context.GetByIdAsync(id);
                return (200, new List<TaskData> { result } , true);
            }
            catch (Exception ex)
            {
                return (500, null, false);

            }
        }

        public async Task<(int StatusCode, bool Result)> TaskUpdate(int id, AddTaskRequest req)
        {
            try
            {
                var data = await _context.GetByIdAsync(id);

                data.Title = req.Title ?? data.Title;
                data.Description = req.Description ?? data.Description;
                data.DueDate = req.DueDate ?? data.DueDate;

                await _context.UpdateAsync(data);
                return (200, true);
            }catch(Exception ex)
            {
                return (500, false);
            }
        }

        public async Task<(int StatusCode, bool Result)> TaskDelete(int id)
        {
            try
            {
                var data = await _context.GetByIdAsync(id);

                data.Flag = true;
                await _context.UpdateAsync(data);

                return (200, true);
            }
            catch (Exception)
            {
                return (500, false);
            }
        }

        public async Task<(int StatusCode, bool Result)> TaskRestore(int id)
        {
            try
            {
                var data = await _context.GetByIdAsync(id);

                data.Flag = false;
                await _context.UpdateAsync(data);

                return (200, true);
            }
            catch (Exception)
            {
                return (500, false);
            }
        }

        public async Task<(int StatusCode, bool Result)> TaskComplete(int id)
        {
            try
            {
                var data = await _context.GetByIdAsync(id);

                data.IsCompleted = true;
                await _context.UpdateAsync(data);

                return (200, true);
            }
            catch (Exception)
            {
                return (500, false);
            }
        }

        public async Task<(int StatusCode, IEnumerable<TaskData> data, bool Result)> GetAllDeletedTask()
        {
            try
            {
                var result = await _context.GetAllAsync();

                var RES = result
                          .Where(c => !c.IsCompleted && c.Flag)
                          .ToList();
                return (200, RES, true);
            }
            catch (Exception ex)
            {
                return (500, null, false);

            }
        }
        public async Task<(int StatusCode, bool Result)> DeleteParmenetly(int id)
        {
            try
            {
                var data = await _context.GetByIdAsync(id);
                await _context.DeleteAsync(data);
                return(200, true);
            }
            catch (Exception ex)
            {
                return (500, false);
            }
        }

        public async Task<(int StatusCode, bool Result)> massUpload(IFormFile file)
        {
            try
            {
                if(file == null || file.Length == 0)
                {
                    return (500, false);
                }
                var uploadFolder = $"{Directory.GetCurrentDirectory()}\\upload";
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                var filePath = Path.Combine(uploadFolder, file.Name);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        bool HeaderSkip  = false;   
                        do
                        {
                            while (reader.Read())
                            {
                                if(!HeaderSkip)
                                {
                                    HeaderSkip = true;
                                    continue;
                                }
                                TaskData t = new TaskData();
                                t.Title = reader.GetValue(1).ToString();
                                t.Description = reader.GetValue(2).ToString();
                                t.IsCompleted = false;

                                _applicationDb.AddAsync(t);
                                _applicationDb.SaveChangesAsync();
                            }
                        } while (reader.NextResult());

                        
                    }
                }
                return (200, true);
            }
            catch (Exception ex) {
                return (500,false);
            }
        }

        public async Task<(int StatusCode, IEnumerable<TaskData> data, bool Result)> GetExcel()
        {
            var data = await _context.GetAllAsync();
           

            return (200, data, true);
        }

       
    }
}
