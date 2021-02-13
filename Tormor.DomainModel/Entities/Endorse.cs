using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tormor.DomainModel
{
    [MetadataType(typeof(Endorse_Validation))]
    public partial class Endorse
    {
        public Endorse()
        {
            //ใส่ค่า default ที่จะให้ show ใน form กรณีที่สร้าง record ใหม่ (อีกส่วนหนึ่งอยู่ใน DoNewRecord ใน repo)
            this.RequestDate = DateTime.Today;
        }

        public class Endorse_Validation
        {
            [Required(ErrorMessage = "กรุณากรอกลำดับที่ตามลำดับในเล่มจริง")]
            [DisplayName("ลำดับที่")]
            public string Code { get; set; }

            [DisplayName("ผู้ยื่นคำร้อง")]
            public string AlienId { get; set; }

            [Required(ErrorMessage = "กรุณากรอกวันที่สลักหลัง")]
            [DisplayName("สลักหลังเมื่อ")]
            public DateTime RequestDate { get; set; }

            [Required(ErrorMessage = "กรุณากรอกชนิดตม.")]
            [DisplayName("ชนิดตม.")]
            public string TMType { get; set; }

            //[Required(ErrorMessage = "กรุณากรอกวันที่หมดอายุ")]
            [DisplayName("หมดอายุ")]
            public DateTime ExpiredDate { get; set; }

            [DisplayName("การตรวจลงตรา")]
            public string EndorseStamps { get; set; }
        }
    }
    [MetadataType(typeof(EndorseStamp_Validation))]
    public partial class EndorseStamp
    {
        public EndorseStamp()
        {
            //ใส่ค่า default ที่จะให้ show ใน form กรณีที่สร้าง record ใหม่ (อีกส่วนหนึ่งอยู่ใน DoNewRecord ใน repo)
            this.StampDate = DateTime.Today;
        }
        public EndorseStamp(int endorseId) : this()
        {
            this.EndorseId = endorseId;
        }
        public class EndorseStamp_Validation
        {
            [Required(ErrorMessage = "กรุณากรอกลำดับการตรวจลงตรา")]
            [DisplayName("ลำดับที่")]
            public string Code { get; set; }

            [DisplayName("วันที่")]
            public DateTime StampDate { get; set; }

            [DisplayName("วันหมดอายุ")]
            public DateTime StampExpiredDate { get; set; }

            [DisplayName("เลขที่ใบเสร็จรับเงิน")]
            public InvoiceInfo Invoice { get; set; }

            [Required(ErrorMessage = "กรุณากรอกครั้ง")]
            [DisplayName("ครั้ง")]
            public string SMTime { get; set; }
        }
    }
}
