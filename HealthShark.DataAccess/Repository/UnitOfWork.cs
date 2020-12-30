using HealthShark.Data;
using HealthShark.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthShark.DataAccess.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _db;


        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            BodyType = new BodyTypeRepository(_db);
            Package = new PackageRepository(_db);
        }

        public IBodyTypeRepository BodyType { get; private set; }

        public IPackageRepository Package { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }


}
