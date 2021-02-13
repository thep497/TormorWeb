using System;
namespace Tormor.DomainModel
{
    public interface IStayRepository
    {
        string CurrentUserName { get; set; }
        string GetNewCode();
        void DoDelete(global::Tormor.DomainModel.Staying90Day stay);
        void DoNewRecord(global::Tormor.DomainModel.Staying90Day stay);
        void DoSave(global::Tormor.DomainModel.Staying90Day stay, bool isCreate);
        global::System.Linq.IQueryable<global::Tormor.DomainModel.Staying90Day> FindAll(DateTime? dtpFromDate = null, DateTime? dtpToDate = null);
        global::Tormor.DomainModel.Staying90Day GetOne(int id);
        global::Tormor.DomainModel.Staying90Day GetOneByCode(string code, DateTime stayYear, int id = -1);
    }
}
