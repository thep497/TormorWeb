using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Tormor.DomainModel
{
    [MetadataType(typeof(PersonName_Validation))]
    [DisplayColumn("FullName")]
    public partial class PersonName
    {
        //[ScaffoldColumn(false)]
        [DisplayName("ชื่อ-สกุล")]
        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(Title))
                    return FirstName + " " + MiddleName + " " + LastName;
                else
                    return Title + " " + FirstName + " " + MiddleName + " " + LastName;
            }
        }

        public class PersonName_Validation
        {
            [DisplayName("คำนำหน้าชื่อ")]
            [UIHint("CBNameTitle", "MVC")]
            public string Title { get; set; }

            [DisplayName("ชื่อ")]
            [Required(ErrorMessage = "กรุณากรอกชื่อบุคคล")]
            public string FirstName { get; set; }
            [DisplayName("ชื่อกลาง")]
            public string MiddleName { get; set; }
            [DisplayName("ชื่อสกุล")]
            public string LastName { get; set; }
        }

        public void CopyFrom(PersonName value)
        {
            this.FirstName = value.FirstName;
            this.LastName = value.LastName;
            this.MiddleName = value.MiddleName;
            this.Title = value.Title;
        }
    }

    [MetadataType(typeof(IDDocument_Validation))]
    [DisplayColumn("DocNo")]
    public partial class IDDocument
    {
        public string FullDetail(string dateFmt=null)
        {
            var result = this.DocNo ?? "-";
            if (this.DateIssued != null)
                result += " ออกเมื่อ: " + (this.DateIssued ?? DateTime.Today).Date.ToString(dateFmt ?? "d");
            if (this.IssuedFrom != null)
                result += " ที่ " + this.IssuedFrom;
            return result;
        }
        public class IDDocument_Validation
        {
            [DisplayName("เลขที่")]
            public string DocNo { get; set; }

            [DisplayName("สถานที่ออกบัตร")]
            public string IssuedFrom { get; set; }

            [DisplayName("วันออกบัตร")]
            public DateTime DateIssued { get; set; }
            
            [DisplayName("หมดอายุ")]
            public DateTime DateExpired { get; set; }
        }
        public void CopyFrom(IDDocument value)
        {
            this.DocNo = value.DocNo;
            this.IssuedFrom = value.IssuedFrom;
            this.DateIssued = value.DateIssued;
            this.DateExpired = value.DateExpired;
        }
    }

    [MetadataType(typeof(Address_Validation))]
    [DisplayColumn("FullAddress")]
    public partial class Address
    {
        public string FullAddress
        {
            get
            {
                var result = this.AddrNo ?? "";
                if (this.Road != null) result += " ถ." + this.Road;
                if (this.Tumbol != null) result += " ต." + this.Tumbol;
                if (this.Amphur != null) result += " อ." + this.Amphur;
                if (this.Province != null) result += " จ." + this.Province;
                if (this.Postcode!= null) result += " " + this.Postcode;
                if (this.Phone != null) result += " โทร." + this.Phone;

                return result.Trim();
            }
        }

        public void CopyFrom(Address value)
        {
            this.AddrNo = value.AddrNo;
            this.Road = value.Road;
            this.Tumbol = value.Tumbol;
            this.Amphur = value.Amphur;
            this.Province = value.Province;
            this.Postcode = value.Postcode;
            this.Phone = value.Phone;
        }
        public class Address_Validation
        {
            [DisplayName("เลขที่/ที่อยู่")]
            public string AddrNo { get; set; }

            [DisplayName("ถนน")]
            public string Road { get; set; }

            [DisplayName("ตำบล")]
            public string Tumbol { get; set; }

            [DisplayName("อำเภอ")]
            public string Amphur { get; set; }

            [DisplayName("จังหวัด")]
            [UIHint("CBProvince", "MVC")]
            public string Province { get; set; }
            
            [DisplayName("รหัสไปรษณีย์")]
            public string Postcode { get; set; }
            
            [DisplayName("เบอร์ติดต่อ")]
            public string Phone { get; set; }
        }
    }

    [MetadataType(typeof(InvoiceInfo_Validation))]
    public partial class InvoiceInfo
    {
        public class InvoiceInfo_Validation
        {
            [DisplayName("เลขที่ใบเสร็จ")]
            public string InvoiceNo { get; set; }

            [DisplayName("จำนวนเงิน")]
            public Decimal? Charge { get; set; }
        }
    }

    [MetadataType(typeof(OutDetail_Validation))]
    public partial class OutDetail
    {
        public class OutDetail_Validation
        {
            [DisplayName("ไปไหน")]
            public string Destination { get; set; }

            [DisplayName("โดยพาหนะ")]
            public string ByVehicle { get; set; }

            [DisplayName("วันที่ไป")]
            public DateTime OutDate { get; set; }
        }
    }

    [MetadataType(typeof(LogInfo_Validation))]
    public partial class LogInfo
    {
        public void LogAdded(string userName)
        {
            if ((this.AddedBy ?? "") == "")
            {
                this.AddedBy = userName;
                this.AddedDate = DateTime.Now;
            }
        }
        public void LogUpdated(string userName)
        {
            this.UpdatedBy = userName;
            this.UpdatedDate = DateTime.Now;
        }
        public class LogInfo_Validation
        {
            [DisplayName("เพิ่มโดย")]
            public string AddedBy { get; set; }
            [DisplayName("เพิ่มเมื่อ")]
            public DateTime AddedDate { get; set; }
            [DisplayName("แก้ไขโดย")]
            public string UpdatedBy { get; set; }
            [DisplayName("แก้ไขเมื่อ")]
            public DateTime UpdatedDate { get; set; }
        }
    }

    public partial class PhotoImage
    {
        public void CopyFrom(PhotoImage value)
        {
            this.ContentType = value.ContentType;
            this.FPicture = value.FPicture;
        }
    }
    [MetadataType(typeof(PortDetail_Validation))]
    public partial class PortDetail
    {
        [DisplayName("ท่าเรือ")]
        public string PortNameCountry
        {
            get
            {
                return (PortName ?? "") + " " + (Country ?? "");
            }
        }
        public class PortDetail_Validation
        {
            [DisplayName("ประเทศ")]
            public string Country { get; set; }
            [DisplayName("ชื่อท่าเรือ")]
            public string PortName { get; set; }

        }
    }

    [MetadataType(typeof(CrewInOut_Validation))]
    public partial class CrewInOut
    {
        public string Detail(string dateFmt)
        {
            return ((InDate == null) ? "" : (InDate ?? DateTime.Today).ToString(dateFmt)+"\n\r") +
                (string.IsNullOrEmpty(InMethod) ? "" : InMethod+" / ") +
                (string.IsNullOrEmpty(InWay) ? "" : InWay);
        }
        public class CrewInOut_Validation
        {
            [DisplayName("วันที่")]
            public DateTime? InDate { get; set; }
            [DisplayName("โดย")]
            public string InMethod { get; set; }
            [DisplayName("ด่าน/เที่ยวบิน")]
            public string InWay { get; set; }
        }
    }
}
