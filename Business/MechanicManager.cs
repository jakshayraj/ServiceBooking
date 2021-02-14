using Business.Interface;
using BusinessEntities;
using Data.Repository.Interface;
using System.Collections.Generic;

namespace Business
{
    public class MechanicManager : IMechanicManager
    {
        private readonly IMechanicRepository _MechanicRepository;

        public MechanicManager(IMechanicRepository MechanicRepository)
        {
            _MechanicRepository = MechanicRepository;
        }

        public IEnumerable<MechanicViewModel> GetAllMechanic()
        {
            return _MechanicRepository.GetAllMechanic();
        }
        public MechanicViewModel GetMechanic(int? Id)
        {
            return _MechanicRepository.GetMechanic(Id);
        }
        public string CreateMechanic(MechanicViewModel model)
        {
            return _MechanicRepository.CreateMechanic(model);
        }
        public string UpdateMechanic(MechanicViewModel model)
        {
            return _MechanicRepository.UpdateMechanic(model);
        }
        public string DeleteMechanic(int? Id)
        {
            return _MechanicRepository.DeleteMechanic(Id);
        }
    }
}
