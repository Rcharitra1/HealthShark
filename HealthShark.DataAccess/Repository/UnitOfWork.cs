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
            UserPlan = new UserPlanRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            WorkOut = new WorkOutTypeRepository(_db);
            Diet = new DietRepository(_db);
        }

        public IBodyTypeRepository BodyType { get; private set; }

        public IUserPlanRepository UserPlan { get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }


        public IDietRepository Diet { get; private set; }

        public IWorkOutTypeRepository WorkOut { get; private set; }

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
