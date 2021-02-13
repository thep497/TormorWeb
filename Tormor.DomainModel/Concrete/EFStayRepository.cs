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
    public class EFStayRepository : Tormor.DomainModel.IStayRepository
    {
        #region Private members....
        private NeoIMOSKEntities entities = new NeoIMOSKEntities();

        /// <summary>
        /// What to do before save any edit or newly created record
        /// </summary>
        /// <param name="stay"></param>
        private void _doBeforePost(Staying90Day stay, bool isCreate)
        {
            stay.ClearNullDate();
            stay.UpdateInfo.LogUpdated(CurrentUserName);
            if (stay.Alien != null)
                stay.Alien.UpdateInfo.LogUpdated(CurrentUserName);

            if (isCreate)
            {
                if (stay.Alien != null)
                {
                    stay.Alien.DataInType = 4; //stay
                    stay.CurrentAddress.CopyFrom(stay.Alien.CurrentAddress);
                    stay.PassportCard.CopyFrom(stay.Alien.PassportCard);
                }
            }
        }

        //
        // Insert / Delete Method
        /// <summary>
        /// สำหรับเพิ่ม record ลง ef
        /// </summary>
        /// <param name="stay"></param>
        private void _add(Staying90Day stay)
        {
            entities.Staying90Days.AddObject(stay);
        }

        /// <summary>
        /// สำหรับลบ record ออกจาก ef รวมทั้งลบลูกที่เกี่ยวข้อง (ถ้ามี)
        /// </summary>
        /// <param name="stay"></param>
        private void _delete(Staying90Day stay)
        {
            // ถ้ามี ref ตัวลูก ให้ loop ลบ ref ก่อน...
            //entities.Staying90Days.DeleteObject(stay);
            stay.IsCancel = true;
        }

        private void _checkCode(Staying90Day stay, bool isCreate)
        {
            int stayId = 0;
            if (!isCreate) stayId = stay.Id;
            var oldCode = (from p in entities.Staying90Days
                           where (p.Id != stayId) &&
                                 (p.Code == stay.Code) &&
                                 (p.RequestDate.Year == stay.RequestDate.Year) &&
                                 (!p.IsCancel)
                           select p.Id).Count();
            if (oldCode > 0)
                throw new ApplicationException("dupcode");

        }
        /// <summary>
        /// ตรวจสอบว่าข้อมูลที่กรอกเข้ามาถูกต้องหรือไม่ 
        /// โดยเป็นส่วนของ logic ที่ไม่สามารถระบุใน metadata ไม่ได้ และไม่ได้ระบุเอาไว้ใน constraint ของ database
        /// </summary>
        /// <param name="stay"></param>
        /// <param name="isCreate"></param>
        private void _validate(Staying90Day stay, bool isCreate)
        {
            var errors = new RulesException<Staying90Day>();

            //ไม่ต้องเช็ค code ตรงนี้ ปล่อยให้ saveCheckCode จัดการ
            //if (stay.Code != "")
            //{
            //    //_checkCode(stay,isCreate);
            //}

            if (string.IsNullOrWhiteSpace(stay.ArrivalCard.DocNo))
                errors.ErrorFor(x => x.ArrivalCard, "กรุณากรอกเลขที่บัตรขาเข้าด้วยค่ะ");

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
                if (exMessage.Contains("IX_Staying90Days_Code"))
                {
                    var clash = new RulesException<Staying90Day>();
                    clash.ErrorFor(x => x.Code, "Sorry, problem with index on Code");
                    throw clash;
                }
                throw; // Rethrow any other exceptions to avoid interfering with them 
            }
        }

        /// <summary>
        /// เรียกการบันทึกข้อมูล โดยตรวจสอบว่า Code ซ้ำหรือไม่
        /// </summary>
        /// <param name="stay"></param>
        /// <param name="numRecursive">จำนวนที่ให้วน loop ปรกติไม่ต้องใส่มา</param>
        private void _saveCheckCode(Staying90Day stay, int numRecursive = 5)
        {
            try
            {
                _checkCode(stay, stay.Id == 0);
                _save();
            }
            catch (Exception ex)
            {
                //ถ้าซ้ำ ก็หา code ใหม่...
                if (ex.ExMessage() == "dupcode")
                {
                    if (numRecursive > 0)
                    {
                        stay.Code = GetNewCode();
                        _saveCheckCode(stay, --numRecursive); //recursive
                    }
                    else
                    {
                        var clash = new RulesException<Staying90Day>();
                        clash.ErrorFor(x => x.Code, "Sorry, error when generating the code.");
                        throw clash;
                    }
                }
                else
                    throw;
            }
        }
        #endregion

        #region Public (Interface)...
        public string CurrentUserName { get; set; }
        //
        // Query Method

        /// <summary>
        /// เรียกข้อมูล stay ทั้งหมด
        /// </summary>
        /// <returns>stay table</returns>
        public IQueryable<Staying90Day> FindAll(DateTime? dtpFromDate=null, DateTime? dtpToDate=null)
        {
            var result = from v in entities.Staying90Days
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
        /// เรียกข้อมูล stay ตาม id ที่ระบุ
        /// </summary>
        /// <param name="id">stay's id</param>
        /// <returns>stay entity</returns>
        public Staying90Day GetOne(int id)
        {
            return this.FindAll().FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// เรียกข้อมูล stay ตาม id ที่ระบุ
        /// </summary>
        /// <param name="id">stay's id</param>
        /// <returns>stay entity</returns>
        public Staying90Day GetOneByCode(string code, DateTime stayYear, int id = -1)
        {
            return this.FindAll()
                .FirstOrDefault(p => (p.Code == code) && 
                                     (p.RequestDate.Year == stayYear.Year) &&
                                     (p.Id != id));
        }

        public string GetNewCode()
        {
            string result = (from p in entities.Staying90Days
                             select p.Code).DefaultIfEmpty("000000").Max();
            return (Convert.ToInt32(result) + 1).ToString("D6");
        }

        /// <summary>
        /// What to do when create new stay
        /// </summary>
        /// <param name="stay"></param>
        public void DoNewRecord(Staying90Day stay)
        {
            //ทำใน controller แล้ว stay.Code = GetNewCode();
            stay.UpdateInfo.LogAdded(CurrentUserName);
            if (stay.Alien != null)
                stay.Alien.UpdateInfo.LogAdded(CurrentUserName);

            stay.IsCancel = false;
        }

        /// <summary>
        /// prepare data and save to ef, ใช้ทั้ง update และ insert
        /// </summary>
        /// <param name="stay"></param>
        /// <param name="isCreate">ส่ง true ถ้าต้องการ insert</param>
        public void DoSave(Staying90Day stay, bool isCreate)
        {
            if (isCreate)
            {
                //ถ้า Insert แต่ Alien มีค่าอยู่แล้ว ต้องเอาออกก่อน ไม่งั้นจะ add Alien เข้าไปด้วย
                if (stay.AlienId != 0)
                {
                    var updatedAlien = stay.Alien;
                    stay.Alien = null;
                    //ให้ไป update ที่ alien โดยตรง
                    var oldAlien = entities.Aliens.Single(a => a.Id == stay.AlienId);
                    entities.Aliens.Attach(oldAlien);
                    updatedAlien.Photo.CopyFrom(oldAlien.Photo); //ต้อง copy ถ่ายค่าของรูปภาพมาด้วย เพราะไม่มีข้อมูลเก็บในหน้าจอ
                    entities.Aliens.ApplyCurrentValues(updatedAlien);
                }

                _add(stay);
            }

            _doBeforePost(stay, isCreate);
            _validate(stay, isCreate);

            _saveCheckCode(stay);
        }

        /// <summary>
        /// prepare data and delete from ef
        /// </summary>
        /// <param name="stay"></param>
        public void DoDelete(Staying90Day stay)
        {
            _delete(stay);
            _save();
        }
        #endregion
    }
}
