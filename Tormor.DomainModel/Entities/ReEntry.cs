using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tormor.DomainModel
{
    [MetadataType(typeof(ReEntry_Validation))]
    public partial class ReEntry
    {
        public ReEntry()
        {
            //ใส่ค่า default ที่จะให้ show ใน form กรณีที่สร้าง record ใหม่ (อีกส่วนหนึ่งอยู่ใน DoNewRecord ใน repo)
            this.RequestDate = DateTime.Today;
        }

        public class ReEntry_Validation
        {
            [Required(ErrorMessage = "กรุณากรอกลำดับที่ตามลำดับในเล่มจริง")]
            [DisplayName("ลำดับที่")]
            public string Code { get; set; }

            [DisplayName("ผู้ยื่นคำร้อง")]
            public string AlienId { get; set; }

            [Required(ErrorMessage = "กรุณากรอกวันที่รับคำร้อง")]
            [DisplayName("วันที่รับคำร้อง")]
            public DateTime RequestDate { get; set; }

            [Required(ErrorMessage = "กรุณากรอกรหัส")]
            [DisplayName("รหัส")]
            public string ReEntryCode { get; set; }

            [DisplayName("ครั้ง")]
            public string SMTime { get; set; }

            [DisplayName("เลขที่ใบเสร็จรับเงิน")]
            public InvoiceInfo Invoice { get; set; }

            //[Required(ErrorMessage = "กรุณากรอกวันที่อนุญาตถึง")]
            [DisplayName("อนุญาตถึง")]
            public DateTime PermitToDate { get; set; }

        }
    }
}
