using HealthShark.Data;
using HealthShark.DataAccess.Repository.IRepository;
using HealthShark.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthShark.DataAccess.Repository
{
    public class DietRepository: Repository<Diet>, IDietRepository
    {
        private readonly ApplicationDbContext _db;

        public DietRepository(ApplicationDbContext db):base (db)
        {
            _db = db;
        }


        public void Update(Diet diet)
        {
            var objFromDb = _db.Diets.Find(diet.Id);
            if (objFromDb != null)
            {
                objFromDb.Type = diet.Type;
                objFromDb.Description = diet.Description;
            }
        }
    }
}
