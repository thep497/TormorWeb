using System;
namespace Tormor.DomainModel
{
    public interface IReferenceRepository
    {
        string CurrentUserName { get; set; }
        void DoDelete(global::Tormor.DomainModel.zz_Reference reference);
        void DoNewRecord(int refTypeId, string code, global::Tormor.DomainModel.zz_Reference reference);
        void DoSave(global::Tormor.DomainModel.zz_Reference reference, bool isCreate);
        global::System.Linq.IQueryable<global::Tormor.DomainModel.zz_Reference> FindAll(int refTypeId);
        global::Tormor.DomainModel.zz_Reference GetOne(int id);
        global::Tormor.DomainModel.zz_Reference GetOne(int refTypeId, string code);
        global::Tormor.DomainModel.zz_Reference GetOneByName(int refTypeId, string refName);
        int? GetRefRefTypeId(int refTypeId);
    }
}
