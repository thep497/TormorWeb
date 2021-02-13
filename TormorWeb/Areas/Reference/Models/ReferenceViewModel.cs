using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tormor.DomainModel;
using System.ComponentModel.DataAnnotations;

namespace Tormor.Web.Models
{
    public class ReferenceViewModel
    {
        private IReferenceRepository refRepo;
        private int _refTypeId;

        /// <summary>
        /// constucture for the class
        /// </summary>
        /// <param name="referenceRepository"></param>
        /// <param name="refTypeId"></param>
        public ReferenceViewModel(IReferenceRepository referenceRepository,int refTypeId)
        {
            this.refRepo = referenceRepository;
            _refTypeId = refTypeId;
        }

        public int RefTypeId { get { return _refTypeId; } }
        public bool HasReference
        {
            get
            {
                zz_Reference reference = refRepo.GetOne(0, _refTypeId.ToString());
                if (reference != null)
                {
                    if ((reference.RefRefTypeId == null) || (reference.RefRefTypeId == 0))
                        return false;
                    return true;
                }
                return false;
            }
        }
        public string RefTypeName
        {
            get
            {
                zz_Reference reference = refRepo.GetOne(0, _refTypeId.ToString());
                if (reference != null)
                    return reference.RefName;
                return "Not Defined";
            }
        }
        public IEnumerable<zz_Reference> References
        {
            get
            {
                return refRepo.FindAll(_refTypeId).ToList(); //th20101126 ต้องใส่ tolist ด้วย ไม่งั้นจะ error (The specified type member is not supported in LINQ to Entities. Only initializers, entity members, and entity navigation properties are supported)
            }
        }
    }
}