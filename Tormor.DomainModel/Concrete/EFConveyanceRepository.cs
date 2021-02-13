using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NNS.ModelHelpers;
using NNS.GeneralHelpers;
using System.Linq.Expressions;

namespace Tormor.DomainModel
{
    public class EFConveyanceRepository : Tormor.DomainModel.IConveyanceRepository 
    {
        #region Private members....
        private NeoIMOSKEntities entities = new NeoIMOSKEntities();

        /// <summary>
        /// What to do before save any edit or newly created record
        /// </summary>
        /// <param name="conv"></param>
        private void _doBeforePost(Conveyance conv)
        {
            conv.UpdateInfo.LogUpdated(CurrentUserName);
        }

        //
        // Insert / Delete Method
        /// <summary>
        /// สำหรับเพิ่ม record ลง ef
        /// </summary>
        /// <param name="conv"></param>
        private void _add(Conveyance conv)
        {
            entities.Conveyances.AddObject(conv);
        }

        /// <summary>
        /// สำหรับลบ record ออกจาก ef รวมทั้งลบลูกที่เกี่ยวข้อง (ถ้ามี)
        /// </summary>
        /// <param name="conv"></param>
        private void _delete(Conveyance conv)
        {
            // ถ้ามี ref ตัวลูก ให้ loop ลบ ref ก่อน...
            //entities.Conveyances.DeleteObject(conv);
            conv.IsCancel = true;
        }

        /// <summary>
        /// ตรวจสอบว่าข้อมูลที่กรอกเข้ามาถูกต้องหรือไม่ 
        /// โดยเป็นส่วนของ logic ที่ไม่สามารถระบุใน metadata ไม่ได้ และไม่ได้ระบุเอาไว้ใน constraint ของ database
        /// </summary>
        /// <param name="conv"></param>
        /// <param name="isCreate"></param>
        private void _validate(Conveyance conv, bool isCreate)
        {
            var errors = new RulesException<Conveyance>();

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
                if (exMessage.Contains("IX_Conveyances_Name"))
                {
                    var clash = new RulesException<Conveyance>();
                    clash.ErrorFor(x => x.Name, "Sorry, duplicate name not allowed");
                    throw clash;
                }
                throw; // Rethrow any other exceptions to avoid interfering with them 
            }
        }

        /// <summary>
        /// เรียกการบันทึกข้อมูล โดยตรวจสอบว่า Code ซ้ำหรือไม่
        /// </summary>
        /// <param name="conv"></param>
        /// <param name="numRecursive">จำนวนที่ให้วน loop ปรกติไม่ต้องใส่มา</param>
        private void _saveCheckCode(Conveyance conv, int numRecursive = 5)
        {
            try
            {
                _save();
            }
            catch (ApplicationException ex)
            {
                var clash = new RulesException<Conveyance>();
                clash.ErrorFor(x => x.Name, "Error :" + ex.ExMessage());
                throw clash;
            }
        }

        #endregion

        #region Public (Interface)...
        public string CurrentUserName { get; set; }
        //
        // Query Method

        /// <summary>
        /// เรียกข้อมูล conv ทั้งหมด
        /// </summary>
        /// <returns>conv table</returns>
        public IQueryable<Conveyance> FindAll()
        {
            return entities.Conveyances.Where(p => !p.IsCancel);
        }

        /// <summary>
        /// ค้นหา Conveyance จากเงื่อนไขที่ส่งมาซึ่งอาจจะเป็น passcard.docno หรือ fullname ก็ได้
        /// </summary>
        /// <param name="ownerCond"></param>
        /// <returns></returns>
        public IQueryable<Conveyance> Search(string ownerCond, string nameCond)
        {
            ownerCond = ownerCond.ToLower();
            var lNameCond = nameCond.Replace(" ", "");

            var result = entities.Conveyances.Where(c => !c.IsCancel);

            if (!string.IsNullOrEmpty(ownerCond))
                result = result.Where(c => c.OwnerName.ToLower().Contains(ownerCond));

            if (!string.IsNullOrEmpty(lNameCond))
                result = result.Where(c => c.Name.Replace(" ","").ToLower().Contains(lNameCond));

            return result;
        }

        /// <summary>
        /// เรียกข้อมูล conv ตาม id ที่ระบุ
        /// </summary>
        /// <param name="id">conv's id</param>
        /// <returns>conv entity</returns>
        public Conveyance GetOne(int id)
        {
            return this.FindAll().FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// What to do when create new conv
        /// </summary>
        /// <param name="conv"></param>
        public void DoNewRecord(Conveyance conv)
        {
            conv.UpdateInfo.LogAdded(CurrentUserName);
        }

        /// <summary>
        /// prepare data and save to ef, ใช้ทั้ง update และ insert
        /// </summary>
        /// <param name="conv"></param>
        /// <param name="isCreate">ส่ง true ถ้าต้องการ insert</param>
        public void DoSave(Conveyance conv, bool isCreate)
        {
            if (isCreate)
                _add(conv);

            _doBeforePost(conv);
            _validate(conv, isCreate);
            _saveCheckCode(conv);
        }

        /// <summary>
        /// prepare data and delete from ef
        /// </summary>
        /// <param name="conv"></param>
        public void DoDelete(Conveyance conv)
        {
            _delete(conv);
            _save();
        }
        #endregion
    }
}
