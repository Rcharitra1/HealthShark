using HealthShark.Data;
using HealthShark.DataAccess.Repository.IRepository;
using HealthShark.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthShark.DataAccess.Repository
{
    public class WorkOutTypeRepository: Repository<WorkOutType>, IWorkOutTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public WorkOutTypeRepository(ApplicationDbContext db):base (db)
        {
            _db = db;
        }


        public void Update(WorkOutType workOut)
        {
            var objFromDb = _db.WorkOutTypes.Find(workOut.Id);
            if (objFromDb != null)
            {

                objFromDb.Type = workOut.Type;
                objFromDb.Description = workOut.Description;
            }
        }
    }
}
