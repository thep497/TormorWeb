using System;
namespace Tormor.DomainModel
{
    public interface IConveyanceRepository
    {
        string CurrentUserName { get; set; }
        void DoDelete(global::Tormor.DomainModel.Conveyance alien);
        void DoNewRecord(global::Tormor.DomainModel.Conveyance alien);
        void DoSave(global::Tormor.DomainModel.Conveyance alien, bool isCreate);
        global::System.Linq.IQueryable<global::Tormor.DomainModel.Conveyance> FindAll();
        global::Tormor.DomainModel.Conveyance GetOne(int id);
        global::System.Linq.IQueryable<global::Tormor.DomainModel.Conveyance> Search(string ownerCond, string nameCond);
    }
}
