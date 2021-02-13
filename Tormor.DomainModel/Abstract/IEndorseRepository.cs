using System;
namespace Tormor.DomainModel
{
    public interface IEndorseRepository
    {
        string CurrentUserName { get; set; }
        void DoDelete(global::Tormor.DomainModel.Endorse endorse);
        void DoNewRecord(global::Tormor.DomainModel.Endorse endorse);
        void DoSave(global::Tormor.DomainModel.Endorse endorse, bool isCreate);
        global::System.Linq.IQueryable<global::Tormor.DomainModel.Endorse> FindAll(DateTime? dtpFromDate = null, DateTime? dtpToDate = null);
        global::Tormor.DomainModel.Endorse GetOne(int id);
        global::Tormor.DomainModel.Endorse GetOneByCode(string code, DateTime endorseYear, int id = -1);
    }
}
