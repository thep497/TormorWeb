// *******************************************************************
//1. ชื่อโปรแกรม 	: NeoSKIMO
//2. ชื่อระบบ 		: DomainModel
//3. ชื่อ Unit 	: ConveyanceInOut
//4. ผู้เขียนโปรแกรม	: อมรเทพ
//5. วันที่สร้าง	    : 201012xx
//6. จุดประสงค์ของ Unit นี้ : Entity Class เก็บ Extended Field และ Validation ของ รายการเรือเข้าออก (ConveyanceInOut)
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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using NNS.ModelHelpers;

namespace Tormor.DomainModel
{
    [MetadataType(typeof(ConveyanceInOut_Validation))]
    public partial class ConveyanceInOut
    {
        private readonly IReferenceRepository repo = new EFReferenceRepository();
        private readonly IAddRemoveCrewRepository addRemoveCrewRepo = new EFAddRemoveCrewRepository();

        public ConveyanceInOut()
        {
            //ใส่ค่า default ที่จะให้ show ใน form กรณีที่สร้าง record ใหม่ (อีกส่วนหนึ่งอยู่ใน DoNewRecord ใน repo)
            this.RequestDate = DateTime.Today;
            this.NumCrew = 0;
            this.NumPassenger = 0;
            this.InOutDate = DateTime.Today;
        }

        [DisplayName("ประเภท")]
        public string InOutTypeName
        {
            get
            {
                var refs = repo.GetOne(ModelConst.REF_CONVINOUT, this.InOutType);
                if (refs != null)
                    return refs.RefName;
                return "";
            }
        }
        [DisplayName("จากท่าเรือ")]
        public string PortForm
        {
            get
            {
                if (this.InOutType == ModelConst.CONVINOUT_IN) //เข้า
                {
                    return PortInFrom.PortNameCountry;
                }
                return PortOutFrom.PortNameCountry;
            }
        }
        [DisplayName("ถึงท่าเรือ")]
        public string PortTo
        {
            get
            {
                if (this.InOutType == ModelConst.CONVINOUT_IN) //เข้า
                {
                    return PortInTo.PortNameCountry;
                }
                return PortOutTo.PortNameCountry;
            }
        }

        public int NumAddRemoveCrew
        {
            get
            {
                return DiffCrew.Count();
            }
        }

        public IList<AddRemoveCrew> DiffCrew
        {
            get
            {
                var convNameCond = this.Conveyance.Name.ToLower().Replace(" ", "");
                if (this.InOutType == ModelConst.CONVINOUT_IN) //เข้า ต้องหาจากรายการคนลด เพราะถ้าเรือเข้า คนต้องมากับเรือ (InDetail คือ เรือ) แล้วออกทางอื่น
                {
                    var x = from a in addRemoveCrewRepo.SearchByInOutDate(ModelConst.ADDREMOVETYPE_RMV, this.InOutDate, this.InOutDate)
                            where a.InDetail.InWay.ToLower().Replace(" ", "") == convNameCond
                            select a;
                    return x.ToList();
                }
                else //th20110409 กรณีเรือออก หาทั้งคนเพิ่ม และคนลด (ของเรือเข้า) PD18-540102 Req 8.1
                {
                    //รายการคนเพิ่ม
                    var x = from a in addRemoveCrewRepo.SearchByInOutDate(ModelConst.ADDREMOVETYPE_ADD, this.InOutDate, this.InOutDate)
                            where a.OutDetail.InWay.ToLower().Replace(" ", "") == convNameCond
                            select a;
                    //th20110409 รายการคนลด
                    var y = from a in addRemoveCrewRepo.SearchByConveyanceLastIn(convNameCond,this.InOutDate)
                            //where a.InDetail.InWay.ToLower().Replace(" ", "") == convNameCond
                            select a;
                    return x.Concat(y).ToList();
                }
            }
        }

        public class ConveyanceInOut_Validation
        {
            [DisplayName("พาหนะ")]
            public int ConveyanceId{ get; set; }

            [DisplayName("รายการเข้า/ออก")]
            [Required(ErrorMessage = "กรุณากรอกประเภทเข้า/ออก")]
            public string InOutType { get; set; }

            [DisplayName("วันที่ทำรายการ")]
            [Required(ErrorMessage = "กรุณากรอกวันที่ทำรายการ")]
            public DateTime RequestDate { get; set; }

            [DisplayName("วัน/เวลาที่เรือเข้าหรือออก")]
            [Required(ErrorMessage = "กรุณากรอกวันที่เรือเข้า/ออก")]
            public DateTime InOutDate { get; set; }

            [UIHint("Time","MVC")]
            [DisplayName("วัน/เวลาที่เข้า/ออก")]
            public DateTime InOutTime { get; set; }

            [DisplayName("มาจากท่า")]
            public PortDetail PortInFrom { get; set; }

            [DisplayName("มาถึงท่า")]
            public PortDetail PortInTo { get; set; }

            [DisplayName("ออกจากท่า")]
            public PortDetail PortOutFrom { get; set; }

            [DisplayName("ออกไปที่ท่า")]
            public PortDetail PortOutTo { get; set; }

            [DisplayName("จำนวนคนประจำพาหนะ")]
            public int? NumCrew { get; set; }

            [DisplayName("จำนวนคนโดยสาร")]
            public int? NumPassenger { get; set; }

            [DisplayName("ชื่อตัวแทน")]
            public string AgencyName { get; set; }

            [DisplayName("เจ้าพนักงาน")]
            public string InspectOfficer { get; set; }
        }
    }
}
