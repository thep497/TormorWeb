using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tormor.DomainModel
{
    [MetadataType(typeof(VisaDetail_Validation))]
    public partial class VisaDetail
    {
        public VisaDetail()
        {
            //ใส่ค่า default ที่จะให้ show ใน form กรณีที่สร้าง record ใหม่ (อีกส่วนหนึ่งอยู่ใน DoNewRecord ใน repo)
            this.RequestDate = DateTime.Today;
        }

        public class VisaDetail_Validation
        {
            [Required(ErrorMessage = "กรุณากรอกลำดับที่ตามลำดับในเล่มจริง")]
            [DisplayName("ลำดับที่")]
            public string Code { get; set; }

            [DisplayName("ผู้ยื่นคำร้อง")]
            public int AlienId { get; set; }

            [Required(ErrorMessage = "กรุณากรอกวันที่รับคำร้อง")]
            [DisplayName("วันที่รับคำร้อง")]
            public DateTime RequestDate { get; set; }

            [DisplayName("วันที่นัดฟังผล")]
            public DateTime ResultAppointmentDate { get; set; }

            [DisplayName("เดินทางเข้ามาเมื่อ")]
            public DateTime DateArrive { get; set; }

            [DisplayName("อนุญาตถึง")]
            public DateTime PermitToDate { get; set; }

            [Required(ErrorMessage = "กรุณากรอกระยะเวลา")]
            [DisplayName("ระยะเวลา")]
            public string StayPeriod { get; set; }
        
            [Required(ErrorMessage = "กรุณากรอกเหตุผล")]
            [DisplayName("เหตุผลที่ขอ")]
            public string StayReason { get; set; }

            [Required(ErrorMessage = "กรุณากรอกรายละเอียดเหตุผล")]
            [DisplayName("เหตุผล (รายละเอียด)")]
            public string StayReasonDetail { get; set; }

            [DisplayName("ผลการพิจารณา")]
            [UIHint("CBIsPermit", "MVC")]
            public bool? IsPermit { get; set; }

            [DisplayName("เลขที่ใบเสร็จรับเงิน")]
            public InvoiceInfo Invoice { get; set; }

        }
    }
}
