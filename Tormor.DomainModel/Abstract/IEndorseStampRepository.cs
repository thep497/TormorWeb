using System;
namespace Tormor.DomainModel
{
    public interface IEndorseStampRepository
    {
        string CurrentUserName { get; set; }
        void DoDelete(Tormor.DomainModel.EndorseStamp endorseStamp);
        void DoNewRecord(Tormor.DomainModel.EndorseStamp endorseStamp);
        void DoSave(Tormor.DomainModel.EndorseStamp endorseStamp, bool isCreate);
        System.Linq.IQueryable<Tormor.DomainModel.EndorseStamp> FindAll(int endorseId);
        Tormor.DomainModel.EndorseStamp GetOne(int endorseId, int endorseStampId);
        Tormor.DomainModel.EndorseStamp GetOneByCode(int endorseId, string code, DateTime endorseStampYear, int endorseStampId = -1);
    }
}
