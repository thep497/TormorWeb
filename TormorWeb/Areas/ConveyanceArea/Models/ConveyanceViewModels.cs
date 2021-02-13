using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tormor.DomainModel;
using System.ComponentModel.DataAnnotations;

namespace Tormor.Web.Models
{
    [MetadataType(typeof(Tormor.DomainModel.Conveyance.Conveyance_Validation))]
    public class ConveyanceLite
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public string RegistrationNo { get; set; }
        public LogInfo UpdateInfo { get; set; }
        public bool IsCancel { get; set; }

        public ConveyanceLite()
        {
        }

        public ConveyanceLite(Conveyance conv)
        {
            this.CopyFrom(conv);
        }

        public void CopyFrom(Conveyance conv)
        {
            if (conv != null)
            {
                this.Id = conv.Id;
                this.Name = conv.Name;
                this.OwnerName = conv.OwnerName;
                this.RegistrationNo = conv.RegistrationNo;
                this.UpdateInfo = conv.UpdateInfo;
                this.IsCancel = conv.IsCancel;
            }
        }
    }

    public class ConveyanceViewModel
    {
        public ConveyanceLite Conveyance;

        public ConveyanceViewModel()
        {
            this.Conveyance = new ConveyanceLite();
        }
        public ConveyanceViewModel(Conveyance conv)
        {
            this.Conveyance = new ConveyanceLite();
            this.Conveyance.CopyFrom(conv);
        }
    }
}