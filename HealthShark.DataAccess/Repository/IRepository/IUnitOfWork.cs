using System;
using System.Collections.Generic;
using System.Text;

namespace HealthShark.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork:IDisposable
    {
       public IBodyTypeRepository BodyType { get; }

        public IPackageRepository Package { get; }
        void Save();
    }
}
