using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Tormor.DomainModel
{
    [MetadataType(typeof(zz_Reference_Validation))]
    public partial class zz_Reference
    {
        private IReferenceRepository repo = new EFReferenceRepository();

        #region Extended Fields
        [UIHint("ClientRefRefNameCB" , "MVC")]
        public string RefRefName
        {
            get
            {
                var rrName = (from r in repo.FindAll(this.RefRefTypeId ?? 0)
                              where r.Code == this.RefCode 
                              select r.RefName).DefaultIfEmpty("").First();

                return rrName;
            }
            set
            {
                this.RefCode = (from r in repo.FindAll(this.RefRefTypeId ?? 0)
                                where r.RefName == value
                                select r.Code).FirstOrDefault();
            }
        }
        #endregion

        public class zz_Reference_Validation
        {
            //ไม่ต้องแสดง ถ้าไม่ใส่โปรแกรมจะใส่ให้เอง [Required(ErrorMessage = "Code is Required")]
            [DisplayName("รหัส")]
            public string Code { get; set; }

            [Required(ErrorMessage = "RefName is Required")]
            [DisplayName("ความหมาย")]
            public string RefName { get; set; }

            [DisplayName("คำอธิบาย")]
            public string RefDesc { get; set; }

            [DisplayName("อ้างอิง")]
            public string RefRefName { get; set; }
        }
    }
}
