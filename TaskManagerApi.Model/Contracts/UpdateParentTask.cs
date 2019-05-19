using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskManagerApi.Model.Contracts
{
    public class UpdateParentTask
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
