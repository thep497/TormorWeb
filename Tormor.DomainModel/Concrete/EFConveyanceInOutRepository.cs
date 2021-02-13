// *******************************************************************
//1. ชื่อโปรแกรม 	: NeoSKIMO
//2. ชื่อระบบ 		: DomainModel
//3. ชื่อ Unit 	: EFConveyanceInOutRepository
//4. ผู้เขียนโปรแกรม	: อมรเทพ
//5. วันที่สร้าง	    : 201012xx
//6. จุดประสงค์ของ Unit นี้ : Repository สำหรับจัดการการเข้าออกของเรือ (ConveyanceInOut)
// *******************************************************************
// แก้ไขครั้งที่ : 1
// ประวัติการแก้ไข
// ครั้งที่ 0 : เริ่มต้นสร้าง unit ใหม่
// ครั้งที่ 1 : th20110407 เพิ่มการค้นหา ID/Seaman PD18-540102 Req 2
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
    public class EFConveyanceInOutRepository : Tormor.DomainModel.IConveyanceInOutRepository 
    {
        #region Private members....
        private NeoIMOSKEntities entities = new NeoIMOSKEntities();

        /// <summary>
        /// What to do before save any edit or newly created record
        /// </summary>
        /// <param name="convInOut"></param>
        private void _doBeforePost(ConveyanceInOut convInOut, bool isCreate)
        {
            //เอาค่าวันใส่ใน InOutTime
            convInOut.InOutTime = convInOut.InOutDate.AddTime(convInOut.InOutTime);

            convInOut.ClearNullDate();
            convInOut.UpdateInfo.LogUpdated(CurrentUserName);
            if (convInOut.Conveyance != null)
                convInOut.Conveyance.UpdateInfo.LogUpdated(CurrentUserName);

            //update จำนวนลูกเรือและผู้โดยสาร
            var numPsgrs = convInOut.Passengers.Count(c => !c.IsCancel && !(c.IsCrew ?? false));
            if (numPsgrs > 0)
                convInOut.NumPassenger = numPsgrs;

            //th20110409 เพิ่ม numAddCrews เพื่อต้องการรวมจำนวนคนเพิ่มเข้าไปด้วย
            var numCrews = convInOut.Passengers.Count(c => !c.IsCancel && (c.IsCrew ?? false));
            var numAddCrews = convInOut.DiffCrew.Count(c => !c.IsCancel && (c.AddRemoveType == ModelConst.ADDREMOVETYPE_ADD));

            var numAllCrews = numCrews + numAddCrews;
            if (numAllCrews > 0)
                convInOut.NumCrew = numAllCrews;

            if (isCreate)
            {
                if (convInOut.Conveyance != null)
                {
                    //convInOut.Conveyance.DataInType = 4; //convInOut
                    //convInOut.CurrentAddress.CopyFrom(convInOut.Conveyance.CurrentAddress);
                    //convInOut.PassportCard.CopyFrom(convInOut.Conveyance.PassportCard);
                }
            }

            //หาค่าใน AddRemoveCrew ด้วย ถ้ามีให้ update ไป
            foreach (var dcrew in convInOut.DiffCrew)
            {
                var oldAddRemoveCrew = entities.AddRemoveCrews.FirstOrDefault(a => a.Id == dcrew.Id);
                if (oldAddRemoveCrew != null)
                {
                    entities.AddRemoveCrews.Attach(oldAddRemoveCrew);
                    oldAddRemoveCrew.ConveyanceInOut = convInOut;
                    entities.AddRemoveCrews.ApplyCurrentValues(oldAddRemoveCrew);
                }
            }
        }

        //
        // Insert / Delete Method
        /// <summary>
        /// สำหรับเพิ่ม record ลง ef
        /// </summary>
        /// <param name="convInOut"></param>
        private void _add(ConveyanceInOut convInOut)
        {
            entities.ConveyanceInOuts.AddObject(convInOut);
        }

        /// <summary>
        /// สำหรับลบ record ออกจาก ef รวมทั้งลบลูกที่เกี่ยวข้อง (ถ้ามี)
        /// </summary>
        /// <param name="convInOut"></param>
        private void _delete(ConveyanceInOut convInOut)
        {
            // ถ้ามี ref ตัวลูก ให้ loop ลบ ref ก่อน...
            // ถ้ามี ref ตัวลูก ให้ loop ลบ ref ก่อน...
            foreach (var psgr in convInOut.Passengers)
            {
                psgr.IsCancel = true;
            }
            convInOut.IsCancel = true;
        }

        private void _checkCode(ConveyanceInOut convInOut, bool isCreate)
        {
            int stayId = 0;
            if (!isCreate) stayId = convInOut.Id;
            var oldCode = (from p in entities.ConveyanceInOuts
                           where (p.Id != stayId) &&
                                 (p.Code == convInOut.Code) &&
                                 (p.RequestDate.Year == convInOut.RequestDate.Year)
                           select p.Id).Count();
            if (oldCode > 0)
                throw new ApplicationException("dupcode");

        }
        /// <summary>
        /// ตรวจสอบว่าข้อมูลที่กรอกเข้ามาถูกต้องหรือไม่ 
        /// โดยเป็นส่วนของ logic ที่ไม่สามารถระบุใน metadata ไม่ได้ และไม่ได้ระบุเอาไว้ใน constraint ของ database
        /// </summary>
        /// <param name="convInOut"></param>
        /// <param name="isCreate"></param>
        private void _validate(ConveyanceInOut convInOut, bool isCreate)
        {
            var errors = new RulesException<ConveyanceInOut>();

            //ไม่ต้องเช็ค code ตรงนี้ ปล่อยให้ saveCheckCode จัดการ
            //if (convInOut.Code != "")
            //{
            //    //_checkCode(convInOut,isCreate);
            //}

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
                if (exMessage.Contains("IX_ConveyanceInOuts_Code"))
                {
                    var clash = new RulesException<ConveyanceInOut>();
                    clash.ErrorFor(x => x.Code, "Sorry, problem with index on Code");
                    throw clash;
                }
                throw; // Rethrow any other exceptions to avoid interfering with them 
            }
        }

        /// <summary>
        /// เรียกการบันทึกข้อมูล โดยตรวจสอบว่า Code ซ้ำหรือไม่
        /// </summary>
        /// <param name="convInOut"></param>
        /// <param name="numRecursive">จำนวนที่ให้วน loop ปรกติไม่ต้องใส่มา</param>
        private void _saveCheckCode(ConveyanceInOut convInOut, int numRecursive = 5)
        {
            try
            {
                _checkCode(convInOut, convInOut.Id == 0);
                _save();
            }
            catch (Exception ex)
            {
                //ถ้าซ้ำ ก็หา code ใหม่...
                if (ex.ExMessage() == "dupcode")
                {
                    if (numRecursive > 0)
                    {
                        convInOut.Code = GetNewCode();
                        _saveCheckCode(convInOut, --numRecursive); //recursive
                    }
                    else
                    {
                        var clash = new RulesException<ConveyanceInOut>();
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

        public IList<ConveyanceInOut> FindAll(ConveyanceSearchInfo convSearch)
        {
            DateTime? dtpFromDate = convSearch.dtpFromDate;
            DateTime? dtpToDate = convSearch.dtpToDate;

            var result = from p in entities.ConveyanceInOuts
                         select p;
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
            if (!convSearch.InOutDateFrom.IsNull())
                result = result.Where(s => convSearch.InOutDateFrom <= s.InOutDate);
            if (!convSearch.InOutDateTo.IsNull())
                result = result.Where(s => s.InOutDate <= convSearch.InOutDateTo);

            //th vvv ค้นหาแบบ where s.TType in ("1","2",) ...
            var typesToFind = new List<string>();
            if (convSearch.WantIn) typesToFind.Add("1");
            if (convSearch.WantOut) typesToFind.Add("2");

            if (!string.IsNullOrEmpty(convSearch.AgencyName))
            {
                var search = convSearch.AgencyName.Replace(" ", "").ToLower();
                result = result.Where(s => s.AgencyName.Replace(" ", "").ToLower().Contains(search));
            }
            if (!string.IsNullOrEmpty(convSearch.InspectOfficer))
            {
                var search = convSearch.InspectOfficer.Replace(" ", "").ToLower();
                result = result.Where(s => s.InspectOfficer.Replace(" ", "").ToLower().Contains(search));
            }
            //ข้อมูลเรือ
            if (!string.IsNullOrEmpty(convSearch.Name))
            {
                var search = convSearch.Name.Replace(" ", "").ToLower();
                result = result.Where(s => s.Conveyance.Name.Replace(" ", "").ToLower().Contains(search));
            }
            if (!string.IsNullOrEmpty(convSearch.OwnerName))
            {
                var search = convSearch.OwnerName.Replace(" ", "").ToLower();
                result = result.Where(s => s.Conveyance.OwnerName.Replace(" ", "").ToLower().Contains(search));
            }
            if (!string.IsNullOrEmpty(convSearch.RegistrationNo))
            {
                var search = convSearch.RegistrationNo.Replace(" ", "").ToLower();
                result = result.Where(s => s.Conveyance.RegistrationNo.Replace(" ", "").ToLower().Contains(search));
            }

            //ข้อมูลท่า
            if (!string.IsNullOrEmpty(convSearch.PortInFrom))
            {
                var search = convSearch.PortInFrom.Replace(" ", "").ToLower();
                result = result.Where(s => s.PortInFrom.PortName.Replace(" ", "").ToLower().Contains(search));
            }
            if (!string.IsNullOrEmpty(convSearch.PortInFrom_Country))
            {
                var search = convSearch.PortInFrom_Country.Replace(" ", "").ToLower();
                result = result.Where(s => s.PortInFrom.Country.Replace(" ", "").ToLower().Contains(search));
            }
            if (!string.IsNullOrEmpty(convSearch.PortInTo))
            {
                var search = convSearch.PortInTo.Replace(" ", "").ToLower();
                result = result.Where(s => s.PortInTo.PortName.Replace(" ", "").ToLower().Contains(search));
            }
            if (!string.IsNullOrEmpty(convSearch.PortOutFrom))
            {
                var search = convSearch.PortOutFrom.Replace(" ", "").ToLower();
                result = result.Where(s => s.PortOutFrom.PortName.Replace(" ", "").ToLower().Contains(search));
            }
            if (!string.IsNullOrEmpty(convSearch.PortOutTo))
            {
                var search = convSearch.PortInTo.Replace(" ", "").ToLower();
                result = result.Where(s => s.PortOutTo.PortName.Replace(" ", "").ToLower().Contains(search));
            }
            if (!string.IsNullOrEmpty(convSearch.PortOutTo_Country))
            {
                var search = convSearch.PortOutTo_Country.Replace(" ", "").ToLower();
                result = result.Where(s => s.PortOutTo.Country.Replace(" ", "").ToLower().Contains(search));
            }

            //ข้อมูลคน
            if (!string.IsNullOrEmpty(convSearch.AlienName))
            {
                var search = convSearch.AlienName.Replace(" ", "").ToLower();
                result = result.Where(c => c.Passengers.Any(s => !s.IsCancel &&
                                               ((s.Alien.Name.FirstName ?? "") +
                                                (s.Alien.Name.MiddleName ?? "") +
                                                (s.Alien.Name.LastName ?? "")).Replace(" ", "").ToLower().Contains(search)) ||
                                           c.AddRemoveCrews.Any(s => !s.IsCancel &&
                                               ((s.Alien.Name.FirstName ?? "") +
                                                (s.Alien.Name.MiddleName ?? "") +
                                                (s.Alien.Name.LastName ?? "")).Replace(" ", "").ToLower().Contains(search)));
            }
            if (!string.IsNullOrEmpty(convSearch.AlienPassport))
            {
                var search = convSearch.AlienPassport.Replace(" ", "").ToLower();
                result = result.Where(c => c.Passengers.Any(s => !s.IsCancel &&
                                                                  s.Alien.PassportCard.DocNo.Replace(" ", "").ToLower().Contains(search) ||
                                                                  //th20110407 PD18-540102 Req 2
                                                                  s.Alien.IDCardNo.Replace(" ", "").ToLower().Contains(search) ||
                                                                  s.Alien.SeamanCardNo.Replace(" ", "").ToLower().Contains(search) 
                                                            ) ||
                                           c.AddRemoveCrews.Any(s => !s.IsCancel && 
                                                                      s.Alien.PassportCard.DocNo.Replace(" ", "").ToLower().Contains(search) ||
                                                                      //th20110407 PD18-540102 Req 2
                                                                      s.Alien.IDCardNo.Replace(" ", "").ToLower().Contains(search) ||
                                                                      s.Alien.SeamanCardNo.Replace(" ", "").ToLower().Contains(search) 
                                                               ));
            }
            if (!string.IsNullOrEmpty(convSearch.AlienNationality))
            {
                var search = convSearch.AlienNationality.Replace(" ", "").ToLower();
                result = result.Where(c => c.Passengers.Any(s => !s.IsCancel && s.Alien.Nationality.Replace(" ", "").ToLower().Contains(search)) ||
                                           c.AddRemoveCrews.Any(s => !s.IsCancel && s.Alien.Nationality.Replace(" ", "").ToLower().Contains(search)));
            }

            result = result.Where(s => typesToFind.Contains(s.InOutType));
            //th ^^^

            //ต้อง convert เป็น List ก่อน จึงจะสามารถใช้ c# local function ใน where ได้ ไม่อย่างนั้นจะ EF Error
            var lResult = result.ToList();

            return lResult;
        }

        /// <summary>
        /// เรียกข้อมูล convInOut ทั้งหมด
        /// </summary>
        /// <returns>convInOut table</returns>
        public IQueryable<ConveyanceInOut> FindAll(DateTime? dtpFromDate=null, DateTime? dtpToDate=null)
        {
            var result = from v in entities.ConveyanceInOuts
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
            return result.OrderBy(v => v.RequestDate).ThenBy(v => v.Code);
        }

        /// <summary>
        /// เรียกข้อมูล convInOut ตาม id ที่ระบุ
        /// </summary>
        /// <param name="id">convInOut's id</param>
        /// <returns>convInOut entity</returns>
        public ConveyanceInOut GetOne(int id)
        {
            return this.FindAll().FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// เรียกข้อมูล convInOut ตาม id ที่ระบุ
        /// </summary>
        /// <param name="id">convInOut's id</param>
        /// <returns>convInOut entity</returns>
        public ConveyanceInOut GetOneByCode(string code, DateTime stayYear, int id = -1)
        {
            return this.FindAll()
                .FirstOrDefault(p => (p.Code == code) && 
                                     (p.RequestDate.Year == stayYear.Year) &&
                                     (p.Id != id));
        }

        public string GetNewCode()
        {
            string result = (from p in entities.ConveyanceInOuts
                             select p.Code).DefaultIfEmpty("000000").Max();
            return (Convert.ToInt32(result) + 1).ToString("D6");
        }

        /// <summary>
        /// What to do when create new convInOut
        /// </summary>
        /// <param name="convInOut"></param>
        public void DoNewRecord(ConveyanceInOut convInOut)
        {
            //ทำใน controller แล้ว convInOut.Code = GetNewCode();
            convInOut.UpdateInfo.LogAdded(CurrentUserName);
            convInOut.Conveyance.UpdateInfo.LogAdded(CurrentUserName);

            convInOut.IsCancel = false;
        }

        /// <summary>
        /// prepare data and save to ef, ใช้ทั้ง update และ insert
        /// </summary>
        /// <param name="convInOut"></param>
        /// <param name="isCreate">ส่ง true ถ้าต้องการ insert</param>
        public void DoSave(ConveyanceInOut convInOut, bool isCreate)
        {
            if (isCreate)
            {
                //ถ้า Insert แต่ Conveyance มีค่าอยู่แล้ว ต้องเอาออกก่อน ไม่งั้นจะ add Conveyance เข้าไปด้วย
                if (convInOut.ConveyanceId != 0)
                {
                    var updatedConv = convInOut.Conveyance;
                    convInOut.Conveyance = null;
                    //ให้ไป update ที่ conveyance โดยตรง
                    var oldConv = entities.Conveyances.Single(a => a.Id == convInOut.ConveyanceId);
                    entities.Conveyances.Attach(oldConv);
                    entities.Conveyances.ApplyCurrentValues(updatedConv);

                    _updateCrews(convInOut);
                }

                _add(convInOut);
            }

            _doBeforePost(convInOut, isCreate);
            _validate(convInOut, isCreate);

            _saveCheckCode(convInOut);

            //if (isCreate)
        }

        private void _updateCrews(ConveyanceInOut convInOut)
        {
            //หารายการลูกเรือคราวที่แล้ว
            //เริ่มต้นที่การหาวันที่ล่าสุดของเรือที่กำหนด
            //th20110409 แก้ Bug กรณีที่มีเรือ แต่ไม่มีรายการเข้าออก จะ error เนื่องจาก query นี้ให้ค่า null
            DateTime? maxInOutdate = entities.Crews
                                            .Where(c => 
                                                (c.ConveyanceInOut != null) &&
                                                (c.ConveyanceInOut.ConveyanceId == convInOut.ConveyanceId) &&
                                                (c.ConveyanceInOut.InOutDate <= convInOut.InOutDate) && //th20110409 เอาเฉพาะอันที่เก่ากว่าด้วย เผื่อ user key ย้อนหลัง
                                                //th ไม่ต้องกรองอันนี้ เรือเข้าก็เอาชุดเก่าที่เคยออกไปมาให้ได้ (c.ConveyanceInOut.InOutType == ModelConst.CONVINOUT_IN) && //th20110409 กรองเฉพาะเรือเข้า
                                                (c.IsCrew ?? false) && 
                                                !c.IsCancel)
                                            .Max(c => (DateTime?)c.ConveyanceInOut.InOutDate);

            if (maxInOutdate == null) //ถ้าไม่มี record เก่า ไม่ต้องทำต่อ
                return;

            //หา id ของรายการเข้าออกเรือ
            int? maxConveyanceInOutId = entities.ConveyanceInOuts
                                               .Where(c => !c.IsCancel && 
                                                            c.ConveyanceId == convInOut.ConveyanceId && //th20110409 ควรจะต้องกรองเฉพาะเรือลำเดิมเท่านั้น ไม่งั้น ถ้ามีหลายลำเข้าวันเดียวกัน ก็จะเอามามั่ว
                                                            c.InOutDate == maxInOutdate)
                                               .Max(c => (int?)c.Id);

            if (maxConveyanceInOutId == null)
                return;

            //ท่อนที่ 1 เอาลูกเรือตรง ๆ เข้ามาใส่
            var lastcrews = (from c in entities.Crews
                             where (c.ConveyanceInOut.ConveyanceId == convInOut.ConveyanceId) &&
                                   (c.IsCrew ?? false) && 
                                   !c.IsCancel &&
                                   (c.ConveyanceInOut.Id == maxConveyanceInOutId) &&
                                   //หาพวกที่ไม่ได้อยู่ใน remove ของคราวที่แล้ว
                                   !(from d in entities.AddRemoveCrews
                                     where !d.IsCancel && 
                                           (d.ConveyanceInOutId == maxConveyanceInOutId) &&
                                           (d.AddRemoveType == ModelConst.ADDREMOVETYPE_RMV) //เอาเฉพาะรายการลด
                                     select d.AlienId).Contains(c.AlienId)
                             select c).ToList();

            foreach (var crew in lastcrews)
            {
                //entities.Crews.Attach(crew);
                //entities.Crews.ApplyCurrentValues(crew);
                var newcrew = new Crew();

                newcrew.Code = crew.Code;
                newcrew.AlienId = crew.AlienId;
                newcrew.PassportCard.DocNo = crew.PassportCard.DocNo;
                
                //th20110411 เพิ่มการ copy id และ seaman Req 2
                newcrew.IDCardNo = crew.IDCardNo;
                newcrew.SeamanCardNo = crew.SeamanCardNo;

                newcrew.IsCrew = crew.IsCrew;
                newcrew.IsCancel = crew.IsCancel;

                newcrew.Remark = crew.Remark; //th20110409

                newcrew.ConveyanceInOut = convInOut;
                newcrew.UpdateInfo.LogAdded(CurrentUserName);
                newcrew.UpdateInfo.LogUpdated(CurrentUserName);

                entities.Crews.AddObject(newcrew);
            }

            //th20110409 ท่อนที่ 2 ถ้าเป็นขาเข้า เอาคนเพิ่มจากคราวที่แล้ว มาใส่ด้วย
            if (convInOut.InOutType == ModelConst.CONVINOUT_IN)
            {
                var lastAddCrews = from d in entities.AddRemoveCrews
                                   where !d.IsCancel &&
                                         (d.ConveyanceInOutId == maxConveyanceInOutId) &&
                                         (d.AddRemoveType == ModelConst.ADDREMOVETYPE_ADD) //เอาเฉพาะรายการเพิ่ม
                                   select d;

                foreach (var addCrew in lastAddCrews)
                {
                    if (!lastcrews.Any(lc => lc.AlienId == addCrew.AlienId)) //เอาคนที่ซ้ำกับ list 1 ออกไป
                    {
                        //entities.Crews.Attach(crew);
                        //entities.Crews.ApplyCurrentValues(crew);
                        var newcrew = new Crew();

                        newcrew.Code = addCrew.Code;
                        newcrew.AlienId = addCrew.AlienId;
                        newcrew.PassportCard.DocNo = addCrew.Alien.PassportCard.DocNo;

                        //th20110411 เพิ่มการ copy id และ seaman Req 2
                        newcrew.IDCardNo = addCrew.Alien.IDCardNo;
                        newcrew.SeamanCardNo = addCrew.Alien.SeamanCardNo;

                        newcrew.IsCrew = true;
                        newcrew.IsCancel = addCrew.IsCancel;

                        newcrew.Remark = addCrew.ExtendedData.Custom1; //th20110409

                        newcrew.ConveyanceInOut = convInOut;
                        newcrew.UpdateInfo.LogAdded(CurrentUserName);
                        newcrew.UpdateInfo.LogUpdated(CurrentUserName);

                        entities.Crews.AddObject(newcrew);
                    }
                }
            }
        }

        /// <summary>
        /// prepare data and delete from ef
        /// </summary>
        /// <param name="convInOut"></param>
        public void DoDelete(ConveyanceInOut convInOut)
        {
            _delete(convInOut);
            _save();
        }
        #endregion
    }
}
