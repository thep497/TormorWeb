// *******************************************************************
//1. ชื่อโปรแกรม 	: NeoSKIMO
//2. ชื่อระบบ 		: DomainModel
//3. ชื่อ Unit 	: AlienTransaction
//4. ผู้เขียนโปรแกรม	: อมรเทพ
//5. วันที่สร้าง	    : 201012xx
//6. จุดประสงค์ของ Unit นี้ : Entity Model สำหรับแสดงข้อมูลที่ใช้ค้นหา Alien
// *******************************************************************
// แก้ไขครั้งที่ : 1
// ประวัติการแก้ไข
// ครั้งที่ 0 : เริ่มต้นสร้าง unit ใหม่
// ครั้งที่ 1 : th20110407 เพิ่มการค้นหา StayReason PD18-540102 Req 3.2 และ พาหนะ 7.4
// *******************************************************************
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using NNS.GeneralHelpers;

namespace Tormor.DomainModel
{
    public class AlienTransaction
    {
        private IReferenceRepository repo = new EFReferenceRepository();

        public int Id { get; set; }

        [DisplayName("ประเภท")]
        public string TType { get; set; }

        [DisplayName("ผู้ยื่นคำร้อง")]
        public Alien Alien { get; set; }

        [DisplayName("อายุ")]
        public int Age
        {
            get
            {
                return (this.Alien.DateOfBirth ?? DateTime.Today).CalcAgeYear();
            }
        }

        [DisplayName("อายุ ณ วันรับคำร้อง")]
        public int RequestAge
        {
            get
            {
                return (this.Alien.DateOfBirth ?? DateTime.Today).CalcAgeYear(this.RequestDate);
            }
        }

        [DisplayName("ลำดับที่")]
        public string Code { get; set; }

        [DisplayName("วันที่ยื่นคำร้อง")]
        public DateTime RequestDate { get; set; }

        [DisplayName("Passport")]
        public IDDocument PassportCard { get; set; }

        [DisplayName("ใบเสร็จ")]
        public InvoiceInfo Invoice { get; set; }

        [DisplayName("อนุญาตถึงวันที่")]
        public DateTime? PermitToDate { get; set; }

        [DisplayName("เข้ามาเมื่อ")]
        public DateTime? DateArrive { get; set; }

        //th20110407 PD18-540102 Req 3.2, 7.4
        [DisplayName("เหตุผลที่ขออยู่ต่อ")]
        public string StayReason { get; set; }
        [DisplayName("ชื่อพาหนะ")]
        public string ConveyanceName { get; set; }

        public bool IsCancel { get; set; }
        public LogInfo UpdateInfo { get; set; }

        [DisplayName("ประเภทรายการ")]
        public string TypeName
        {
            get
            {
                var rrName = (from r in repo.FindAll(100)
                              where r.Code == this.TType
                              select r.RefName).DefaultIfEmpty("").First();

                return rrName;
            }
        }

    }
}
