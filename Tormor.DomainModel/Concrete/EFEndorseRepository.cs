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
    public class EFEndorseRepository : Tormor.DomainModel.IEndorseRepository
    {
        #region Private members....
        private NeoIMOSKEntities entities = new NeoIMOSKEntities();

        /// <summary>
        /// What to do before save any edit or newly created record
        /// </summary>
        /// <param name="endorse"></param>
        private void _doBeforePost(Endorse endorse, bool isCreate)
        {
            endorse.ClearNullDate();
            endorse.UpdateInfo.LogUpdated(CurrentUserName);
            if (endorse.Alien != null)
                endorse.Alien.UpdateInfo.LogUpdated(CurrentUserName);

            if (isCreate)
            {
                if (endorse.Alien != null)
                {
                    endorse.Alien.DataInType = 3; //endorse
                    endorse.PassportCard.CopyFrom(endorse.Alien.PassportCard);
                    endorse.HabitatCard.CopyFrom(endorse.Alien.HabitatCard);
                    endorse.CurrentAddress.CopyFrom(endorse.Alien.CurrentAddress);
                }
            }
        }

        //
        // Insert / Delete Method
        /// <summary>
        /// สำหรับเพิ่ม record ลง ef
        /// </summary>
        /// <param name="endorse"></param>
        private void _add(Endorse endorse)
        {
            entities.Endorses.AddObject(endorse);
        }

        /// <summary>
        /// สำหรับลบ record ออกจาก ef รวมทั้งลบลูกที่เกี่ยวข้อง (ถ้ามี)
        /// </summary>
        /// <param name="endorse"></param>
        private void _delete(Endorse endorse)
        {
            // ถ้ามี ref ตัวลูก ให้ loop ลบ ref ก่อน...
            foreach (var eStamp in endorse.EndorseStamps)
            {
                eStamp.IsCancel = true;
            }
            //entities.Endorses.DeleteObject(endorse);
            endorse.IsCancel = true;
        }

        /// <summary>
        /// ตรวจสอบว่าข้อมูลที่กรอกเข้ามาถูกต้องหรือไม่ 
        /// โดยเป็นส่วนของ logic ที่ไม่สามารถระบุใน metadata ไม่ได้ และไม่ได้ระบุเอาไว้ใน constraint ของ database
        /// </summary>
        /// <param name="endorse"></param>
        /// <param name="isCreate"></param>
        private void _validate(Endorse endorse, bool isCreate)
        {
            var errors = new RulesException<Endorse>();

            if (endorse.Code != "")
            {
                int endorseId = 0;
                if (!isCreate) endorseId = endorse.Id;
                var oldCode = (from p in entities.Endorses
                               where (p.Id != endorseId) &&
                                     (p.Code == endorse.Code) &&
                                     (p.RequestDate.Year == endorse.RequestDate.Year) &&
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
                if (exMessage.Contains("IX_Endorses_Code"))
                {
                    var clash = new RulesException<Endorse>();
                    clash.ErrorFor(x => x.Code, "Sorry, problem with index on Code");
                    throw clash;
                }
                throw; // Rethrow any other exceptions to avoid interfering with them 
            }
        }

        /// <summary>
        /// เรียกการบันทึกข้อมูล โดยตรวจสอบว่า Code ซ้ำหรือไม่
        /// </summary>
        /// <param name="endorse"></param>
        /// <param name="numRecursive">จำนวนที่ให้วน loop ปรกติไม่ต้องใส่มา</param>
        private void _saveCheckCode(Endorse endorse, int numRecursive = 5)
        {
            try
            {
                _save();
            }
            catch (Exception ex)
            {
                var clash = new RulesException<Endorse>();
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
        /// เรียกข้อมูล endorse ทั้งหมด
        /// </summary>
        /// <returns>endorse table</returns>
        public IQueryable<Endorse> FindAll(DateTime? dtpFromDate = null, DateTime? dtpToDate = null)
        {
            var result = from v in entities.Endorses
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
        /// เรียกข้อมูล endorse ตาม id ที่ระบุ
        /// </summary>
        /// <param name="id">endorse's id</param>
        /// <returns>endorse entity</returns>
        public Endorse GetOne(int id)
        {
            return this.FindAll().FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// เรียกข้อมูล endorse ตาม id ที่ระบุ
        /// </summary>
        /// <param name="id">endorse's id</param>
        /// <returns>endorse entity</returns>
        public Endorse GetOneByCode(string code, DateTime endorseYear, int id = -1)
        {
            return this.FindAll()
                .FirstOrDefault(p => (p.Code == code) &&
                                     (p.RequestDate.Year == endorseYear.Year) &&
                                     (p.Id != id));
        }
        #endregion

        /// <summary>
        /// What to do when create new endorse
        /// </summary>
        /// <param name="endorse"></param>
        public void DoNewRecord(Endorse endorse)
        {
            //endorse.Code = _getNewProductCode();
            endorse.UpdateInfo.LogAdded(CurrentUserName);
            if (endorse.Alien != null)
                endorse.Alien.UpdateInfo.LogAdded(CurrentUserName);

            endorse.IsCancel = false;
        }

        /// <summary>
        /// prepare data and save to ef, ใช้ทั้ง update และ insert
        /// </summary>
        /// <param name="endorse"></param>
        /// <param name="isCreate">ส่ง true ถ้าต้องการ insert</param>
        public void DoSave(Endorse endorse, bool isCreate)
        {
            if (isCreate)
            {
                //ถ้า Insert แต่ Alien มีค่าอยู่แล้ว ต้องเอาออกก่อน ไม่งั้นจะ add Alien เข้าไปด้วย
                if (endorse.AlienId != 0)
                {
                    var updatedAlien = endorse.Alien;
                    endorse.Alien = null;
                    //ให้ไป update ที่ alien โดยตรง
                    var oldAlien = entities.Aliens.Single(a => a.Id == endorse.AlienId);
                    entities.Aliens.Attach(oldAlien);
                    updatedAlien.Photo.CopyFrom(oldAlien.Photo); //ต้อง copy ถ่ายค่าของรูปภาพมาด้วย เพราะไม่มีข้อมูลเก็บในหน้าจอ
                    entities.Aliens.ApplyCurrentValues(updatedAlien);
                }

                _add(endorse);
            }

            _doBeforePost(endorse, isCreate);
            _validate(endorse, isCreate);

            _saveCheckCode(endorse);
        }

        /// <summary>
        /// prepare data and delete from ef
        /// </summary>
        /// <param name="endorse"></param>
        public void DoDelete(Endorse endorse)
        {
            _delete(endorse);
            _save();
        }

        #endregion
    }
}
