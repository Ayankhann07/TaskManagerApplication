using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TaskManagerApplication.Models;
using TaskManagerApplication.Interfaces;

namespace TaskManagerApplication.Services
{
    public class TaskService : ITaskService
    {
        private SQLiteAsyncConnection _db;

        private async Task Init()
        {
            if (_db != null)
                return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "tasks.db3");
            _db = new SQLiteAsyncConnection(dbPath);
            await _db.CreateTableAsync<TaskModel>();
        }

        public async Task<List<TaskModel>> GetTasksAsync()
        {
            await Init();
            return await _db.Table<TaskModel>().ToListAsync();
        }

        public async Task<int> AddTaskAsync(TaskModel task)
        {
            await Init();
            return await _db.InsertAsync(task);
        }

        public async Task<int> UpdateTaskAsync(TaskModel task)
        {
            await Init();
            return await _db.UpdateAsync(task);
        }

        public async Task<int> DeleteTaskAsync(TaskModel task)
        {
            await Init();
            return await _db.DeleteAsync(task);
        }
        public interface ITaskService
        {
            Task<List<TaskModel>> GetTasksAsync();
            Task SaveTaskAsync(TaskModel task);
        }

    }
}
