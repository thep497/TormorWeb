using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tormor.DomainModel;

namespace Tormor.Web.Models
{
    public class ConveyanceSearchViewModel
    {        
        public ConveyanceSearchInfo SearchInfo { get; set; }
        public IEnumerable<ConveyanceInOut> ConvInOuts { get; set; }

        public ConveyanceSearchViewModel()
        {
        }

        public ConveyanceSearchViewModel(ConveyanceSearchInfo searchInfo, IEnumerable<ConveyanceInOut> cio)
        {
            this.SearchInfo = searchInfo;
            this.ConvInOuts = cio;
        }

    }
}