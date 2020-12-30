using HealthShark.Data;
using HealthShark.DataAccess.Repository.IRepository;
using HealthShark.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthShark.DataAccess.Repository
{
    public class BodyTypeRepository: Repository<BodyType>, IBodyTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public BodyTypeRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(BodyType bodyType)
        {
            var objFromDb = _db.BodyTypes.FirstOrDefault(m => m.Id == bodyType.Id);

            if (objFromDb != null)
            {
                objFromDb.Description = bodyType.Description;
                _db.SaveChanges();

            }
        }
    }
}
