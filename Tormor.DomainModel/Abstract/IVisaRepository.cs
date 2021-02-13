using System;
namespace Tormor.DomainModel
{
    public interface IVisaRepository
    {
        string CurrentUserName { get; set; }
        void DoDelete(Tormor.DomainModel.VisaDetail visa);
        void DoNewRecord(Tormor.DomainModel.VisaDetail visa);
        void DoSave(Tormor.DomainModel.VisaDetail visa, bool isCreate);
        System.Linq.IQueryable<Tormor.DomainModel.VisaDetail> FindAll(DateTime? dtpFromDate, DateTime? dtpToDate);
        Tormor.DomainModel.VisaDetail GetOne(int id);
        Tormor.DomainModel.VisaDetail GetOneByCode(string code, DateTime visaYear, int id = -1);
    }
}
