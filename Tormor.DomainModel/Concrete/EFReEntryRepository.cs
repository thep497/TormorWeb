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
    public class EFReEntryRepository : Tormor.DomainModel.IReEntryRepository
    {
        #region Private members....
        private NeoIMOSKEntities entities = new NeoIMOSKEntities();

        /// <summary>
        /// What to do before save any edit or newly created record
        /// </summary>
        /// <param name="reentry"></param>
        private void _doBeforePost(ReEntry reentry, bool isCreate)
        {
            reentry.ClearNullDate();
            reentry.UpdateInfo.LogUpdated(CurrentUserName);
            if (reentry.Alien != null)
                reentry.Alien.UpdateInfo.LogUpdated(CurrentUserName);

            if (isCreate)
            {
                if (reentry.Alien != null)
                {
                    reentry.Alien.DataInType = 2; //reentry
                    reentry.PassportCard.CopyFrom(reentry.Alien.PassportCard);
                }
            }
        }

        //
        // Insert / Delete Method
        /// <summary>
        /// สำหรับเพิ่ม record ลง ef
        /// </summary>
        /// <param name="reentry"></param>
        private void _add(ReEntry reentry)
        {
            entities.ReEntrys.AddObject(reentry);
        }

        /// <summary>
        /// สำหรับลบ record ออกจาก ef รวมทั้งลบลูกที่เกี่ยวข้อง (ถ้ามี)
        /// </summary>
        /// <param name="reentry"></param>
        private void _delete(ReEntry reentry)
        {
            // ถ้ามี ref ตัวลูก ให้ loop ลบ ref ก่อน...
            //entities.ReEntrys.DeleteObject(reentry);
            reentry.IsCancel = true;
        }

        /// <summary>
        /// ตรวจสอบว่าข้อมูลที่กรอกเข้ามาถูกต้องหรือไม่ 
        /// โดยเป็นส่วนของ logic ที่ไม่สามารถระบุใน metadata ไม่ได้ และไม่ได้ระบุเอาไว้ใน constraint ของ database
        /// </summary>
        /// <param name="reentry"></param>
        /// <param name="isCreate"></param>
        private void _validate(ReEntry reentry, bool isCreate)
        {
            var errors = new RulesException<ReEntry>();

            if (reentry.Code != "")
            {
                int reentryId = 0;
                if (!isCreate) reentryId = reentry.Id;
                var oldCode = (from p in entities.ReEntrys
                               where (p.Id != reentryId) &&
                                     (p.Code == reentry.Code) &&
                                     (p.RequestDate.Year == reentry.RequestDate.Year) &&
                                     (!p.IsCancel)
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
            catch (Exception ex)
            {
                //ตรวจสอบ error จาก sql server
                string exMessage = ex.ExMessage();

                //ไม่มีการเช็คอะไร เพียงแค่เขียนเป็นตัวอย่างเอาไว้
                if (exMessage.Contains("IX_ReEntrys_Code"))
                {
                    var clash = new RulesException<ReEntry>();
                    clash.ErrorFor(x => x.Code, "Sorry, problem with index on Code");
                    throw clash;
                }
                throw; // Rethrow any other exceptions to avoid interfering with them 
            }
        }

        /// <summary>
        /// เรียกการบันทึกข้อมูล โดยตรวจสอบว่า Code ซ้ำหรือไม่
        /// </summary>
        /// <param name="reentry"></param>
        /// <param name="numRecursive">จำนวนที่ให้วน loop ปรกติไม่ต้องใส่มา</param>
        private void _saveCheckCode(ReEntry reentry, int numRecursive = 5)
        {
            try
            {
                _save();
            }
            catch (Exception ex)
            {
                var clash = new RulesException<ReEntry>();
                clash.ErrorFor(x => x.Code, "Error :" + ex.ExMessage());
                throw clash;
            }
        }
        #endregion

        #region Public (Interface)...
        public string CurrentUserName { get; set; }
        //
        // Query Method

        /// <summary>
        /// เรียกข้อมูล reentry ทั้งหมด
        /// </summary>
        /// <returns>reentry table</returns>
        public IQueryable<ReEntry> FindAll(DateTime? dtpFromDate=null, DateTime? dtpToDate=null)
        {
            var result = from v in entities.ReEntrys
                         select v;

            result = result.Where(p => !p.IsCancel);

            if (dtpFromDate.IsNull() && dtpToDate.IsNull())
            {
                //do nothing...
            }
            else if ((dtpFromDate != null) && (dtpToDate != null))
            {
                result = result.Where(p => p.RequestDate >= dtpFromDate && p.RequestDate <= dtpToDate);
            }
            else
            {
                throw new ApplicationException("Date cannot be null...");
            }
            return result;
        }

        /// <summary>
        /// เรียกข้อมูล reentry ตาม id ที่ระบุ
        /// </summary>
        /// <param name="id">reentry's id</param>
        /// <returns>reentry entity</returns>
        public ReEntry GetOne(int id)
        {
            return this.FindAll().FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// เรียกข้อมูล reentry ตาม id ที่ระบุ
        /// </summary>
        /// <param name="id">reentry's id</param>
        /// <returns>reentry entity</returns>
        public ReEntry GetOneByCode(string code, DateTime reentryYear, int id = -1)
        {
            return this.FindAll()
                .FirstOrDefault(p => (p.Code == code) && 
                                     (p.RequestDate.Year == reentryYear.Year) &&
                                     (p.Id != id));
        }

        /// <summary>
        /// What to do when create new reentry
        /// </summary>
        /// <param name="reentry"></param>
        public void DoNewRecord(ReEntry reentry)
        {
            //reentry.Code = _getNewProductCode();
            reentry.UpdateInfo.LogAdded(CurrentUserName);
            if (reentry.Alien != null)
                reentry.Alien.UpdateInfo.LogAdded(CurrentUserName);
            
            reentry.IsCancel = false;
        }

        /// <summary>
        /// prepare data and save to ef, ใช้ทั้ง update และ insert
        /// </summary>
        /// <param name="reentry"></param>
        /// <param name="isCreate">ส่ง true ถ้าต้องการ insert</param>
        public void DoSave(ReEntry reentry, bool isCreate)
        {
            if (isCreate)
            {
                //ถ้า Insert แต่ Alien มีค่าอยู่แล้ว ต้องเอาออกก่อน ไม่งั้นจะ add Alien เข้าไปด้วย
                if (reentry.AlienId != 0) 
                {
                    var updatedAlien = reentry.Alien;
                    reentry.Alien = null;
                    //ให้ไป update ที่ alien โดยตรง
                    var oldAlien = entities.Aliens.Single(a => a.Id == reentry.AlienId);
                    entities.Aliens.Attach(oldAlien);
                    updatedAlien.Photo.CopyFrom(oldAlien.Photo); //ต้อง copy ถ่ายค่าของรูปภาพมาด้วย เพราะไม่มีข้อมูลเก็บในหน้าจอ
                    entities.Aliens.ApplyCurrentValues(updatedAlien);
                }

                _add(reentry);
            }

            _doBeforePost(reentry, isCreate);
            _validate(reentry, isCreate);

            _saveCheckCode(reentry);
        }

        /// <summary>
        /// prepare data and delete from ef
        /// </summary>
        /// <param name="reentry"></param>
        public void DoDelete(ReEntry reentry)
        {
            _delete(reentry);
            _save();
        }
        #endregion
    }
}
