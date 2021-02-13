// *******************************************************************
//1. ชื่อโปรแกรม 	: NeoSKIMO
//2. ชื่อระบบ 		: DomainModel
//3. ชื่อ Unit 	: EFCrewRepository
//4. ผู้เขียนโปรแกรม	: อมรเทพ
//5. วันที่สร้าง	    : 201012xx
//6. จุดประสงค์ของ Unit นี้ : Repository จัดการ Table Crew
// *******************************************************************
// แก้ไขครั้งที่ : 1
// ประวัติการแก้ไข
// ครั้งที่ 0 : เริ่มต้นสร้าง unit ใหม่
// ครั้งที่ 1 : //th20110407 แก้การ Save ให้บันทึก field เพิ่ม 2 field (ID/Seaman) PD18-540102 Req 2
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
    public class EFCrewRepository : Tormor.DomainModel.ICrewRepository 
    {
        #region Private members....
        private readonly NeoIMOSKEntities entities = new NeoIMOSKEntities();

        /// <summary>
        /// What to do before save any edit or newly created record
        /// </summary>
        /// <param name="crew"></param>
        private void _doBeforePost(Crew crew, bool isCreate)
        {
            crew.ClearNullDate();
            crew.UpdateInfo.LogUpdated(CurrentUserName);
            if (crew.Alien != null)
                crew.Alien.UpdateInfo.LogUpdated(CurrentUserName);

            if (crew.Alien == null)
                return;

            //ข้างล่างนี้ ประกันได้ว่าค่า Alien ไม่ null
            if (isCreate)
            {
                crew.Alien.DataInType = (crew.IsCrew ?? true) ? 5 : 6; //มากับเรือ - ผู้โดยสาร
            }
            //3 ตัวแปรนี้ ไม่ว่าจะ insert หรือ edit ก็ต้องแก้ไข เพราะเป็นการ key ผ่านทางลูกเรือโดยตรง จึงต้องแก้ค่าตามเพื่อไม่ให้ user สับสน
            crew.PassportCard.CopyFrom(crew.Alien.PassportCard);
            //th20110407 PD18-540102 Req 2
            crew.IDCardNo = crew.Alien.IDCardNo;
            crew.SeamanCardNo = crew.Alien.SeamanCardNo;
        }

        //
        // Insert / Delete Method
        /// <summary>
        /// สำหรับเพิ่ม record ลง ef
        /// </summary>
        /// <param name="crew"></param>
        private void _add(Crew crew)
        {
            entities.Crews.AddObject(crew);
        }

        /// <summary>
        /// สำหรับลบ record ออกจาก ef รวมทั้งลบลูกที่เกี่ยวข้อง (ถ้ามี)
        /// </summary>
        /// <param name="crew"></param>
        private void _delete(Crew crew)
        {
            // ถ้ามี ref ตัวลูก ให้ loop ลบ ref ก่อน...
            //entities.ConveyanceInOuts.DeleteObject(crew);
            crew.IsCancel = true;
        }

        /// <summary>
        /// ตรวจสอบว่าข้อมูลที่กรอกเข้ามาถูกต้องหรือไม่ 
        /// โดยเป็นส่วนของ logic ที่ไม่สามารถระบุใน metadata ไม่ได้ และไม่ได้ระบุเอาไว้ใน constraint ของ database
        /// </summary>
        /// <param name="crew"></param>
        /// <param name="isCreate"></param>
        private void _validate(Crew crew, bool isCreate)
        {
            var errors = new RulesException<Crew>();

            if (crew.Code != "")
            {
                int crewId = 0;
                if (!isCreate) crewId = crew.Id;
                var oldCode = (from p in entities.Crews
                                  where (p.Id != crewId) &&
                                        (p.Code == crew.Code) &&
                                        (p.IsCrew == crew.IsCrew) &&
                                        !p.IsCancel &&
                                        (p.ConveyanceInOutId == crew.ConveyanceInOutId)
                                  select p.Id).Count();
                if (oldCode > 0)
                    errors.ErrorFor(x => x.Code, "Code ที่กรอก ซ้ำกับรายการอื่นในหน้านี้");
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
        /// <param name="crew"></param>
        /// <param name="numRecursive">จำนวนที่ให้วน loop ปรกติไม่ต้องใส่มา</param>
        private void _saveCheckCode(Crew crew, int numRecursive = 5)
        {
            try
            {
                _save();
            }
            catch (Exception ex)
            {
                var clash = new RulesException<Crew>();
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
        /// เรียกข้อมูล crew ทั้งหมด
        /// </summary>
        /// <returns>crew table</returns>
        public IQueryable<Crew> FindAll(int conveyanceInOutId,bool isCrew)
        {
            return entities.Crews.Where(p => (p.ConveyanceInOutId == conveyanceInOutId) && 
                                             (!p.IsCancel) && 
                                             (p.IsCrew == isCrew));
        }

        /// <summary>
        /// เรียกข้อมูล crew ตาม id ที่ระบุ
        /// </summary>
        /// <param name="id">crew's id</param>
        /// <returns>crew entity</returns>
        public Crew GetOne(int conveyanceInOutId, bool isCrew, int crewId)
        {
            return this.FindAll(conveyanceInOutId,isCrew).FirstOrDefault(p => p.Id == crewId);
        }

        /// <summary>
        /// เรียกข้อมูล crew ตาม id ที่ระบุ
        /// </summary>
        /// <param name="id">crew's id</param>
        /// <returns>crew entity</returns>
        public Crew GetOneByCode(int conveyanceInOutId, string code, bool isCrew, int crewId = -1)
        {
            return this.FindAll(conveyanceInOutId,isCrew)
                .FirstOrDefault(p => (p.Code == code) &&
                                     (p.ConveyanceInOutId == conveyanceInOutId) &&
                                     (p.IsCrew == isCrew) &&
                                     (p.Id != crewId));
        }
        #endregion

        /// <summary>
        /// What to do when create new crew
        /// </summary>
        /// <param name="crew"></param>
        public void DoNewRecord(Crew crew)
        {
            crew.UpdateInfo.LogAdded(CurrentUserName);
            if (crew.Alien != null)
                crew.Alien.UpdateInfo.LogAdded(CurrentUserName);
            
            crew.IsCancel = false;
        }

        /// <summary>
        /// prepare data and save to ef, ใช้ทั้ง update และ insert
        /// </summary>
        /// <param name="crew"></param>
        /// <param name="isCreate">ส่ง true ถ้าต้องการ insert</param>
        public void DoSave(Crew crew, bool isCreate)
        {
            if (isCreate)
            {
                //ถ้า Insert แต่ Alien มีค่าอยู่แล้ว ต้องเอาออกก่อน ไม่งั้นจะ add Alien เข้าไปด้วย
                if (crew.AlienId != 0)
                {
                    var updatedAlien = crew.Alien;
                    crew.Alien = null;
                    //ให้ไป update ที่ alien โดยตรง
                    var oldAlien = entities.Aliens.Single(a => a.Id == crew.AlienId);
                    entities.Aliens.Attach(oldAlien);
                    updatedAlien.Photo.CopyFrom(oldAlien.Photo); //ต้อง copy ถ่ายค่าของรูปภาพมาด้วย เพราะไม่มีข้อมูลเก็บในหน้าจอ
                    entities.Aliens.ApplyCurrentValues(updatedAlien);
                }

                _add(crew);
            }

            _doBeforePost(crew, isCreate);
            _validate(crew, isCreate);

            _saveCheckCode(crew);
        }

        /// <summary>
        /// prepare data and delete from ef
        /// </summary>
        /// <param name="crew"></param>
        public void DoDelete(Crew crew)
        {
            _delete(crew);
            _save();
        }

        #endregion
    }
}
