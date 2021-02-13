// *******************************************************************
//1. ชื่อโปรแกรม 	: NeoSKIMO
//2. ชื่อระบบ 		: DomainModel
//3. ชื่อ Unit 	: EFAlienTransactionRepository
//4. ผู้เขียนโปรแกรม	: อมรเทพ
//5. วันที่สร้าง	    : 201012xx
//6. จุดประสงค์ของ Unit นี้ : Repository สำหรับแสดงข้อมูลที่ใช้ค้นหา Alien
// *******************************************************************
// แก้ไขครั้งที่ : 1
// ประวัติการแก้ไข
// ครั้งที่ 0 : เริ่มต้นสร้าง unit ใหม่
// ครั้งที่ 1 : th20110407 เพิ่มการค้นหา StayReason PD18-540102 Req 3.2 และ พาหนะ 7.4
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
    public class EFAlienTransactionRepository : Tormor.DomainModel.IAlienTransactionRepository 
    {
        private NeoIMOSKEntities entities = new NeoIMOSKEntities();

        #region Public (Interface)...
        public string CurrentUserName { get; set; }
        //
        // Query Method

        /// <summary>
        /// เรียกข้อมูล visa ทั้งหมด
        /// </summary>
        /// <returns>visa table</returns>
        public IList<AlienTransaction> FindAll(AlienSearchInfo alienSearch)
        {
            DateTime? dtpFromDate = alienSearch.dtpFromDate;
            DateTime? dtpToDate = alienSearch.dtpToDate;

            var visaResult = from v in entities.VisaDetails
                             select new AlienTransaction
                             {
                                 TType = "1",
                                 Id = v.Id,
                                 Alien = v.Alien,
                                 Code = v.Code,
                                 RequestDate = v.RequestDate,
                                 PassportCard = v.PassportCard,
                                 Invoice = v.Invoice,
                                 DateArrive = v.DateArrive,
                                 PermitToDate = v.PermitToDate,
                                 StayReason = v.StayReason + " / " + v.StayReasonDetail, //th20110407 PD18-540102 Req 3.2
                                 ConveyanceName = "", //th20110407 PD18-540102 Req 7.4
                                 IsCancel = v.IsCancel,
                                 UpdateInfo = v.UpdateInfo
                             };
            var reentryResult = from v in entities.ReEntrys
                                select new AlienTransaction
                                {
                                    TType = "2",
                                    Id = v.Id,
                                    Alien = v.Alien,
                                    Code = v.Code,
                                    RequestDate = v.RequestDate,
                                    PassportCard = v.PassportCard,
                                    Invoice = v.Invoice,
                                    DateArrive = null,
                                    PermitToDate = v.PermitToDate,
                                    StayReason = "", //th20110407 PD18-540102 Req 3.2
                                    ConveyanceName = "", //th20110407 PD18-540102 Req 7.4
                                    IsCancel = v.IsCancel,
                                    UpdateInfo = v.UpdateInfo
                                };
            var endorseResult = from v in entities.Endorses
                                select new AlienTransaction
                                {
                                    TType = "3",
                                    Id = v.Id,
                                    Alien = v.Alien,
                                    Code = v.Code,
                                    RequestDate = v.RequestDate,
                                    PassportCard = v.PassportCard,
                                    Invoice = v.EndorseStamps.FirstOrDefault(x => !x.IsCancel && x.StampExpiredDate == v.EndorseStamps.Where(y => !y.IsCancel).Max(y => y.StampExpiredDate)).Invoice, //th20101226 NC5312xx
                                    DateArrive = null,
                                    PermitToDate = v.EndorseStamps.Where(x => !x.IsCancel).Max(x => x.StampExpiredDate),
                                    StayReason = "", //th20110407 PD18-540102 Req 3.2
                                    ConveyanceName = "", //th20110407 PD18-540102 Req 7.4
                                    IsCancel = v.IsCancel,
                                    UpdateInfo = v.UpdateInfo
                                };
            var stayResult = from v in entities.Staying90Days
                             select new AlienTransaction
                             {
                                 TType = "4",
                                 Id = v.Id,
                                 Alien = v.Alien,
                                 Code = v.Code,
                                 RequestDate = v.RequestDate,
                                 PassportCard = v.PassportCard,
                                 Invoice = v.Invoice,
                                 DateArrive = v.ArrivalDate,
                                 PermitToDate = null,
                                 StayReason = "", //th20110407 PD18-540102 Req 3.2
                                 ConveyanceName = "", //th20110407 PD18-540102 Req 7.4
                                 IsCancel = v.IsCancel,
                                 UpdateInfo = v.UpdateInfo
                             };
            var convResult = from v in entities.ConveyanceInOuts
                             from a in v.Passengers
                             select new AlienTransaction
                             {
                                 TType = (v.InOutType == "1" ? "5" : "6"),
                                 Id = v.Id,
                                 Alien = a.Alien,
                                 Code = v.Code,
                                 RequestDate = v.RequestDate,
                                 PassportCard = a.PassportCard,
                                 Invoice = v.Invoice,
                                 DateArrive = v.InOutDate,
                                 PermitToDate = null,
                                 StayReason = "", //th20110407 PD18-540102 Req 3.2
                                 ConveyanceName = v.Conveyance.Name, //th20110407 PD18-540102 Req 7.4
                                 IsCancel = a.IsCancel,
                                 UpdateInfo = v.UpdateInfo
                             };
            var adrmResult = from v in entities.AddRemoveCrews
                             select new AlienTransaction
                             {
                                 TType = (v.AddRemoveType == 1 ? "7" : "8"),
                                 Id = v.Id,
                                 Alien = v.Alien,
                                 Code = v.Code+" "+(v.SubCode ?? ""),
                                 RequestDate = v.RequestDate,
                                 PassportCard = v.Alien.PassportCard,
                                 Invoice = v.Invoice,
                                 DateArrive = v.AddRemoveType == 1 ? v.OutDetail.InDate : v.InDetail.InDate,
                                 PermitToDate = null,
                                 StayReason = "", //th20110407 PD18-540102 Req 3.2
                                 ConveyanceName = v.ConveyanceInOut.Conveyance.Name, //th20110407 PD18-540102 Req 7.4
                                 IsCancel = v.IsCancel,
                                 UpdateInfo = v.UpdateInfo
                             };

            var result = visaResult.Concat(reentryResult).Concat(endorseResult).Concat(stayResult)
                                   .Concat(convResult).Concat(adrmResult);
            result = result.OrderBy(c => c.RequestDate).ThenBy(c => c.TType).ThenBy(c => c.Code);

            result = result.Where(p => !p.IsCancel);
            if (!dtpFromDate.IsNull())
                result = result.Where(p => p.RequestDate >= dtpFromDate);
            if (!dtpToDate.IsNull())
                result = result.Where(p => p.RequestDate <= dtpToDate);

            if (!string.IsNullOrEmpty(alienSearch.PassportNo_Old))
            {
                var passportNo_Old = alienSearch.PassportNo_Old.ToLower();
                result = result.Where(s => s.PassportCard.DocNo.ToLower().Contains(passportNo_Old));
            }

            if (!string.IsNullOrEmpty(alienSearch.PassportNo_Current))
            {
                var passportNo_Current = alienSearch.PassportNo_Current.ToLower();
                result = result.Where(s => s.Alien.PassportCard.DocNo.ToLower().Contains(passportNo_Current));
            }

            if (!string.IsNullOrEmpty(alienSearch.AlienName))
            {
                var lNameCond = alienSearch.AlienName.Replace(" ", "");
                result = result.Where(s => ((s.Alien.Name.Title ?? "") + 
                                            (s.Alien.Name.FirstName ?? "") + 
                                            (s.Alien.Name.MiddleName ?? "") + 
                                            (s.Alien.Name.LastName ?? "")).ToLower().Contains(lNameCond));
            }


            if (!string.IsNullOrEmpty(alienSearch.CurrentAddress))
            {
                var currentAddress = alienSearch.CurrentAddress.Replace(" ", "");
                result = result.Where(s => ((s.Alien.CurrentAddress.AddrNo ?? "") +
                                                (s.Alien.CurrentAddress.Road ?? "") +
                                                (s.Alien.CurrentAddress.Tumbol ?? "") +
                                                (s.Alien.CurrentAddress.Amphur ?? "") +
                                                (s.Alien.CurrentAddress.Province ?? "") +
                                                (s.Alien.CurrentAddress.Postcode ?? "") +
                                                (s.Alien.CurrentAddress.Phone ?? "")).ToLower().Contains(currentAddress));
            }

            if (!string.IsNullOrEmpty(alienSearch.HabitatCardNo_Current))
            {
                var habitatCardNo_Current = alienSearch.HabitatCardNo_Current.ToLower();
                result = result.Where(s => s.Alien.HabitatCard.DocNo.ToLower().Contains(habitatCardNo_Current));
            }

            if (!string.IsNullOrEmpty(alienSearch.InvoiceNo))
            {
                var invoiceNo = alienSearch.InvoiceNo.ToLower();
                result = result.Where(s => s.Invoice.InvoiceNo.ToLower().Contains(invoiceNo));
            }

            if (!string.IsNullOrEmpty(alienSearch.Sex))
            {
                var sex = alienSearch.Sex.ToLower();
                result = result.Where(s => s.Alien.Sex.ToLower().Contains(sex));
            }

            if (!string.IsNullOrEmpty(alienSearch.Nationality))
            {
                var nationality = alienSearch.Nationality.ToLower();
                result = result.Where(s => s.Alien.Nationality.ToLower().Contains(nationality));
            }

            //th20110407 PD18-540102 Req 3.2
            if (!string.IsNullOrEmpty(alienSearch.StayReason))
            {
                var stayreason = alienSearch.StayReason.ToLower();
                result = result.Where(s => s.StayReason.ToLower().Contains(stayreason));
            }

            //th20110407 PD18-540102 Req 7.4
            if (!string.IsNullOrEmpty(alienSearch.ConveyanceName))
            {
                var convName = alienSearch.ConveyanceName.ToLower();
                result = result.Where(s => s.ConveyanceName.ToLower().Contains(convName));
            }

            if (!string.IsNullOrEmpty(alienSearch.Code))
            {
                var code = alienSearch.Code.ToLower();
                result = result.Where(s => s.Code.ToLower().Contains(code));
            }

            if (!alienSearch.DateArriveFrom.IsNull())
                result = result.Where(s => alienSearch.DateArriveFrom <= s.DateArrive);
            if (!alienSearch.DateArriveTo.IsNull())
                result = result.Where(s => s.DateArrive <= alienSearch.DateArriveTo);

            if (!alienSearch.PermitDateFrom.IsNull())
                result = result.Where(s => alienSearch.PermitDateFrom <= s.PermitToDate);
            if (!alienSearch.PermitDateTo.IsNull())
                result = result.Where(s => s.PermitToDate <= alienSearch.PermitDateTo);

            //th vvv ค้นหาแบบ where s.TType in ("1","2",) ...
            var typesToFind = new List<string>();
            if (alienSearch.WantVisa) typesToFind.Add("1");
            if (alienSearch.WantReEntry) typesToFind.Add("2");
            if (alienSearch.WantEndorse) typesToFind.Add("3");
            if (alienSearch.WantStay) typesToFind.Add("4");
            if (alienSearch.WantShip)
            {
                typesToFind.Add("5");
                typesToFind.Add("6");
                typesToFind.Add("7");
                typesToFind.Add("8");
            }

            result = result.Where(s => typesToFind.Contains(s.TType));
            //th ^^^

            //ต้อง convert เป็น List ก่อน จึงจะสามารถใช้ c# local function ใน where ได้ ไม่อย่างนั้นจะ EF Error
            var lResult = result.ToList();
            if ((alienSearch.AgeFrom ?? 0) > 0)
                lResult = lResult.Where(s => alienSearch.AgeFrom <= s.Age).ToList();
            if ((alienSearch.AgeTo ?? 0) > 0)
                lResult = lResult.Where(s => s.Age <= alienSearch.AgeTo).ToList();

            return lResult;
        }

        /// <summary>
        /// เรียกข้อมูล visa ตาม id ที่ระบุ
        /// </summary>
        /// <param name="id">visa's id</param>
        /// <returns>visa entity</returns>
        public AlienTransaction GetOne(string ttype, int id)
        {
            return this.FindAll(new AlienSearchInfo()).FirstOrDefault(p => p.Id == id && p.TType == ttype);
        }

        /// <summary>
        /// เรียกข้อมูล visa ตาม id ที่ระบุ
        /// </summary>
        /// <param name="id">visa's id</param>
        /// <returns>visa entity</returns>
        public AlienTransaction GetOneByCode(string ttype, string code, DateTime visaYear, int id = -1)
        {
            return this.FindAll(new AlienSearchInfo())
                .FirstOrDefault(p => (p.TType == ttype) &&
                                     (p.Code == code) && 
                                     (p.RequestDate.Year == visaYear.Year) &&
                                     (p.Id != id));
        }

        #endregion
    }
}
