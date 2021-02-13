using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Tormor.DomainModel
{
    [MetadataType(typeof(Conveyance_Validation))]
    public partial class Conveyance
    {

        public class Conveyance_Validation
        {
            [Required(ErrorMessage = "กรุณากรอกชื่อพาหนะ")]
            [DisplayName("ชื่อพาหนะ")]
            public string Name { get; set; }

            [DisplayName("หมายเลขทะเบียน")]
            public string RegistrationNo { get; set; }

            [Required(ErrorMessage = "กรุณากรอกชื่อเจ้าของพาหนะ")]
            [DisplayName("ชื่อเจ้าของพาหนะ")]
            public string OwnerName { get; set; }

        }
    }
}
