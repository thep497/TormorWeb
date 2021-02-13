using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Tormor.DomainModel;
using System.ComponentModel.DataAnnotations;

namespace Tormor.DomainModel
{
    public class ConveyanceSearchInfo
    {
        [DisplayName("ข้อมูลพาหนะเข้า")]
        public bool WantIn { get; set; }

        [DisplayName("ข้อมูลพาหนะออก")]
        public bool WantOut { get; set; }

        [DisplayName("ลำดับที่เอกสาร")]
        public string Code { get; set; }

        [DisplayName("วันที่เข้า/ออก")]
        public DateTime? InOutDateFrom { get; set; }
        [DisplayName("-")]
        public DateTime? InOutDateTo { get; set; }

        [DisplayName("ชื่อพาหนะ")]
        public string Name { get; set; }

        [DisplayName("หมายเลขทะเบียน")]
        public string RegistrationNo { get; set; }

        [DisplayName("ชื่อเจ้าของพาหนะ")]
        public string OwnerName { get; set; }

        [DisplayName("มาจากท่า")]
        public string PortInFrom { get; set; }
        [DisplayName("มาจากประเทศ")]
        public string PortInFrom_Country { get; set; }

        [DisplayName("มาถึงท่า (ในประเทศ)")]
        [UIHint("CBPortInThai", "MVC")]
        public string PortInTo { get; set; }

        [DisplayName("ออกจากท่า (ในประเทศ)")]
        [UIHint("CBPortInThai", "MVC")]
        public string PortOutFrom { get; set; }

        [DisplayName("ออกไปที่ท่า")]
        public string PortOutTo { get; set; }
        [DisplayName("ออกไปประเทศ")]
        public string PortOutTo_Country { get; set; }

        [DisplayName("ชื่อตัวแทน")]
        public string AgencyName { get; set; }

        [DisplayName("เจ้าพนักงาน")]
        public string InspectOfficer { get; set; }

        [DisplayName("ชื่อบุคคลต่างด้าว")]
        public string AlienName { get; set; }

        [DisplayName("Passport/ID/Seaman")]
        public string AlienPassport { get; set; }

        [DisplayName("สัญชาติ")]
        [UIHint("CBNationalityAll", "MVC")]
        public string AlienNationality { get; set; }

        /* /////////////////////////////////////////////////////// */
        [DisplayName("ตัวกรองวันที่")]
        public int dtpSelectRange { get; set; }

        [DisplayName("วันที่ยื่นคำร้อง-จาก")]
        public DateTime? dtpFromDate { get; set; }

        [DisplayName("วันที่ยื่นคำร้อง-ถึง")]
        public DateTime? dtpToDate { get; set; }

        public ConveyanceSearchInfo()
        {
            WantIn = true;
            WantOut = true;

            //th20110411 หากเรียกครั้งแรก ให้ default ค่าวันที่เป็นเดือนปัจจุบัน
            dtpSelectRange = 2;
        }
    }
}