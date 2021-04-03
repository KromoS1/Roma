using System;

namespace Todo.Web.Controllers
{
    public class UpdateTaskRequest
    {
        public int? Priority { get; set; }
        public DateTime? Due { get; set; }
        public string Status { get; set; }
    }
}