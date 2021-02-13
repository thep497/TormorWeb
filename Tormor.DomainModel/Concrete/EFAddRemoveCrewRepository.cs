// *******************************************************************
//1. ชื่อโปรแกรม 	: NeoSKIMO
//2. ชื่อระบบ 		: DomainModel
//3. ชื่อ Unit 	: IAddRemoveCrewRepository
//4. ผู้เขียนโปรแกรม	: อมรเทพ
//5. วันที่สร้าง	    : 201012xx
//6. จุดประสงค์ของ Unit นี้ : EF Class สำหรับจัดการกับข้อมูลของ AddRemoveCrew
// *******************************************************************
// แก้ไขครั้งที่ : 1
// ประวัติการแก้ไข
// ครั้งที่ 0 : เริ่มต้นสร้าง unit ใหม่
// ครั้งที่ 1 : //th20110409 เพิ่มคนลดลงในเรือออก PD18-540102 Req 8.1/8.2
// *******************************************************************
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
    public class EFAddRemoveCrewRepository : Tormor.DomainModel.IAddRemoveCrewRepository
    {
        #region Private members....
        private readonly NeoIMOSKEntities entities = new NeoIMOSKEntities();

        /// <summary>
        /// What to do before save any edit or newly created record
        /// </summary>
        /// <param name="addRemoveCrew"></param>
        private void _doBeforePost(AddRemoveCrew addRemoveCrew, bool isCreate, int addRemoveType)
        {
            addRemoveCrew.ClearNullDate();
            addRemoveCrew.UpdateInfo.LogUpdated(CurrentUserName);
            if (addRemoveCrew.Alien != null)
                addRemoveCrew.Alien.UpdateInfo.LogUpdated(CurrentUserName);

            if (isCreate)
            {
                if (addRemoveCrew.Alien != null)
                {
                    addRemoveCrew.Alien.DataInType = 5; // ลูกเรือ มากับเรือ
                }
            }
            //Update ConveyanceInOut ตรงนี้ 
            ConveyanceInOut convInOut;
            if (addRemoveCrew.AddRemoveType == ModelConst.ADDREMOVETYPE_ADD)
            {
                var convNameCond = addRemoveCrew.OutDetail.InWay.Replace(" ", "").ToLower();
                var convInOutDate = addRemoveCrew.OutDetail.InDate;
                convInOut = (from a in entities.ConveyanceInOuts
                             where (a.Conveyance.Name.ToLower().Replace(" ", "") == convNameCond) &&
                                   (a.InOutDate == convInOutDate)
                             select a).FirstOrDefault();
            }
            else
            {
                var convNameCond = addRemoveCrew.InDetail.InWay.Replace(" ", "").ToLower();
                var convInOutDate = addRemoveCrew.InDetail.InDate;
                convInOut = (from a in entities.ConveyanceInOuts
                             where (a.Conveyance.Name.ToLower().Replace(" ", "") == convNameCond) &&
                                   (a.InOutDate == convInOutDate)
                             select a).FirstOrDefault();
            }
            if (convInOut != null)
                addRemoveCrew.ConveyanceInOut = convInOut;
        }

        //
        // Insert / Delete Method
        /// <summary>
        /// สำหรับเพิ่ม record ลง ef
        /// </summary>
        /// <param name="addRemoveCrew"></param>
        private void _add(AddRemoveCrew addRemoveCrew)
        {
            entities.AddRemoveCrews.AddObject(addRemoveCrew);
        }

        /// <summary>
        /// สำหรับลบ record ออกจาก ef รวมทั้งลบลูกที่เกี่ยวข้อง (ถ้ามี)
        /// </summary>
        /// <param name="addRemoveCrew"></param>
        private void _delete(AddRemoveCrew addRemoveCrew)
        {
            // ถ้ามี ref ตัวลูก ให้ loop ลบ ref ก่อน...
            //entities.AddRemoveCrews.DeleteObject(addRemoveCrew);
            addRemoveCrew.IsCancel = true;
        }

        /// <summary>
        /// ตรวจสอบว่าข้อมูลที่กรอกเข้ามาถูกต้องหรือไม่ 
        /// โดยเป็นส่วนของ logic ที่ไม่สามารถระบุใน metadata ไม่ได้ และไม่ได้ระบุเอาไว้ใน constraint ของ database
        /// </summary>
        /// <param name="addRemoveCrew"></param>
        /// <param name="isCreate"></param>
        private void _validate(AddRemoveCrew addRemoveCrew, bool isCreate, int addRemoveType)
        {
            var errors = new RulesException<AddRemoveCrew>();

            if (addRemoveCrew.Code != "")
            {
                int addRemoveCrewId = 0;
                if (!isCreate) addRemoveCrewId = addRemoveCrew.Id;
                //ถ้ามี subcode
                if (addRemoveCrew.SubCode != "")
                {
                    var oldCode = (from p in entities.AddRemoveCrews
                                   where (p.Id != addRemoveCrewId) &&
                                         (p.Code == addRemoveCrew.Code) &&
                                         (p.SubCode == addRemoveCrew.SubCode) &&
                                         (p.AddRemoveType == addRemoveType) &&
                                         (!p.IsCancel)
                                   select p.Id).Count();
                    if (oldCode > 0)
                        errors.ErrorFor(x => x, "ลำดับที่และลำดับย่อยที่กรอก ซ้ำกับรายการอื่นในปีเดียวกัน");
                }
                //ถ้าไม่มี ให้เช็คกับ code โดยตรง
                else
                {
                    var oldCode = (from p in entities.AddRemoveCrews
                                   where (p.Id != addRemoveCrewId) &&
                                         (p.Code == addRemoveCrew.Code) &&
                                         (p.AddRemoveType == addRemoveType) &&
                                         (!p.IsCancel)
                                   select p.Id).Count();
                    if (oldCode > 0)
                        errors.ErrorFor(x => x, "ลำดับที่ ที่กรอก ซ้ำกับรายการอื่นในปีเดียวกัน");
                }
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
                if (exMessage.Contains("IX_AddRemoveCrews_Code"))
                {
                    var clash = new RulesException<AddRemoveCrew>();
                    clash.ErrorFor(x => x.Code, "Sorry, problem with index on Code");
                    throw clash;
                }
                throw; // Rethrow any other exceptions to avoid interfering with them 
            }
        }

        /// <summary>
        /// เรียกการบันทึกข้อมูล โดยตรวจสอบว่า Code ซ้ำหรือไม่
        /// </summary>
        /// <param name="addRemoveCrew"></param>
        /// <param name="numRecursive">จำนวนที่ให้วน loop ปรกติไม่ต้องใส่มา</param>
        private void _saveCheckCode(AddRemoveCrew addRemoveCrew, int numRecursive = 5)
        {
            try
            {
                _save();
            }
            catch (Exception ex)
            {
                var clash = new RulesException<AddRemoveCrew>();
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
        /// เรียกข้อมูล addRemoveCrew ทั้งหมด
        /// </summary>
        /// <returns>addRemoveCrew table</returns>
        public IQueryable<AddRemoveCrew> FindAll(int? addRemoveType=null, DateTime? dtpFromDate = null, DateTime? dtpToDate = null)
        {
            var result = from v in entities.AddRemoveCrews
                         select v;

            result = result.Where(p => !p.IsCancel);

            if (addRemoveType != null)
            {
                int art = addRemoveType ?? 1;
                result = result.Where(p => (p.AddRemoveType == art));
            }
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

        public IQueryable<AddRemoveCrew> SearchByInOutDate(int addRemoveType, DateTime? dtpFromDate = null, DateTime? dtpToDate = null)
        {
            var result = from v in entities.AddRemoveCrews
                         select v;

            result = result.Where(p => !p.IsCancel);
            result = result.Where(p => (p.AddRemoveType == addRemoveType));
            //ถ้า addRemoveType=2 (ลดคน) ให้หาจาก InDetail
            if (addRemoveType == ModelConst.ADDREMOVETYPE_RMV)
            {
                if (!dtpFromDate.IsNull())
                {
                    result = result.Where(p => p.InDetail.InDate >= dtpFromDate);
                }
                if (!dtpToDate.IsNull())
                {
                    result = result.Where(p => p.InDetail.InDate <= dtpToDate);
                }
            }
            else
            {
                if (!dtpFromDate.IsNull())
                {
                    result = result.Where(p => p.OutDetail.InDate >= dtpFromDate);
                }
                if (!dtpToDate.IsNull())
                {
                    result = result.Where(p => p.OutDetail.InDate <= dtpToDate);
                }
            }
            return result;
        }

        public IQueryable<AddRemoveCrew> SearchByConveyanceLastIn(string convNameCond, DateTime convOutDate)
        {
            //หาเรือเข้าก่อนว่าเข้าเมื่อไร
            DateTime? convInDate = 
                entities.ConveyanceInOuts
                        .Where(c => !c.IsCancel &&
                                     c.InOutType == ModelConst.CONVINOUT_IN &&
                                     c.InOutDate <= convOutDate &&
                                     c.Conveyance.Name.ToLower().Replace(" ", "") == convNameCond
                              )
                        .Max(c => (DateTime?)c.InOutDate); //ใช้ DateTime? กรณีที่ไม่มี record จะไม่ error

            if (convInDate == null)
                return new List<AddRemoveCrew>().AsQueryable();

            //ถ้ามีเรือเข้า ให้หาข้อมูลคนลดของเรือลำนั้น
            var result = entities.AddRemoveCrews
                                 .Where(p => !p.IsCancel &&
                                       (p.AddRemoveType == ModelConst.ADDREMOVETYPE_RMV) && //เรือเข้า มีคนลด
                                       (p.InDetail.InDate == convInDate) && //ตรงนี้ convInDate จะไม่ null แน่นอน
                                       (p.InDetail.InWay.ToLower().Replace(" ", "") == convNameCond)
                                 );

            return result;
        }

        /// <summary>
        /// เรียกข้อมูล addRemoveCrew ตาม id ที่ระบุ
        /// </summary>
        /// <param name="id">addRemoveCrew's id</param>
        /// <returns>addRemoveCrew entity</returns>
        public AddRemoveCrew GetOne(int id,int? addRemoveType)
        {
            return this.FindAll(addRemoveType).FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// เรียกข้อมูล addRemoveCrew ตาม id ที่ระบุ
        /// </summary>
        /// <param name="id">addRemoveCrew's id</param>
        /// <returns>addRemoveCrew entity</returns>
        public AddRemoveCrew GetOneByCode(string code, DateTime addRemoveCrewYear,int addRemoveType, int id = -1)
        {
            return this.FindAll(addRemoveType)
                .FirstOrDefault(p => (p.Code == code) &&
                                     (p.RequestDate.Year == addRemoveCrewYear.Year) &&
                                     (p.Id != id));
        }
        #endregion

        /// <summary>
        /// What to do when create new addRemoveCrew
        /// </summary>
        /// <param name="addRemoveCrew"></param>
        public void DoNewRecord(AddRemoveCrew addRemoveCrew, int addRemoveType)
        {
            //addRemoveCrew.Code = _getNewProductCode();
            addRemoveCrew.AddRemoveType = addRemoveType;
            addRemoveCrew.UpdateInfo.LogAdded(CurrentUserName);
            if (addRemoveCrew.Alien != null)
                addRemoveCrew.Alien.UpdateInfo.LogAdded(CurrentUserName);

            addRemoveCrew.IsCancel = false;
        }

        /// <summary>
        /// prepare data and save to ef, ใช้ทั้ง update และ insert
        /// </summary>
        /// <param name="addRemoveCrew"></param>
        /// <param name="isCreate">ส่ง true ถ้าต้องการ insert</param>
        public void DoSave(AddRemoveCrew addRemoveCrew, bool isCreate, int addRemoveType)
        {
            if (isCreate)
            {
                //ถ้า Insert แต่ Alien มีค่าอยู่แล้ว ต้องเอาออกก่อน ไม่งั้นจะ add Alien เข้าไปด้วย
                if (addRemoveCrew.AlienId != 0)
                {
                    var updatedAlien = addRemoveCrew.Alien;
                    addRemoveCrew.Alien = null;
                    //ให้ไป update ที่ alien โดยตรง
                    var oldAlien = entities.Aliens.Single(a => a.Id == addRemoveCrew.AlienId);
                    entities.Aliens.Attach(oldAlien);
                    updatedAlien.Photo.CopyFrom(oldAlien.Photo); //ต้อง copy ถ่ายค่าของรูปภาพมาด้วย เพราะไม่มีข้อมูลเก็บในหน้าจอ
                    entities.Aliens.ApplyCurrentValues(updatedAlien);
                }

                _add(addRemoveCrew);
            }

            _doBeforePost(addRemoveCrew, isCreate, addRemoveType);
            _validate(addRemoveCrew, isCreate, addRemoveType);

            _saveCheckCode(addRemoveCrew);
        }

        /// <summary>
        /// prepare data and delete from ef
        /// </summary>
        /// <param name="addRemoveCrew"></param>
        public void DoDelete(AddRemoveCrew addRemoveCrew)
        {
            _delete(addRemoveCrew);
            _save();
        }

        #endregion
    }
}