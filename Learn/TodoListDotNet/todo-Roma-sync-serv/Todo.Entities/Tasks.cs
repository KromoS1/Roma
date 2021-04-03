using System;

namespace Todo.Entities
{
    public class Tasks
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? Priority { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? Due { get; set; }
        public string Status { get; set; }
    }
}
