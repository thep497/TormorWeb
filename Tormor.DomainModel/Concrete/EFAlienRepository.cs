// *******************************************************************
//1. ชื่อโปรแกรม 	: NeoSKIMO
//2. ชื่อระบบ 		: DomainModel
//3. ชื่อ Unit 	: EFAlienRepository
//4. ผู้เขียนโปรแกรม	: อมรเทพ
//5. วันที่สร้าง	    : 201012xx
//6. จุดประสงค์ของ Unit นี้ : Repository จัดการ Aliens
// *******************************************************************
// แก้ไขครั้งที่ : 1
// ประวัติการแก้ไข
// ครั้งที่ 0 : เริ่มต้นสร้าง unit ใหม่
// ครั้งที่ 1 : th20110407 เพิ่มการค้นหา 2 field (ID/Seaman) ในช่อง Passport PD18-540102 Req 2
// *******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NNS.ModelHelpers;
using NNS.GeneralHelpers;
using System.Linq.Expressions;

namespace Tormor.DomainModel
{
    public class EFAlienRepository : Tormor.DomainModel.IAlienRepository
    {
        #region Private members....
        private readonly NeoIMOSKEntities entities = new NeoIMOSKEntities();

        /// <summary>
        /// Select Query ของ Alien ที่ยอมให้ add where เข้ามาได้ 
        /// </summary>
        /// <param name="whereCls"></param>
        /// <returns></returns>
        private IQueryable<Alien> _selectAll(List<Expression<Func<Alien, bool>>> whereCls)
        {
            var selectAllQry = from t in entities.Aliens
                               select t;

            if (whereCls != null)
            {
                whereCls.ForEach(whereStmt => selectAllQry = selectAllQry.Where(whereStmt));
            }
            return selectAllQry;
        }

        /// <summary>
        /// What to do before save any edit or newly created record
        /// </summary>
        /// <param name="alien"></param>
        private void _doBeforePost(Alien alien)
        {
            alien.UpdateInfo.LogUpdated(CurrentUserName);
        }

        //
        // Insert / Delete Method
        /// <summary>
        /// สำหรับเพิ่ม record ลง ef
        /// </summary>
        /// <param name="alien"></param>
        private void _add(Alien alien)
        {
            entities.Aliens.AddObject(alien);
        }

        /// <summary>
        /// สำหรับลบ record ออกจาก ef รวมทั้งลบลูกที่เกี่ยวข้อง (ถ้ามี)
        /// </summary>
        /// <param name="alien"></param>
        private void _delete(Alien alien)
        {
            // ถ้ามี ref ตัวลูก ให้ loop ลบ ref ก่อน...
            //entities.Aliens.DeleteObject(alien);
            alien.IsCancel = true;
        }

        /// <summary>
        /// ตรวจสอบว่าข้อมูลที่กรอกเข้ามาถูกต้องหรือไม่ 
        /// โดยเป็นส่วนของ logic ที่ไม่สามารถระบุใน metadata ไม่ได้ และไม่ได้ระบุเอาไว้ใน constraint ของ database
        /// </summary>
        /// <param name="alien"></param>
        /// <param name="isCreate"></param>
        private void _validate(Alien alien, bool isCreate)
        {
            var errors = new RulesException<Alien>();

            if (alien.Name.FirstName == "a")
                errors.ErrorFor(x => x.Name.FirstName, "a is not allowed...");

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
                if (exMessage.Contains("IX_mst_Product_ProdCode"))
                {
                    throw new ApplicationException("dupcode");
                }
                if (exMessage.Contains("IX_mst_Product_ProdName"))
                {
                    var clash = new RulesException<Alien>();
                    clash.ErrorFor(x => x.Name.FirstName, "Sorry, duplicate name not allowed");
                    throw clash;
                }
                throw; // Rethrow any other exceptions to avoid interfering with them 
            }
        }

        /// <summary>
        /// เรียกการบันทึกข้อมูล โดยตรวจสอบว่า Code ซ้ำหรือไม่
        /// </summary>
        /// <param name="alien"></param>
        /// <param name="numRecursive">จำนวนที่ให้วน loop ปรกติไม่ต้องใส่มา</param>
        private void _saveCheckCode(Alien alien, int numRecursive = 5)
        {
            try
            {
                _save();
            }
            catch (ApplicationException ex)
            {
                var clash = new RulesException<Alien>();
                clash.ErrorFor(x => x.Name.FirstName, "Error :" + ex.ExMessage());
                throw clash;
            }
        }

        #endregion

        #region Public (Interface)...
        public string CurrentUserName { get; set; }
        //
        // Query Method

        /// <summary>
        /// เรียกข้อมูล alien ทั้งหมด
        /// </summary>
        /// <returns>alien table</returns>
        public IQueryable<Alien> FindAll()
        {
            return entities.Aliens.Where(p => !p.IsCancel);
        }

        /// <summary>
        /// ค้นหา Alien จากเงื่อนไขที่ส่งมาซึ่งอาจจะเป็น passcard.docno หรือ fullname ก็ได้
        /// </summary>
        /// <param name="passportCond"></param>
        /// <returns></returns>
        public IQueryable<Alien> Search(string passportCond, string nameCond)
        {
            passportCond = passportCond.ToLower();
            var lNameCond = nameCond.Replace(" ", "");

            var whereCls = new List<Expression<Func<Alien, bool>>>();

            whereCls.Add(alien => !alien.IsCancel);

            if (!string.IsNullOrEmpty(passportCond))
                //th20110407 เพิ่มเงื่อนไขการค้นหา ID/Seaman PD18-540102 req 2
                whereCls.Add(alien => alien.PassportCard.DocNo.ToLower().Contains(passportCond) ||
                                      alien.IDCardNo.ToLower().Contains(passportCond) ||
                                      alien.SeamanCardNo.ToLower().Contains(passportCond)
                             );

            if (!string.IsNullOrEmpty(lNameCond))
                whereCls.Add(alien => ((alien.Name.Title ?? "") + (alien.Name.FirstName ?? "") + (alien.Name.MiddleName ?? "") + (alien.Name.LastName ?? "")).ToLower().Contains(lNameCond));

            return _selectAll(whereCls);
        }

        private bool SearchAlien(Alien alien, string searchCond)
        {
            if ((alien.Name != null) && (alien.PassportCard != null))
            {
                return TpMisc.IsSearchCondInAllField_ByAnd(searchCond, new string[] { alien.PassportCard.DocNo, alien.Name.Title, alien.Name.FirstName, alien.Name.MiddleName, alien.Name.LastName });
            }
            else if (alien.Name != null)
            {
                return TpMisc.IsSearchCondInAllField_ByAnd(searchCond, new string[] { alien.Name.Title, alien.Name.FirstName, alien.Name.MiddleName, alien.Name.LastName });
            }
            else
                return TpMisc.IsSearchCondInAllField_ByAnd(searchCond, new string[] { alien.PassportCard.DocNo });
        }

        /// <summary>
        /// เรียกข้อมูล alien ตาม id ที่ระบุ
        /// </summary>
        /// <param name="id">alien's id</param>
        /// <returns>alien entity</returns>
        public Alien GetOne(int id)
        {
            return this.FindAll().FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// What to do when create new alien
        /// </summary>
        /// <param name="alien"></param>
        public void DoNewRecord(Alien alien)
        {
            //alien.Code = _getNewProductCode();
            alien.UpdateInfo.LogAdded(CurrentUserName);
        }

        /// <summary>
        /// prepare data and save to ef, ใช้ทั้ง update และ insert
        /// </summary>
        /// <param name="alien"></param>
        /// <param name="isCreate">ส่ง true ถ้าต้องการ insert</param>
        public void DoSave(Alien alien, bool isCreate)
        {
            if (isCreate)
                _add(alien);

            _doBeforePost(alien);
            _validate(alien, isCreate);
            _saveCheckCode(alien);
        }

        /// <summary>
        /// prepare data and delete from ef
        /// </summary>
        /// <param name="alien"></param>
        public void DoDelete(Alien alien)
        {
            _delete(alien);
            _save();
        }
        #endregion
    }
}
