using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApi.Model.Contracts;

namespace TaskManagerApi.Model.Mapper
{
    public static class ParentTaskMapper
    {
        public static Model.ParentTask Map(CreateParentTask newParentTask)
        {
            return new ParentTask
            {
                Id = newParentTask.Id,
                Name = newParentTask.Name
            };
        }

        public static Model.ParentTask Map(Model.ParentTask entity, UpdateParentTask updateParentTask)
        {
            if (entity == null)
            {
                entity = new Model.ParentTask();
            }

            entity.Id = updateParentTask.Id;
            entity.Name = updateParentTask.Name;

            return entity;
        }
    }
}
