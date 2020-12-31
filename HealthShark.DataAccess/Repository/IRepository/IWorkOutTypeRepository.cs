using HealthShark.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthShark.DataAccess.Repository.IRepository
{
    public interface IWorkOutTypeRepository: IRepository<WorkOutType>
    {
        void Update(WorkOutType workOut);
    }
}
