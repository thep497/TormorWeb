using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tormor.DomainModel;

namespace Tormor.Web.Models
{
    public class SSSearchRepository : Tormor.DomainModel.ISearchRepository
    {
        public AlienSearchInfo AlienSearch
        {
            get
            {
                return (AlienSearchInfo)HttpContext.Current.Session["AlienSearchInfo"];
            }
            set
            {
                HttpContext.Current.Session["AlienSearchInfo"] = value;
            }
        }

        public ConveyanceSearchInfo ConveyanceSearch
        {
            get
            {
                return (ConveyanceSearchInfo)HttpContext.Current.Session["ConveyanceSearchInfo"];
            }
            set
            {
                HttpContext.Current.Session["ConveyanceSearchInfo"] = value;
            }
        }

    }
}