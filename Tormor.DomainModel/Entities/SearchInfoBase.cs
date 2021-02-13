// *******************************************************************
//1. ชื่อโปรแกรม 	: NeoSKIMO
//2. ชื่อระบบ 		: DomainModel
//3. ชื่อ Unit 	: SearchInfoBase
//4. ผู้เขียนโปรแกรม	: อมรเทพ
//5. วันที่สร้าง	    : 201012xx
//6. จุดประสงค์ของ Unit นี้ : Entity เก็บข้อมูลช่วงวันที่ในการค้นหา เพื่อให้จำตลอดโปรแกรม
// *******************************************************************
// แก้ไขครั้งที่ : 0
// ประวัติการแก้ไข
// ครั้งที่ 0 : เริ่มต้นสร้าง unit ใหม่
// *******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using NNS.GeneralHelpers;

namespace Tormor.DomainModel
{
    public class SearchInfoBase
    {
        public SearchInfoBase()
        {
            dtpSelectRange = 0;
            dtpFromDate = DateTime.Today.StartOfTheMonth();
            dtpToDate = DateTime.Today;
        }

        [DisplayName("ตัวกรองวันที่")]
        public int dtpSelectRange { get; set; }

        [DisplayName("วันที่ค้นหา-จาก")]
        public DateTime? dtpFromDate { get; set; }

        [DisplayName("วันที่ค้นหา-ถึง")]
        public DateTime? dtpToDate { get; set; }
    }
}
