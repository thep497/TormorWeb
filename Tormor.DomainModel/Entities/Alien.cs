// *******************************************************************
//1. ชื่อโปรแกรม 	: NeoSKIMO
//2. ชื่อระบบ 		: DomainModel
//3. ชื่อ Unit 	: Alien
//4. ผู้เขียนโปรแกรม	: อมรเทพ
//5. วันที่สร้าง	    : 201012xx
//6. จุดประสงค์ของ Unit นี้ : Entity Class เก็บ Extended Field และ Validation ของ Aliens
// *******************************************************************
// แก้ไขครั้งที่ : 1
// ประวัติการแก้ไข
// ครั้งที่ 0 : เริ่มต้นสร้าง unit ใหม่
// ครั้งที่ 1 : เพิ่ม 2 field (ID/Seaman) PD18-540102 Req 2
// *******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using NNS.GeneralHelpers;

namespace Tormor.DomainModel
{
    [MetadataType(typeof(Alien_Validation))]
    public partial class Alien
    {
        //th20110804 PD18-540802 ตม.ต้องการกรอกอายุได้ด้วย
        public int? Age
        {
            get
            {
                if (this.DateOfBirth != null)
                {
                    var xDate = this.DateOfBirth ?? DateTime.Today;
                    return xDate.CalcAgeYear();
                }

                return null;
            }
            set
            {
                if ((this.DateOfBirth == null) && ((value ?? 0) != 0))
                {
                    this.DateOfBirth = (value ?? 0).CalcDateOfBirth();
                }
            }
        }

        public class Alien_Validation
        {
            //[Required(ErrorMessage = "Fullname is Required")]
            //public PersonName Name { get; set; }

            //[DataType(DataType.Date)]
            [DisplayName("วันเกิด")]
            public string DateOfBirth { get; set; }

            [DisplayName("อายุ")]
            public int? Age { get; set; }

            [DisplayName("เลขที่ Passport")]
            public string PassportCard { get; set; }

            [DisplayName("เลขที่ใบสำคัญถิ่นที่อยู่")]
            public string HabitatCard { get; set; }

            [DisplayName("เป็นคนไทย")]
            public bool IsThai { get; set; }

            [DisplayName("เพศ")]
            [UIHint("CBSex", "MVC")]
            public string Sex { get; set; }

            [Required(ErrorMessage = "กรุณากรอกสัญชาติ")]
            [DisplayName("สัญชาติ")]
            [UIHint("CBNationality", "MVC")]
            public string Nationality { get; set; }

            [DisplayName("ที่อยู่ปัจจุบัน")]
            public Address CurrentAddress { get; set; }

            [DisplayName("เลขที่ ID")]
            public string IDCardNo { get; set; }

            [DisplayName("เลขที่ Seaman")]
            public string SeamanCardNo { get; set; }

        }
    }
}
