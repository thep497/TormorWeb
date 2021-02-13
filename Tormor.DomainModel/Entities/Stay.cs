using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tormor.DomainModel
{
    [MetadataType(typeof(Staying90Day_Validation))]
    public partial class Staying90Day
    {
        public Staying90Day()
        {
            //ใส่ค่า default ที่จะให้ show ใน form กรณีที่สร้าง record ใหม่ (อีกส่วนหนึ่งอยู่ใน DoNewRecord ใน repo)
            this.RequestDate = DateTime.Today;
        }

        public class Staying90Day_Validation
        {
            //เฉพาะอันนี้ Auto Run ตามปี (ซ่อน ?)
            [DisplayName("ลำดับที่")]
            public string Code { get; set; }

            [DisplayName("ผู้ยื่นคำร้อง")]
            public int AlienId { get; set; }

            [Required(ErrorMessage = "กรุณากรอกวันที่รับคำร้อง")]
            [DisplayName("วันที่รับคำร้อง")]
            public DateTime RequestDate { get; set; }

            [DisplayName("เดินทางเข้ามาเมื่อ")]
            public DateTime ArrivalDate { get; set; }

            [DisplayName("โดยพาหนะ")]
            public string ArrivalBy { get; set; }

            [Required(ErrorMessage = "กรุณากรอกเลขที่บัตรขาเข้า")]
            [DisplayName("บัตรขาเข้าเลขที่")]
            public IDDocument ArrivalCard { get; set; }

            [DisplayName("ประเภทวีซ่า")]
            public int VisaType { get; set; }

        }
    }
}
