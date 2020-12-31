using HealthShark.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthShark.DataAccess.Repository.IRepository
{
    public interface IDietRepository: IRepository<Diet>
    {
        void Update(Diet diet);
    }
}
