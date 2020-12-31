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
        void Save();
    }
}
