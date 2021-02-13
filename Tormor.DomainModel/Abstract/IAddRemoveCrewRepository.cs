// *******************************************************************
//1. ชื่อโปรแกรม 	: NeoSKIMO
//2. ชื่อระบบ 		: DomainModel
//3. ชื่อ Unit 	: IAddRemoveCrewRepository
//4. ผู้เขียนโปรแกรม	: อมรเทพ
//5. วันที่สร้าง	    : 201012xx
//6. จุดประสงค์ของ Unit นี้ : Interface Class เก็บชื่อ function สำหรับจัดการกับข้อมูลของ AddRemoveCrew
// *******************************************************************
// แก้ไขครั้งที่ : 1
// ประวัติการแก้ไข
// ครั้งที่ 0 : เริ่มต้นสร้าง unit ใหม่
// ครั้งที่ 1 : //th20110409 เพิ่มคนลดลงในเรือออก PD18-540102 Req 8.1/8.2
// *******************************************************************
using System;
namespace Tormor.DomainModel
{
    public interface IAddRemoveCrewRepository
    {
        string CurrentUserName { get; set; }
        void DoDelete(global::Tormor.DomainModel.AddRemoveCrew addRemoveCrew);
        void DoNewRecord(global::Tormor.DomainModel.AddRemoveCrew addRemoveCrew, int addRemoveType);
        void DoSave(global::Tormor.DomainModel.AddRemoveCrew addRemoveCrew, bool isCreate, int addRemoveType);
        global::System.Linq.IQueryable<global::Tormor.DomainModel.AddRemoveCrew> FindAll(int? addRemoveType = null, DateTime? dtpFromDate = null, DateTime? dtpToDate = null);
        global::System.Linq.IQueryable<global::Tormor.DomainModel.AddRemoveCrew> SearchByInOutDate(int addRemoveType, DateTime? dtpFromDate = null, DateTime? dtpToDate = null);
        global::System.Linq.IQueryable<global::Tormor.DomainModel.AddRemoveCrew> SearchByConveyanceLastIn(string convNameCond, DateTime convOutDate);
        global::Tormor.DomainModel.AddRemoveCrew GetOne(int id, int? addRemoveType);
        global::Tormor.DomainModel.AddRemoveCrew GetOneByCode(string code, DateTime addRemoveCrewYear, int addRemoveType, int id = -1);
    }
}
