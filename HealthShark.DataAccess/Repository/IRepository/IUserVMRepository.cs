using HealthShark.Models.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthShark.DataAccess.Repository.IRepository
{
    public interface IUserVMRepository:IRepository<UserVM>
    {
        public void Update(UserVM userVM);
    }
}
