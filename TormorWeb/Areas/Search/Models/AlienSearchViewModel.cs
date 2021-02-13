using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Tormor.DomainModel;

namespace Tormor.Web.Models
{
    public class AlienSearchViewModel
    {
        public AlienSearchInfo SearchInfo { get; set; }
        public IEnumerable<AlienTransaction> AlienTrans { get; set; }

        public AlienSearchViewModel()
        {
        }

        public AlienSearchViewModel(AlienSearchInfo searchInfo, IEnumerable<AlienTransaction> aTrans)
        {
            this.SearchInfo = searchInfo;
            this.AlienTrans = aTrans;
        }

    }
}