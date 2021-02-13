using System;
namespace Tormor.DomainModel
{
    public interface IConveyanceInOutRepository
    {
        string CurrentUserName { get; set; }
        void DoDelete(Tormor.DomainModel.ConveyanceInOut convInOut);
        void DoNewRecord(Tormor.DomainModel.ConveyanceInOut convInOut);
        void DoSave(Tormor.DomainModel.ConveyanceInOut convInOut, bool isCreate);
        System.Collections.Generic.IList<ConveyanceInOut> FindAll(ConveyanceSearchInfo convSearch);
        System.Linq.IQueryable<Tormor.DomainModel.ConveyanceInOut> FindAll(DateTime? dtpFromDate = null, DateTime? dtpToDate = null);
        string GetNewCode();
        Tormor.DomainModel.ConveyanceInOut GetOne(int id);
        Tormor.DomainModel.ConveyanceInOut GetOneByCode(string code, DateTime stayYear, int id = -1);
    }
}
