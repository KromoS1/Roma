using System;
using System.Net.Http;
using System.Threading.Tasks;
using Todo.Entities;

namespace Todo.RestTaskRepository
{
    public class TaskRepository : ITaskRepository
    {
        private const string ServiceUrl = "http://localhost:5000";
        private readonly HttpClient _clientTask = new HttpClient{BaseAddress = new Uri(ServiceUrl)};
        private string requestUri = "api/task/";
        
        public async Task<Tasks[]> GetAllTasks()
        {
            HttpResponseMessage response = await _clientTask.GetAsync(requestUri);
            return response.IsSuccessStatusCode 
                ? await response.Content.ReadAsAsync<Tasks[]>() 
                : Array.Empty<Tasks>();
        }

        public async Task<Tasks> GetTask(int? id)
        {
            HttpResponseMessage response = await _clientTask.GetAsync(requestUri + $"{id}");
            return response.IsSuccessStatusCode
                ? await response.Content.ReadAsAsync<Tasks>()
                : throw new Exception("Wrong id entered.");
        }

        public async Task<string> CreateTask(Tasks task)
        {
            HttpResponseMessage response = await _clientTask.PostAsJsonAsync(requestUri,task);
            return response.IsSuccessStatusCode
                ? response.Headers.Location.ToString()
                : throw new Exception("It is impossible to create a task.");
        }

        public async Task<string> UpdateTask(int? id, Tasks task)
        {
            HttpResponseMessage response = await _clientTask.PutAsJsonAsync(requestUri + $"{id}", task);
            return response.IsSuccessStatusCode
                ? response.ReasonPhrase
                : throw new Exception("Can't change task.");
        }

        public async Task<string> Delete(int? id)
        {
            HttpResponseMessage response = await _clientTask.DeleteAsync(requestUri + $"{id}");
            return response.IsSuccessStatusCode
                ? response.ReasonPhrase
                : throw new Exception("Item cannot be deleted.");
        }

    }
}
