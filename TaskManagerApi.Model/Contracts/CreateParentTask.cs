using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskManagerApi.Model.Contracts
{
    public class CreateParentTask
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
