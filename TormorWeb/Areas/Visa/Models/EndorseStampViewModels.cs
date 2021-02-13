using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tormor.DomainModel;
using System.ComponentModel.DataAnnotations;

namespace Tormor.Web.Models
{
    [MetadataType(typeof(Tormor.DomainModel.EndorseStamp.EndorseStamp_Validation))]
    public class EndorseStampLite
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime? StampDate { get; set; }
        public DateTime? StampExpiredDate { get; set; }
        public InvoiceInfo Invoice { get; set; }
        public string SMTime { get; set; }
        public LogInfo UpdateInfo { get; set; }
        public bool IsCancel { get; set; }

        public EndorseStampLite()
        {
        }

        public EndorseStampLite(EndorseStamp eStamp)
        {
            this.CopyFrom(eStamp);
        }

        public void CopyFrom(EndorseStamp eStamp)
        {
            if (eStamp != null)
            {
                this.Id = eStamp.Id;
                this.Code = eStamp.Code;
                this.StampDate = eStamp.StampDate;
                this.StampExpiredDate = eStamp.StampExpiredDate;
                this.Invoice = eStamp.Invoice;
                this.SMTime = eStamp.SMTime;
                this.UpdateInfo = eStamp.UpdateInfo;
                this.IsCancel = eStamp.IsCancel;
            }
        }
    }

    public class EndorseStampViewModel
    {
        public EndorseStampViewModel(int endorseId)
        {
            this.vmEndorseId = endorseId;
            this.vmEndorseStamps = new List<EndorseStampLite>();
        }
        public EndorseStampViewModel(int endorseId, IEnumerable<EndorseStamp> eStamps)
            : this(endorseId)
        {
            foreach (var eStamp in eStamps)
            {
                this.vmEndorseStamps.Add(new EndorseStampLite(eStamp));
            }
        }

        public int vmEndorseId { get; set; }
        public IList<EndorseStampLite> vmEndorseStamps { get; set; }
    }
} 