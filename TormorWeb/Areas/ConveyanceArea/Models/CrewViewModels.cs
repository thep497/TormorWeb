// *******************************************************************
//1. ชื่อโปรแกรม 	: NeoSKIMO
//2. ชื่อระบบ 		: WebUI
//3. ชื่อ Unit 	: CrewViewModel
//4. ผู้เขียนโปรแกรม	: อมรเทพ
//5. วันที่สร้าง	    : 201012xx
//6. จุดประสงค์ของ Unit นี้ : Class เก็บ ข้อมูลของ Crews ที่จะใช้แสดงในหน้าจอ
// *******************************************************************
// แก้ไขครั้งที่ : 1
// ประวัติการแก้ไข
// ครั้งที่ 0 : เริ่มต้นสร้าง unit ใหม่
// ครั้งที่ 1 : เพิ่ม 2 field (ID/Seaman) PD18-540102 Req 2
//         เพิ่ม วันที่แจ้ง และแก้ไขหมายเหตุ Req 8.3 และ 8.4 กรณีเพิ่มลด
// *******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tormor.DomainModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using NNS.GeneralHelpers;
using NNS.ModelHelpers;

namespace Tormor.Web.Models
{
    public static class CrewHelper
    {
        public static string IsCrewStr(bool isCrew)
        {
            if (isCrew) return "C";
            return "P";
        }
        public static string IsCrewStr(object isCrew)
        {
            try
            {
                return IsCrewStr((bool?)isCrew ?? true);
            }
            catch
            {
                return "C";
            }
        }
    }
    [MetadataType(typeof(Tormor.DomainModel.Crew.Crew_Validation))]
    public class CrewLite
    {
        public int Id { get; set; }
        public int AlienId { get; set; }

        public int AddRemoveType { get; set; }

        [DisplayName("ชื่อ")]
        public string FullName { get; set; }

        [DisplayName("เพศ")]
        public string Sex { get; set; }

        [DisplayName("สัญชาติ")]
        public string Nationality { get; set; }

        [DisplayName("วันเกิด")]
        public DateTime? DateOfBirth { get; set; }

        public bool IsCrew { get; set; }

        [DisplayName("ลำดับที่")]
        public string Code { get; set; }

        [DisplayName("เลขที่ Passport")]
        public IDDocument PassportCard { get; set; }

        //th20110407 PD18-540102 Req 2
        [DisplayName("เลขที่ ID")]
        public string IDCardNo { get; set; }
        [DisplayName("เลขที่ Seaman")]
        public string SeamanCardNo { get; set; }

        //th20110407 PD18-540102 Req 8.3
        [DisplayName("วันที่แจ้ง")]
        public DateTime? RequestDate { get; set; }

        [DisplayName("หมายเหตุ")]
        public string Remark { get; set; }

        public LogInfo UpdateInfo { get; set; }
        public bool IsCancel { get; set; }

        [DisplayName("อายุ")]
        public int Age
        {
            get
            {
                return (this.DateOfBirth ?? DateTime.Today).CalcAgeYear();
            }
        }

        [DisplayName("เพิ่ม/ลด")]
        public string AddRemoveTypeStr
        {
            get
            {
                return this.AddRemoveType == 1 ? "เพิ่ม" : "ลด";
            }
        }


        public CrewLite()
        {
        }

        public CrewLite(Crew crew)
        {
            this.CopyFrom(crew);
        }

        public CrewLite(AddRemoveCrew crew)
        {
            this.CopyFrom(crew);
        }

        public void CopyFrom(Crew crew)
        {
            if (crew != null)
            {
                this.Id = crew.Id;
                this.AlienId = crew.AlienId;
                this.Code = crew.Code;
                this.PassportCard = crew.PassportCard;
                //th20110407
                this.IDCardNo = crew.IDCardNo;
                this.SeamanCardNo = crew.SeamanCardNo;

                this.Remark = crew.Remark;
                this.UpdateInfo = crew.UpdateInfo;
                this.IsCancel = crew.IsCancel;
                this.AddRemoveType = ModelConst.ADDREMOVETYPE_NONE;

                this.FullName = crew.Alien.Name.FullName;
                this.Sex = crew.Alien.Sex;
                this.Nationality = crew.Alien.Nationality;
                this.DateOfBirth = crew.Alien.DateOfBirth;
            }
        }
        public void CopyFrom(AddRemoveCrew crew)
        {
            if (crew != null)
            {
                this.Id = crew.Id;
                this.AlienId = crew.AlienId;
                this.Code = crew.Code;
                this.PassportCard = crew.Alien.PassportCard;
                //th20110407
                this.IDCardNo = crew.Alien.IDCardNo;
                this.SeamanCardNo = crew.Alien.SeamanCardNo;
                //th20110408
                this.RequestDate = crew.RequestDate;
                this.Remark = crew.ExtendedData.Custom1; //th20110408 PD18-540102 Req 8.4 ดึงหมายเหตุแสดง
                
                this.UpdateInfo = crew.UpdateInfo;
                this.IsCancel = crew.IsCancel;
                this.AddRemoveType = crew.AddRemoveType;

                this.FullName = crew.Alien.Name.FullName;
                this.Sex = crew.Alien.Sex;
                this.Nationality = crew.Alien.Nationality;
                this.DateOfBirth = crew.Alien.DateOfBirth;
            }
        }
    }
    public class CrewViewModel
    {
        public CrewViewModel(int conveyanceInOutId, bool isCrew,string cName, DateTime ioDate)
        {
            this.vmConveyanceInOutId = conveyanceInOutId;
            this.vmIsCrew = isCrew;
            this.vmConveyanceName = cName;
            this.vmInOutDate = ioDate;
            this.vmIsAddRemove = false;

            this.vmCrews = new List<CrewLite>();
        }
        public CrewViewModel(int conveyanceInOutId, bool isCrew, string cName, DateTime ioDate, 
            IEnumerable<Crew> crews, IEnumerable<AddRemoveCrew> addRemoveCrews = null )
            : this(conveyanceInOutId,isCrew,cName,ioDate)
        {
            this.vmIsAddRemove = false;
            foreach (var crew in crews.Where(c => c.IsCrew == isCrew))
            {
                this.vmCrews.Add(new CrewLite(crew));
            }

            //th20110409 เพิ่มการดึงรายการเพิ่มลด เข้าไปแสดงใน Tab แรก
            if (isCrew && addRemoveCrews != null)
            {
                foreach (var crew in addRemoveCrews)
                {
                    this.vmCrews.Add(new CrewLite(crew));
                }
            }

            this.vmCrews = this.vmCrews.OrderBy(c => c.AddRemoveType).ThenBy(c => c.Code).ToList();
        }

        public CrewViewModel(int conveyanceInOutId, string cName, DateTime ioDate, IEnumerable<AddRemoveCrew> crews)
            : this(conveyanceInOutId, true, cName, ioDate)
        {
            this.vmIsAddRemove = true;

            foreach (var crew in crews)
            {
                this.vmCrews.Add(new CrewLite(crew));
            }

            this.vmCrews = this.vmCrews.OrderBy(c => c.AddRemoveType).ThenBy(c => c.Code).ToList();
        }

        public int vmConveyanceInOutId { get; set; }
        public bool vmIsCrew { get; set; }
        public bool vmIsAddRemove { get; set; }
        public string vmConveyanceName { get; set; }
        public DateTime vmInOutDate { get; set; }
        public IList<CrewLite> vmCrews { get; set; }

        public string vmIsCrewStr
        {
            get
            {
                return CrewHelper.IsCrewStr(this.vmIsCrew);
            }
        }
    }
} 