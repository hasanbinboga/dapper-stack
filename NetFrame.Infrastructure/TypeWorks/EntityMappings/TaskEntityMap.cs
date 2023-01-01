using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.FluentMap.Mapping;
using NetFrame.Core.Entities;

namespace NetFrame.Infrasturcture.TypeWorks.EntityMappings
{
   public  class TaskEntityMap : EntityMap<TaskEntity>
    {
        public TaskEntityMap()
        {
            Map(s => s.TaskActions).Ignore();
        }
    }
}
