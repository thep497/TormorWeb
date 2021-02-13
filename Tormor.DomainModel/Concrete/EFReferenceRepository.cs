using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NNS.ModelHelpers;
using NNS.GeneralHelpers;

namespace Tormor.DomainModel
{
    public class EFReferenceRepository : Tormor.DomainModel.IReferenceRepository
    {
        #region Private members....
        private NeoIMOSKEntities entities = new NeoIMOSKEntities();

        /// <summary>
        /// What to do before save any edit or newly created record
        /// </summary>
        /// <param name="reference"></param>
        private void _doBeforePost(zz_Reference reference)
        {
            if (reference.RefDesc == null)
                reference.RefDesc = "";
            reference.UpdateInfo.LogUpdated(CurrentUserName);
        }

        /// <summary>
        /// สร้าง code
        /// </summary>
        /// <returns>ถ้าแปลงเป็น Int ได้จะ return ตัวเลขให้ แต่ถ้าไม่ได้ ก็ส่ง error ไปแจ้ง</returns>
        private string _getNewCode(int refTypeId,string defaultCode)
        {
            if (defaultCode != null && defaultCode != "")
                return defaultCode;

            string result = (from p in FindAll(refTypeId)
                             select p.Code).DefaultIfEmpty("000").Max();

            int iResult;
            try
            {
                iResult = Convert.ToInt32(result);
            }
            catch
            {
                iResult = -1;
            }
            if (iResult >= 0)
                return (iResult + 1).ToString("D3");

            //ถ้าไม่สามารถแปลงได้ ให้ throw error แสดงให้ user เห็น
            var clash = new RulesException<zz_Reference>();
            clash.ErrorFor(x => x.Code, "สร้างรหัสอัตโนมัติไม่ได้เนื่องจาก code เดิมไม่ใช่ตัวเลข");
            throw clash;
        }

        //
        // Insert / Delete Method
        /// <summary>
        /// สำหรับเพิ่ม record ลง ef
        /// </summary>
        /// <param name="reference"></param>
        private void _add(zz_Reference reference)
        {
            entities.zz_References.AddObject(reference);
        }

        /// <summary>
        /// สำหรับลบ record ออกจาก ef รวมทั้งลบลูกที่เกี่ยวข้อง (ถ้ามี)
        /// </summary>
        /// <param name="reference"></param>
        private void _delete(zz_Reference reference)
        {
            // ถ้ามี ref ตัวลูก ให้ loop ลบ ref ก่อน...
            entities.zz_References.DeleteObject(reference);
            //th20110407 กรณี reference ให้ลบทิ้งเลย ไม่อย่างนั้น จะกรอกรหัสเดิมไม่ได้ ติด Unique Index 
            //  reference.IsCancel = true;
        }

        /// <summary>
        /// ตรวจสอบว่าข้อมูลที่กรอกเข้ามาถูกต้องหรือไม่ 
        /// โดยเป็นส่วนของ logic ที่ไม่สามารถระบุใน metadata ไม่ได้ และไม่ได้ระบุเอาไว้ใน constraint ของ database
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="isCreate"></param>
        private void _validate(zz_Reference reference, bool isCreate)
        {
            var errors = new RulesException<zz_Reference>();

            if (reference.RefName == "a")
                errors.ErrorFor(x => x.RefName, "a is not allowed...");

            if (errors.Errors.Any())
                throw errors;
        }

        //
        // Persistance
        /// <summary>
        /// บันทึกข้อมูลลง database (ef) จริง ๆ
        /// </summary>
        private void _save()
        {
            try
            {
                entities.SaveChanges();
            }
            catch (Exception ex)
            {
                //ตรวจสอบ error จาก sql server
                string exMessage = ex.ExMessage();

                //เช็คค่า unique index ไม่ต้อง loop เช็คซ้ำอีกแล้ว
                if (exMessage.Contains("IX_zz_References_RefTypeId_Code"))
                {
                    var clash = new RulesException<zz_Reference>();
                    clash.ErrorFor(x => x.Code, "รหัสซ้ำไม่ได้ค่ะ");
                    throw clash;
                }
                if (exMessage.Contains("IX_zz_References_RefTypeId_RefName"))
                {
                    var clash = new RulesException<zz_Reference>();
                    clash.ErrorFor(x => x.RefName, "ความหมายซ้ำไม่ได้ค่ะ");
                    throw clash;
                }
                throw; // Rethrow any other exceptions to avoid interfering with them 
            }
        }

        #endregion

        #region Public (Interface)...
        public string CurrentUserName { get; set; }
        //
        // Query Method

        /// <summary>
        /// เรียกข้อมูล reference ทั้งหมด
        /// </summary>
        /// <returns>reference table</returns>
        public IQueryable<zz_Reference> FindAll(int refTypeId)
        {
            return entities.zz_References
                .Where(p => p.RefTypeId == refTypeId && !p.IsCancel);
        }

        /// <summary>
        /// เรียกข้อมูล reference ตาม id ที่ระบุ
        /// </summary>
        /// <param name="id">reference's id</param>
        /// <returns>reference entity</returns>
        public zz_Reference GetOne(int id)
        {
            return entities.zz_References
                .Where(p => !p.IsCancel) //ไม่ว่าจะอย่างไร จะไม่แสดงข้อมูล iscancel ใน Domain นี้เลย
                .FirstOrDefault(p => p.Id == id);
        }
        public zz_Reference GetOne(int refTypeId, string code)
        {
            return FindAll(refTypeId)
                .FirstOrDefault(p => p.RefTypeId == refTypeId && p.Code == code);
        }

        public zz_Reference GetOneByName(int refTypeId, string refName)
        {
            return entities.zz_References
                .Where(p => !p.IsCancel) //ไม่ว่าจะอย่างไร จะไม่แสดงข้อมูล iscancel ใน Domain นี้เลย
                .FirstOrDefault(p => p.RefTypeId == refTypeId && p.RefName == refName);
        }

        public int? GetRefRefTypeId(int refTypeId)
        {
            var refTypeIdStr = refTypeId.ToString(); //ใช้ toString ใน LINQ ไม่ได้ เลยต้องประกาศอย่างนี้ ???
            var refRefTypeId = (from p in FindAll(0)
                                where p.Code == refTypeIdStr
                                select p.RefRefTypeId)
                               .FirstOrDefault();
            return refRefTypeId;
        }

        /// <summary>
        /// What to do when create new reference
        /// </summary>
        /// <param name="reference"></param>
        public void DoNewRecord(int refTypeId, string code, zz_Reference reference)
        {
            reference.RefTypeId = refTypeId;
            reference.Code = _getNewCode(refTypeId, code);
            //สร้าง reference (ถ้ามี)
            reference.RefRefTypeId = GetRefRefTypeId(refTypeId);

            reference.UpdateInfo.LogAdded(CurrentUserName);
        }

        /// <summary>
        /// prepare data and save to ef, ใช้ทั้ง update และ insert
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="isCreate">ส่ง true ถ้าต้องการ insert</param>
        public void DoSave(zz_Reference reference, bool isCreate)
        {
            if (isCreate)
                _add(reference);

            _doBeforePost(reference);
            _validate(reference, isCreate);
            _save();
        }

        /// <summary>
        /// prepare data and delete from ef
        /// </summary>
        /// <param name="reference"></param>
        public void DoDelete(zz_Reference reference)
        {
            if (_canDeleteData(reference))
            {
                _delete(reference);
                _save();
            }
            else
            {
                throw new ApplicationException("มีใช้งานอยู่ ลบไม่ได้ค่ะ");
            }
        }

        private bool _canDeleteData(zz_Reference reference)
        {
            int numChild = (from p in FindAll(reference.RefTypeId)
                            where (p.RefCode == reference.Code)
                            select p.Id).Count();
            return numChild == 0;
        }
        #endregion
    }
}
