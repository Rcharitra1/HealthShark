using HealthShark.Data;
using HealthShark.DataAccess.Repository.IRepository;
using HealthShark.Models.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthShark.DataAccess.Repository
{
    public class UserAssignmentRepository: Repository<UserAssignmentVM>, IUserAssignmentRepository
    {
        private readonly ApplicationDbContext _db;

        public UserAssignmentRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(UserAssignmentVM vm)
        {
            var objToUpdate = _db.UserAssignmentVMs.Find(vm.Id);

            if(objToUpdate != null)
            {
                objToUpdate.BodyTypeId = vm.BodyTypeId;
                objToUpdate.DietId = vm.DietId;
                objToUpdate.Paid = vm.Paid;
                objToUpdate.TrainerId = vm.TrainerId;
                objToUpdate.UserId = vm.UserId;
                objToUpdate.WorkoutId = vm.WorkoutId;
                objToUpdate.DieticianId = vm.DieticianId;
                objToUpdate.UserPlanId = vm.UserPlanId;

                _db.SaveChanges();
            }
        }
    }
}
