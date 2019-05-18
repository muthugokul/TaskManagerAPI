using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagerApi.Model
{
    public class CreateTask
    {
        public string Name { get; set; }

        public int priority { get; set; }

        public CreateParentTask ParentTask { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
