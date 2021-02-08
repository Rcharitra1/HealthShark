using HealthShark.Models.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthShark.DataAccess.Repository.IRepository
{
    public interface IUserAssignmentRepository: IRepository<UserAssignmentVM>
    {
        void Update(UserAssignmentVM userAssignmentVM);
    }
}
