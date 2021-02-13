using System;
namespace Tormor.DomainModel
{
    public interface IAlienRepository
    {
        string CurrentUserName { get; set; }
        void DoDelete(Tormor.DomainModel.Alien alien);
        void DoNewRecord(Tormor.DomainModel.Alien alien);
        void DoSave(Tormor.DomainModel.Alien alien, bool isCreate);
        System.Linq.IQueryable<Tormor.DomainModel.Alien> FindAll();
        Tormor.DomainModel.Alien GetOne(int id);
        System.Linq.IQueryable<Tormor.DomainModel.Alien> Search(string passportCond, string nameCond);
    }
}
