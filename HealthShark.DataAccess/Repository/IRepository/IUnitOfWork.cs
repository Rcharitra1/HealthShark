using System;
using System.Collections.Generic;
using System.Text;

namespace HealthShark.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork:IDisposable
    {
       public IBodyTypeRepository BodyType { get; }

        public IUserPlanRepository UserPlan { get; }


        public IApplicationUserRepository ApplicationUser { get; }

        public IWorkOutTypeRepository WorkOut { get; }


        public IDietRepository Diet { get; }

        public IUserVMRepository UserVM { get; }

        public IUserAssignmentRepository UserAssignmentVM { get; }
        void Save();
    }
}
