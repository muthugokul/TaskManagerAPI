using System;

namespace TaskManagerApi.Model
{
    /// <summary>
    /// Parent Task model.
    /// </summary>
    public class ParentTask
    {
        /// <summary>
        /// Unique id of parent task.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of parent task.
        /// </summary>
        public string Name{ get; set; }
    }
}
