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
    public class EFVisaRepository : Tormor.DomainModel.IVisaRepository
    {
        #region Private members....
        private NeoIMOSKEntities entities = new NeoIMOSKEntities();

        ///// <summary>
        ///// Select Query ของ Alien ที่ยอมให้ add where เข้ามาได้ 
        ///// </summary>
        ///// <param name="whereCls"></param>
        ///// <returns></returns>
        //private IQueryable<VisaDetail> _selectAll(List<Expression<Func<VisaDetail, bool>>> whereCls)
        //{
        //    var selectAllQry = from t in entities.VisaDetails
        //                       select t;

        //    if (whereCls != null)
        //    {
        //        foreach (Expression<Func<VisaDetail, bool>> whereStmt in whereCls)
        //        {
        //            selectAllQry = selectAllQry.Where(whereStmt);
        //        }
        //    }
        //    return selectAllQry;
        //}

        /// <summary>
        /// What to do before save any edit or newly created record
        /// </summary>
        /// <param name="visa"></param>
        private void _doBeforePost(VisaDetail visa, bool isCreate)
        {
            visa.ClearNullDate();
            visa.UpdateInfo.LogUpdated(CurrentUserName);
            if (visa.Alien != null)
                visa.Alien.UpdateInfo.LogUpdated(CurrentUserName);

            if (isCreate)
            {
                if (visa.Alien != null)
                {
                    visa.Alien.DataInType = 1; //visa
                    visa.CurrentAddress.CopyFrom(visa.Alien.CurrentAddress);
                    visa.PassportCard.CopyFrom(visa.Alien.PassportCard);
                }
            }
        }

        //
        // Insert / Delete Method
        /// <summary>
        /// สำหรับเพิ่ม record ลง ef
        /// </summary>
        /// <param name="visa"></param>
        private void _add(VisaDetail visa)
        {
            entities.VisaDetails.AddObject(visa);
        }

        /// <summary>
        /// สำหรับลบ record ออกจาก ef รวมทั้งลบลูกที่เกี่ยวข้อง (ถ้ามี)
        /// </summary>
        /// <param name="visa"></param>
        private void _delete(VisaDetail visa)
        {
            // ถ้ามี ref ตัวลูก ให้ loop ลบ ref ก่อน...
            //entities.VisaDetails.DeleteObject(visa);
            visa.IsCancel = true;
        }

        /// <summary>
        /// ตรวจสอบว่าข้อมูลที่กรอกเข้ามาถูกต้องหรือไม่ 
        /// โดยเป็นส่วนของ logic ที่ไม่สามารถระบุใน metadata ไม่ได้ และไม่ได้ระบุเอาไว้ใน constraint ของ database
        /// </summary>
        /// <param name="visa"></param>
        /// <param name="isCreate"></param>
        private void _validate(VisaDetail visa, bool isCreate)
        {
            var errors = new RulesException<VisaDetail>();

            if (string.IsNullOrWhiteSpace(visa.StayReasonDetail))
                errors.ErrorFor(x => x.Code, "เลือกข้อมูลระยะเวลาให้ถูกต้องด้วยค่ะ");

            if (visa.Code != "")
            {
                int visaId = 0;
                if (!isCreate) visaId = visa.Id;
                var oldCode = (from p in entities.VisaDetails
                               where (p.Id != visaId) &&
                                     (p.Code == visa.Code) &&
                                     (p.RequestDate.Year == visa.RequestDate.Year) &&
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
                if (exMessage.Contains("IX_VisaDetails_Code"))
                {
                    var clash = new RulesException<VisaDetail>();
                    clash.ErrorFor(x => x.Code, "Sorry, problem with index on Code");
                    throw clash;
                }
                throw; // Rethrow any other exceptions to avoid interfering with them 
            }
        }

        /// <summary>
        /// เรียกการบันทึกข้อมูล โดยตรวจสอบว่า Code ซ้ำหรือไม่
        /// </summary>
        /// <param name="visa"></param>
        /// <param name="numRecursive">จำนวนที่ให้วน loop ปรกติไม่ต้องใส่มา</param>
        private void _saveCheckCode(VisaDetail visa, int numRecursive = 5)
        {
            try
            {
                _save();
            }
            catch (Exception ex)
            {
                var clash = new RulesException<VisaDetail>();
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
        /// เรียกข้อมูล visa ทั้งหมด
        /// </summary>
        /// <returns>visa table</returns>
        public IQueryable<VisaDetail> FindAll(DateTime? dtpFromDate=null, DateTime? dtpToDate=null)
        {
            var result = from v in entities.VisaDetails
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


            //List<Expression<Func<VisaDetail, bool>>> whereCls = new List<Expression<Func<VisaDetail, bool>>>();

            //whereCls.Add(p => !p.IsCancel);

            //if (dtpFromDate.IsNull() && dtpToDate.IsNull())
            //{
            //    //do nothing...
            //}
            //else if ((dtpFromDate != null) && (dtpToDate != null))
            //{
            //    whereCls.Add(p => p.RequestDate >= dtpFromDate && p.RequestDate <= dtpToDate);
            //}
            //else
            //{
            //    throw new ApplicationException("Date cannot be null...");
            //}
            //return _selectAll(whereCls);
        }

        /// <summary>
        /// เรียกข้อมูล visa ตาม id ที่ระบุ
        /// </summary>
        /// <param name="id">visa's id</param>
        /// <returns>visa entity</returns>
        public VisaDetail GetOne(int id)
        {
            return this.FindAll().FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// เรียกข้อมูล visa ตาม id ที่ระบุ
        /// </summary>
        /// <param name="id">visa's id</param>
        /// <returns>visa entity</returns>
        public VisaDetail GetOneByCode(string code, DateTime visaYear, int id = -1)
        {
            return this.FindAll()
                .FirstOrDefault(p => (p.Code == code) && 
                                     (p.RequestDate.Year == visaYear.Year) &&
                                     (p.Id != id));
        }

        /// <summary>
        /// What to do when create new visa
        /// </summary>
        /// <param name="visa"></param>
        public void DoNewRecord(VisaDetail visa)
        {
            //visa.Code = _getNewProductCode();
            visa.UpdateInfo.LogAdded(CurrentUserName);
            if (visa.Alien != null)
                visa.Alien.UpdateInfo.LogAdded(CurrentUserName);


            visa.IsCancel = false;
        }

        /// <summary>
        /// prepare data and save to ef, ใช้ทั้ง update และ insert
        /// </summary>
        /// <param name="visa"></param>
        /// <param name="isCreate">ส่ง true ถ้าต้องการ insert</param>
        public void DoSave(VisaDetail visa, bool isCreate)
        {
            if (isCreate)
            {
                //ถ้า Insert แต่ Alien มีค่าอยู่แล้ว ต้องเอาออกก่อน ไม่งั้นจะ add Alien เข้าไปด้วย
                if (visa.AlienId != 0) 
                {
                    var updatedAlien = visa.Alien;
                    visa.Alien = null;
                    //ให้ไป update ที่ alien โดยตรง
                    var oldAlien = entities.Aliens.Single(a => a.Id == visa.AlienId);
                    entities.Aliens.Attach(oldAlien);
                    updatedAlien.Photo.CopyFrom(oldAlien.Photo); //ต้อง copy ถ่ายค่าของรูปภาพมาด้วย เพราะไม่มีข้อมูลเก็บในหน้าจอ
                    entities.Aliens.ApplyCurrentValues(updatedAlien);
                }

                _add(visa);
                //visa.Alien = UpdatedAlien;
            }

            _doBeforePost(visa, isCreate);
            _validate(visa, isCreate);

            _saveCheckCode(visa);
        }

        /// <summary>
        /// prepare data and delete from ef
        /// </summary>
        /// <param name="visa"></param>
        public void DoDelete(VisaDetail visa)
        {
            _delete(visa);
            _save();
        }
        #endregion
    }
}
