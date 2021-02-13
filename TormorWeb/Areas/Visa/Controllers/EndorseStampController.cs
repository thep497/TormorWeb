using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Tormor.Web.Models;
using Tormor.DomainModel;
using Telerik.Web.Mvc.Extensions;
using NNS.ModelHelpers;
using NNS.GeneralHelpers;
using NNS.MVCHelpers;
using NNS.CustomEvents;
using NNS.Config;

namespace Tormor.Web.Areas.Visa.Controllers
{
    [Authorize]
    public class EndorseStampController : Controller
    {
        private IEndorseStampRepository endorseStampRepo;
        private IReferenceRepository refRepo;

        public EndorseStampController(IEndorseStampRepository endorseStampRepository,IReferenceRepository referenceRepository)
        {
            this.endorseStampRepo = endorseStampRepository;
            this.refRepo = referenceRepository;
        }

        #region Make References Combobox
        [HttpPost]
        public ActionResult _GetSMMoney(string text)
        {
            zz_Reference result;
            if (text.HasValue())
            {
                string refCode = refRepo.GetOneByName(13, text).Code;
                result = refRepo.FindAll(14).FirstOrDefault(c => c.RefCode == refCode);
            }
            else
                result = refRepo.FindAll(14).FirstOrDefault();

            var result_str = "";
            if (result != null)
                result_str = result.RefName;

            return new JsonResult
            {
                Data = result_str
            };
        }
        #endregion
        #region Detail (EndorseStamp) Ajax CRUD
        /// <summary>
        /// Ajax Get EndorseStamp
        /// </summary>
        /// <param name="endorseId">id ของตัวแม่ (endorseId)</param>
        /// <returns></returns>
        [GridAction]
        public ActionResult _GetEndorseStamps(int endorseId)
        {
            EndorseStampViewModel esvm = new EndorseStampViewModel(endorseId, endorseStampRepo.FindAll(endorseId).ToList());
            return View(new GridModel(esvm.vmEndorseStamps));
        }
        public ActionResult _InsertEndorseStamp(int endorseid)
        {
            if (endorseid <= 0) // ยังไม่ได้ Save Master
            {
                return Content(ControllerHelper.ModalContent("กรุณากดปุ่ม Save ด้านบนของเอกสารหลักก่อนค่ะ"));
            }
            ViewData["isCreate"] = true;
            return PartialView("EndorseStampEdit", new EndorseStamp(endorseid));
            //return Content(this.RenderPartialViewToString("EndorseStampEdit", new EndorseStamp(endorseid)));
        }
        public ActionResult _EditEndorseStamp(int endorseid, int endorsestampid)
        {
            ViewData["isCreate"] = false;
            var eStamp = endorseStampRepo.GetOne(endorseid, endorsestampid);
            if (eStamp != null)
                return PartialView("EndorseStampEdit", eStamp);
            //return Content(this.RenderPartialViewToString("EndorseStampEdit", eStamp));
            return Content(ControllerHelper.ModalContent("Cannot find the detail..."));
        }

        [HttpPost]
        public ActionResult _SaveEndorseStamp(bool isCreate, int endorseId, int id = 0)
        {
            EndorseStamp endorseStamp;
            if (!isCreate && id > 0)
            {
                endorseStamp = endorseStampRepo.GetOne(endorseId, id);
            }
            else
            {
                endorseStamp = new EndorseStamp(endorseId);
            }
            this.endorseStampRepo.CurrentUserName = HttpContext.User.Identity.Name;
            try
            {
                if (isCreate)
                    endorseStampRepo.DoNewRecord(endorseStamp);
                UpdateModel(endorseStamp);
                endorseStampRepo.DoSave(endorseStamp, isCreate);

                return Content(Globals.ModalDetailUpdateOK);
            }
            catch (RulesException ex)
            {
                ex.CopyTo(ModelState);
            }
            catch
            {
                //do nothing
            }

            if (isCreate)
                return _InsertEndorseStamp(endorseId);
            return _EditEndorseStamp(endorseId, id);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult _DeleteEndorseStamp(int endorseId, int id)
        {
            EndorseStamp endorseStamp = endorseStampRepo.GetOne(endorseId, id);
            if (endorseStamp != null)
            {
                try
                {
                    //Delete the record                
                    endorseStampRepo.DoDelete(endorseStamp);
                    new RecordDeletedEvent("EndorseStamp", endorseStamp.Id, endorseId, endorseStamp.Code, null, null).Raise();
                }
                catch 
                {
                    return RedirectToAction("_EditEndorseStamp", new { endorseId, id});
                }
            }

            return Content(Globals.ModalDetailUpdateOK);
        }

        #endregion
    }
}
