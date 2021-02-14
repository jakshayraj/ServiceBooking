using BusinessEntities;
using Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerTests.TestRepository
{
    public class TestMechanicRepository : IMechanicRepository
    {
        private List<MechanicViewModel> _dbContext = new List<MechanicViewModel>
        {
            new MechanicViewModel{
                MechanicId =1,
                Name = "Vijay",
                MobileNo = "8989076548",
                EmailId = "vijay@gmail.com",
                Brand = "Kia",
            }
        };

        public string CreateMechanic(MechanicViewModel model)
        {
            if (model != null)
            {
                int Mechanic = _dbContext.Where(a => a.MechanicId == model.MechanicId).Count();
                if (Mechanic > 0)
                {
                    return "Exist";
                }
                _dbContext.Add(model);
                return "Mechanic added";
            }

            return "Model is null";
        }

        public string DeleteMechanic(int? Id)
        {
            int id = (int)Id;
            MechanicViewModel MechanicEntitie = _dbContext.Where(s => s.MechanicId == id).First();
            _dbContext.Remove(MechanicEntitie);
            return "Deleted";
        }

        public IEnumerable<MechanicViewModel> GetAllMechanic()
        {
            IEnumerable<MechanicViewModel> MechanicEntities = _dbContext;
            return MechanicEntities;
        }

        public MechanicViewModel GetMechanic(int? Id)
        {
            int id = (int)Id;
            MechanicViewModel MechanicsEntities = _dbContext.Where(s => s.MechanicId == id).First();
            return MechanicsEntities;
        }

        public string UpdateMechanic(MechanicViewModel model)
        {
            if (model != null)
            {
                _dbContext.Add(model);
                return "Mechanic updated";
            }
            return "Model is null";
        }
    }
}
