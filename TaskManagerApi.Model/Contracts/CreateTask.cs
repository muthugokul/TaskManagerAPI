using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskManagerApi.Model.Contracts
{
    public class CreateTask
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Priority { get; set; }

        public CreateParentTask ParentTask { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
