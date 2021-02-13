using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using NNS.GeneralHelpers;

namespace Tormor.DomainModel
{
    [MetadataType(typeof(AddRemoveCrew_Validation))]
    public partial class AddRemoveCrew
    {
        public AddRemoveCrew()
        {
        }
        public AddRemoveCrew(int addRemoveType) : this()
        {
            this.AddRemoveType = addRemoveType;
        }

        [DisplayName("อายุ")]
        public int AlienAge
        {
            get
            {
                return (this.Alien.DateOfBirth ?? DateTime.Today).CalcAgeYear();
            }
        }

        public class AddRemoveCrew_Validation
        {
            [DisplayName("วันที่แจ้ง")]
            public DateTime RequestDate { get; set; }

            [DisplayName("ลำดับ")]
            public string Code { get; set; }

            [DisplayName("ลำดับย่อย")]
            public string SubCode { get; set; }

            [DisplayName("ข้อมูลบุคคล")]
            public int AlienId { get; set; }

            [DisplayName("บริษัท")]
            public string Company { get; set; }

            [DisplayName("เข้า")]
            public CrewInOut InDetail { get; set; }

            [DisplayName("ออก")]
            public CrewInOut OutDetail { get; set; }
        }
    }
}
