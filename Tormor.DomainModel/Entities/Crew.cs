// *******************************************************************
//1. ชื่อโปรแกรม 	: NeoSKIMO
//2. ชื่อระบบ 		: DomainModel
//3. ชื่อ Unit 	: Crew
//4. ผู้เขียนโปรแกรม	: อมรเทพ
//5. วันที่สร้าง	    : 201012xx
//6. จุดประสงค์ของ Unit นี้ : Entity Class เก็บ Extended Field และ Validation ของ Crews
// *******************************************************************
// แก้ไขครั้งที่ : 1
// ประวัติการแก้ไข
// ครั้งที่ 0 : เริ่มต้นสร้าง unit ใหม่
// ครั้งที่ 1 : //th20110407 เพิ่ม 2 field (ID/Seaman) PD18-540102 Req 2
// *******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Tormor.DomainModel
{
    [MetadataType(typeof(Crew_Validation))]
    public partial class Crew
    {
        public Crew()
        {
            //ใส่ค่า default ที่จะให้ show ใน form กรณีที่สร้าง record ใหม่ (อีกส่วนหนึ่งอยู่ใน DoNewRecord ใน repo)
            //this.StampDate = DateTime.Today;
        }
        public Crew(int conveyanceInOutId,bool isCrew) : this()
        {
            this.ConveyanceInOutId = conveyanceInOutId;
            this.IsCrew = IsCrew;
        }

        public class Crew_Validation
        {
            [Required(ErrorMessage="กรุณากรอกลำดับที่")]
            [DisplayName("ลำดับที่")]
            public string Code { get; set; }

            [DisplayName("Passport")]
            public IDDocument PassportCard { get; set; }

            [DisplayName("เลขที่ ID")]
            public string IDCardNo { get; set; }

            [DisplayName("เลขที่ Seaman")]
            public string SeamanCardNo { get; set; }

            [DisplayName("บุคคล")]
            public int AlienId { get; set; }

            [DisplayName("หมายเหตุ")]
            public string Remark { get; set; }

        }
    }
}
