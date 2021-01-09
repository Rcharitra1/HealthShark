using HealthShark.Data;
using HealthShark.DataAccess.Repository.IRepository;
using HealthShark.Models.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthShark.DataAccess.Repository
{
    public class UserVMRepository: Repository<UserVM>, IUserVMRepository
    {
        private readonly ApplicationDbContext _db;

        public UserVMRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(UserVM userVM)
        {
            var objFromDb = _db.UserVMs.Find(userVM.Id);
            if (objFromDb != null)
            {
                objFromDb.Weight = userVM.Weight;
                objFromDb.Height = userVM.Height;
                objFromDb.Allergies = userVM.Allergies;
                if (userVM.ImageUrl != null)
                {
                    objFromDb.ImageUrl = userVM.ImageUrl;
                }

                objFromDb.MedicalHistory = userVM.MedicalHistory;

                _db.UserVMs.Update(objFromDb);
                
            }
        }

    }
}
