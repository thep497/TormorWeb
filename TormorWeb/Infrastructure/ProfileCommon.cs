using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Profile;

namespace NNS.Config
{
    public class ProfileCommon : ProfileBase
    {
        #region Personal Data
        public virtual string FirstName
        {
            get
            {
                return ((string)(this.GetPropertyValue("FirstName")));
            }
            set
            {
                this.SetPropertyValue("FirstName", value);
            }
        }

        public virtual string LastName
        {
            get
            {
                return ((string)(this.GetPropertyValue("LastName")));
            }
            set
            {
                this.SetPropertyValue("LastName", value);
            }
        }

        public virtual string Position
        {
            get
            {
                return ((string)(this.GetPropertyValue("Position")));
            }
            set
            {
                this.SetPropertyValue("Position", value);
            }
        }

        public virtual string Phone
        {
            get
            {
                return ((string)(this.GetPropertyValue("Phone")));
            }
            set
            {
                this.SetPropertyValue("Phone", value);
            }
        }

        public virtual string Fax
        {
            get
            {
                return ((string)(this.GetPropertyValue("Fax")));
            }
            set
            {
                this.SetPropertyValue("Fax", value);
            }
        }
        #endregion

        #region Preference Data
        public virtual string Culture
        {
            get
            {
                return ((string)(this.GetPropertyValue("Culture")));
            }
            set
            {
                this.SetPropertyValue("Culture", value);
            }
        }

        public virtual int PageSize
        {
            get
            {
                return ((int)(this.GetPropertyValue("PageSize")));
            }
            set
            {
                this.SetPropertyValue("PageSize", value);
            }
        }

        public virtual int MainScreenHeight
        {
            get
            {
                return ((int)(this.GetPropertyValue("MainScreenHeight")));
            }
            set
            {
                this.SetPropertyValue("MainScreenHeight", value);
            }
        }
        #endregion

        public static ProfileCommon GetProfile(string username)
        {
            return ProfileBase.Create(username) as ProfileCommon;
        }
    }
}
