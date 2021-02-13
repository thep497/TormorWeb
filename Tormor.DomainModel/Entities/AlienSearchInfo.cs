// *******************************************************************
//1. ชื่อโปรแกรม 	: NeoSKIMO
//2. ชื่อระบบ 		: DomainModel
//3. ชื่อ Unit 	: AlienTransaction
//4. ผู้เขียนโปรแกรม	: อมรเทพ
//5. วันที่สร้าง	    : 201012xx
//6. จุดประสงค์ของ Unit นี้ : Entity Model สำหรับเก็บข้อมูลที่ใช้ค้นหา Alien จากหน้าจอ Search
// *******************************************************************
// แก้ไขครั้งที่ : 1
// ประวัติการแก้ไข
// ครั้งที่ 0 : เริ่มต้นสร้าง unit ใหม่
// ครั้งที่ 1 : th20110407 เพิ่มการค้นหา StayReason PD18-540102 Req 3.2 และ พาหนะ 7.4
// *******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Tormor.DomainModel;
using System.ComponentModel.DataAnnotations;

namespace Tormor.DomainModel
{
    public class AlienSearchInfo
    {
        [DisplayName("ข้อมูลขออยู่ต่อ")]
        public bool WantVisa { get; set; }

        [DisplayName("ข้อมูล Re-Entry")]
        public bool WantReEntry { get; set; }

        [DisplayName("ข้อมูลสลักหลังถิ่นที่อยู่")]
        public bool WantEndorse { get; set; }

        [DisplayName("ข้อมูลรายงานตัว 90 วัน")]
        public bool WantStay { get; set; }

        [DisplayName("เกี่ยวกับเรือ")]
        public bool WantShip { get; set; }

        [DisplayName("Passport (ณ วันยื่นคำร้อง)")]
        public string PassportNo_Old { get; set; }

        [DisplayName("Passport (ปัจจุบัน)")]
        public string PassportNo_Current { get; set; }

        [DisplayName("ชื่อบุคคลต่างด้าว")]
        public string AlienName { get; set; }

        [DisplayName("ที่อยู่ปัจจุบัน")]
        public string CurrentAddress { get; set; }

        [DisplayName("ใบสำคัญถิ่นที่อยู่")]
        public string HabitatCardNo_Current { get; set; }

        [DisplayName("เพศ")]
        public string Sex { get; set; }

        [DisplayName("สัญชาติ")]
        [UIHint("CBNationalityAll", "MVC")]
        public string Nationality { get; set; }

        [DisplayName("อายุ-ตั้งแต่")]
        public int? AgeFrom { get; set; }

        [DisplayName("อายุ-ถึง")]
        public int? AgeTo { get; set; }

        [DisplayName("ลำดับที่เอกสาร")]
        public string Code { get; set; }

        [DisplayName("เลขที่ใบเสร็จ")]
        public string InvoiceNo { get; set; }

        [DisplayName("เดินทางเข้ามาเมื่อ")]
        public DateTime? DateArriveFrom { get; set; }
        [DisplayName("-")]
        public DateTime? DateArriveTo { get; set; }

        [DisplayName("อนุญาติถึงวันที่")]
        public DateTime? PermitDateFrom { get; set; }
        [DisplayName("-")]
        public DateTime? PermitDateTo { get; set; }
        /* /////////////////////////////////////////////////////// */
        [DisplayName("ตัวกรองวันที่")]
        public int dtpSelectRange { get; set; }

        [DisplayName("วันที่ยื่นคำร้อง-จาก")]
        public DateTime? dtpFromDate { get; set; }

        [DisplayName("วันที่ยื่นคำร้อง-ถึง")]
        public DateTime? dtpToDate { get; set; }

        //th20110407 PD18-540102 Req 3.2, 7.4
        [DisplayName("เหตุผลที่ขออยู่ต่อ")]
        public string StayReason { get; set; }
        [DisplayName("ชื่อพาหนะ")]
        public string ConveyanceName { get; set; }


        public AlienSearchInfo()
        {
            WantVisa = true;
            WantEndorse = true;
            WantReEntry = true;
            WantStay = true;
            WantShip = true;

            //th20110411 หากเรียกครั้งแรก ให้ default ค่าวันที่เป็นเดือนปัจจุบัน
            dtpSelectRange = 2;
        }
        public AlienSearchInfo(bool wantVisa, bool wantReEntry, bool wantEndorse, bool wantStay, bool wantShip)
            :this()
        {
            WantVisa = wantVisa;
            WantEndorse = wantEndorse;
            WantReEntry = wantReEntry;
            WantStay = wantStay;
            WantShip = wantShip; //th20110411 SU-540410/1 //WantShip
        }
    }
}