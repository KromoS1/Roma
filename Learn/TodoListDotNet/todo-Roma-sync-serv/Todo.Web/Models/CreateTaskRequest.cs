using System;

namespace Todo.Web.Controllers
{
    public class CreateTaskRequest
    {
        public string Name { get; set; }
        public int? Priority { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? Due { get; set; }
        public string Status { get; set; }
    }
}