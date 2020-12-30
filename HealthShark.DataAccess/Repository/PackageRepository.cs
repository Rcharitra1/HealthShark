using HealthShark.Data;
using HealthShark.DataAccess.Repository.IRepository;
using HealthShark.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthShark.DataAccess.Repository
{
    public class PackageRepository: Repository<Package>, IPackageRepository
    {
        private readonly ApplicationDbContext _db;


        public PackageRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Package package)
        {
            var objFromDb = _db.Packages.Find(package.Id);
            if (objFromDb != null)
            {
                if (package.ImageUrl != null)
                {
                    objFromDb.ImageUrl = package.ImageUrl;
                    
                }

                objFromDb.Name = package.Name;
                objFromDb.StartDate = package.StartDate;
                objFromDb.EndDate = package.EndDate;
                objFromDb.Price = package.Price;
                objFromDb.CouplePrice = objFromDb.CouplePrice;
                objFromDb.Description = objFromDb.Description;
                _db.SaveChanges();
            }
        }
    }
}
