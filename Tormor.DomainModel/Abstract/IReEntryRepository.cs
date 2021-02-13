using System;
namespace Tormor.DomainModel
{
    public interface IReEntryRepository
    {
        string CurrentUserName { get; set; }
        void DoDelete(Tormor.DomainModel.ReEntry reentry);
        void DoNewRecord(Tormor.DomainModel.ReEntry reentry);
        void DoSave(Tormor.DomainModel.ReEntry reentry, bool isCreate);
        System.Linq.IQueryable<Tormor.DomainModel.ReEntry> FindAll(DateTime? dtpFromDate = null, DateTime? dtpToDate = null);
        Tormor.DomainModel.ReEntry GetOne(int id);
        Tormor.DomainModel.ReEntry GetOneByCode(string code, DateTime reentryYear, int id = -1);
    }
}
