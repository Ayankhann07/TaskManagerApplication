using TaskManagerApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManagerApplication.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskModel>> GetTasksAsync();
        Task<int> AddTaskAsync(TaskModel task);
        Task<int> UpdateTaskAsync(TaskModel task);
        Task<int> DeleteTaskAsync(TaskModel task);
    }
}
