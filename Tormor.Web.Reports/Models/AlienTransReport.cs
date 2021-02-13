using NNS.GeneralHelpers;
using Tormor.DomainModel;

namespace Tormor.Web.Reports.Models
{
    public class AlienTransReport 
    {
        public string TypeName { get; set; }
        public string Code { get; set; }
        public string RequestDate { get; set; }

        public string FullName { get; set; }
        public string PassportCard { get; set; }

        public string DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string Nationality { get; set; }

        public int Age { get; set; }
        public int RequestAge { get; set; }

        public string Invoice_InvoiceNo { get; set; }
        public decimal Invoice_Charge { get; set; }
        public string DateArrive { get; set; }
        public string PermitToDate { get; set; }

        public PhotoImage Photo { get; set; }

        public AlienTransReport(AlienTransaction aTran, string dateFmt,bool loadPhoto=false)
        {
            if (aTran != null)
            {
                this.TypeName = aTran.TypeName;
                this.Code = aTran.Code;
                this.RequestDate = aTran.RequestDate.ToString(dateFmt);

                this.FullName = aTran.Alien.Name.FullName;
                this.PassportCard = aTran.PassportCard.DocNo;

                this.DateOfBirth = aTran.Alien.DateOfBirth.ToString(dateFmt);
                this.Sex = aTran.Alien.Sex;
                this.Nationality = aTran.Alien.Nationality;

                this.Age = aTran.Age;
                this.RequestAge = aTran.RequestAge;

                if (aTran.Invoice != null)
                {
                    this.Invoice_InvoiceNo = aTran.Invoice.InvoiceNo;
                    this.Invoice_Charge = aTran.Invoice.Charge ?? 0m;
                }
                this.PermitToDate = aTran.PermitToDate.ToString(dateFmt);
                this.DateArrive = aTran.DateArrive.ToString(dateFmt);

                if (loadPhoto)
                    this.Photo = aTran.Alien.Photo;
            }
        }
    }
}
