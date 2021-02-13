using System;
namespace Tormor.DomainModel
{
    public interface ICrewRepository
    {
        string CurrentUserName { get; set; }
        void DoDelete(Tormor.DomainModel.Crew crew);
        void DoNewRecord(Tormor.DomainModel.Crew crew);
        void DoSave(Tormor.DomainModel.Crew crew, bool isCreate);
        System.Linq.IQueryable<Tormor.DomainModel.Crew> FindAll(int conveyanceInOutId, bool isCrew);
        Tormor.DomainModel.Crew GetOne(int conveyanceInOutId, bool isCrew, int crewId);
        Tormor.DomainModel.Crew GetOneByCode(int conveyanceInOutId, string code, bool isCrew, int crewId = -1);
    }
}
