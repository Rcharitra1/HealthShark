using HealthShark.Data;
using HealthShark.DataAccess.Repository.IRepository;
using HealthShark.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthShark.DataAccess.Repository
{
    public class UserPlanRepository: Repository<UserPlan>, IUserPlanRepository
    {
        private readonly ApplicationDbContext _db;


        public UserPlanRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(UserPlan plan)
        {
            var objFromDb = _db.UserPlans.Find(plan.Id);
            if (objFromDb != null)
            {
                if (plan.ImageUrl != null)
                {
                    objFromDb.ImageUrl = plan.ImageUrl;
                    
                }

                objFromDb.Name = plan.Name;
                objFromDb.Duration = plan.Duration;
                objFromDb.Active = plan.Active;
                objFromDb.Price = plan.Price;
                objFromDb.CouplePrice = plan.CouplePrice;
                objFromDb.Description = plan.Description;
                _db.SaveChanges();
            }
        }
    }
}
