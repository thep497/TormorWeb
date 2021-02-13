using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NNS.ModelHelpers;
using NNS.GeneralHelpers;
using System.Reflection;
using System.Linq.Expressions;

namespace Tormor.DomainModel
{
    public class EFEndorseStampRepository : Tormor.DomainModel.IEndorseStampRepository
    {
        #region Private members....
        private NeoIMOSKEntities entities = new NeoIMOSKEntities();

        /// <summary>
        /// What to do before save any edit or newly created record
        /// </summary>
        /// <param name="endorseStamp"></param>
        private void _doBeforePost(EndorseStamp endorseStamp, bool isCreate)
        {
            endorseStamp.ClearNullDate();
            endorseStamp.UpdateInfo.LogUpdated(CurrentUserName);

            if (isCreate)
            {
            }
        }

        //
        // Insert / Delete Method
        /// <summary>
        /// สำหรับเพิ่ม record ลง ef
        /// </summary>
        /// <param name="endorseStamp"></param>
        private void _add(EndorseStamp endorseStamp)
        {
            entities.EndorseStamps.AddObject(endorseStamp);
        }

        /// <summary>
        /// สำหรับลบ record ออกจาก ef รวมทั้งลบลูกที่เกี่ยวข้อง (ถ้ามี)
        /// </summary>
        /// <param name="endorseStamp"></param>
        private void _delete(EndorseStamp endorseStamp)
        {
            // ถ้ามี ref ตัวลูก ให้ loop ลบ ref ก่อน...
            //entities.Endorses.DeleteObject(endorseStamp);
            endorseStamp.IsCancel = true;
        }

        /// <summary>
        /// ตรวจสอบว่าข้อมูลที่กรอกเข้ามาถูกต้องหรือไม่ 
        /// โดยเป็นส่วนของ logic ที่ไม่สามารถระบุใน metadata ไม่ได้ และไม่ได้ระบุเอาไว้ใน constraint ของ database
        /// </summary>
        /// <param name="endorseStamp"></param>
        /// <param name="isCreate"></param>
        private void _validate(EndorseStamp endorseStamp, bool isCreate)
        {
            var errors = new RulesException<EndorseStamp>();

            if (endorseStamp.Code != "")
            {
                int endorseStampId = 0;
                if (!isCreate) endorseStampId = endorseStamp.Id;
                var oldCode = (from p in entities.EndorseStamps
                                  where (p.Id != endorseStampId) &&
                                        (p.Code == endorseStamp.Code) &&
                                        (p.StampDate.Year == endorseStamp.StampDate.Year)
                                  select p.Id).Count();
                if (oldCode > 0)
                    errors.ErrorFor(x => x.Code, "Code ที่กรอก ซ้ำกับรายการอื่นในปีเดียวกัน");
            }

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
            catch
            {
                throw; // Rethrow any other exceptions to avoid interfering with them 
            }
        }

        /// <summary>
        /// เรียกการบันทึกข้อมูล โดยตรวจสอบว่า Code ซ้ำหรือไม่
        /// </summary>
        /// <param name="endorseStamp"></param>
        /// <param name="numRecursive">จำนวนที่ให้วน loop ปรกติไม่ต้องใส่มา</param>
        private void _saveCheckCode(EndorseStamp endorseStamp, int numRecursive = 5)
        {
            try
            {
                _save();
            }
            catch (Exception ex)
            {
                var clash = new RulesException<EndorseStamp>();
                clash.ErrorFor(x => x, "Error :" + ex.ExMessage());
                throw clash;
            }
        }
        #endregion

        #region Public (Interface)...
        public string CurrentUserName { get; set; }
        //
        // Query Method

        #region Get Data Section...
        /// <summary>
        /// เรียกข้อมูล endorseStamp ทั้งหมด
        /// </summary>
        /// <returns>endorseStamp table</returns>
        public IQueryable<EndorseStamp> FindAll(int endorseId)
        {
            return entities.EndorseStamps.Where(p => p.EndorseId == endorseId && !p.IsCancel);
        }

        /// <summary>
        /// เรียกข้อมูล endorseStamp ตาม id ที่ระบุ
        /// </summary>
        /// <param name="id">endorseStamp's id</param>
        /// <returns>endorseStamp entity</returns>
        public EndorseStamp GetOne(int endorseId, int endorseStampId)
        {
            return this.FindAll(endorseId).FirstOrDefault(p => p.Id == endorseStampId);
        }

        /// <summary>
        /// เรียกข้อมูล endorseStamp ตาม id ที่ระบุ
        /// </summary>
        /// <param name="id">endorseStamp's id</param>
        /// <returns>endorseStamp entity</returns>
        public EndorseStamp GetOneByCode(int endorseId, string code, DateTime endorseStampYear, int endorseStampId = -1)
        {
            return this.FindAll(endorseId)
                .FirstOrDefault(p => (p.Code == code) &&
                                     (p.StampDate.Year == endorseStampYear.Year) &&
                                     (p.Id != endorseStampId));
        }
        #endregion

        /// <summary>
        /// What to do when create new endorseStamp
        /// </summary>
        /// <param name="endorseStamp"></param>
        public void DoNewRecord(EndorseStamp endorseStamp)
        {
            endorseStamp.UpdateInfo.LogAdded(CurrentUserName);
            
            endorseStamp.IsCancel = false;
        }

        /// <summary>
        /// prepare data and save to ef, ใช้ทั้ง update และ insert
        /// </summary>
        /// <param name="endorseStamp"></param>
        /// <param name="isCreate">ส่ง true ถ้าต้องการ insert</param>
        public void DoSave(EndorseStamp endorseStamp, bool isCreate)
        {
            if (isCreate)
            {
                _add(endorseStamp);
            }

            _doBeforePost(endorseStamp, isCreate);
            _validate(endorseStamp, isCreate);

            _saveCheckCode(endorseStamp);
        }

        /// <summary>
        /// prepare data and delete from ef
        /// </summary>
        /// <param name="endorseStamp"></param>
        public void DoDelete(EndorseStamp endorseStamp)
        {
            _delete(endorseStamp);
            _save();
        }

        #endregion
    }
}
