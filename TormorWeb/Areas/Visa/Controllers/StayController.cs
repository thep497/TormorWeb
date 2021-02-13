using System;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NNS.Config;
using NNS.CustomEvents;
using NNS.ModelHelpers;
using NNS.MVCHelpers;
using Telerik.Web.Mvc.Extensions;
using Tormor.DomainModel;
using NNS.GeneralHelpers;

namespace Tormor.Web.Areas.Visa.Controllers
{
    [Authorize]
    public class StayController : Controller
    {
        private IStayRepository stayRepo;
        private IReferenceRepository refRepo;

        public StayController(IStayRepository stayRepository, IReferenceRepository referenceRepository)
        {
            this.stayRepo = stayRepository;
            this.refRepo = referenceRepository;
        }

        //
        // GET: /Stay/Stay/
        public ActionResult Index(int? dtpSelectRange, DateTime? dtpFromDate, DateTime? dtpToDate)
        {
            ToolbarMenuHelpers.SetToolBar(ViewData, new { New = "Insert", Date = "Index" }, "Stay",
                null, null,  ref dtpSelectRange, ref dtpFromDate, ref dtpToDate);

            return View(stayRepo.FindAll(dtpFromDate, dtpToDate).ToList());
        }

        public ActionResult Index2(int? dtpSelectRange, DateTime? dtpFromDate, DateTime? dtpToDate)
        {
            return RedirectToAction("Index", "Search", new { area = "Search" });
        }

        public ActionResult Insert()
        {
            return doInsert(new Staying90Day());
        }

        private ActionResult doInsert(Staying90Day stay)
        {
            ToolbarMenuHelpers.SetToolBar(ViewData,
                new { Save = "btnSave", New = "Insert", Close = "Index2" }, "Stay");

            //makeReferenceViewData();
            stay.Code = stayRepo.GetNewCode();
            return View(stay);
        }

        public ActionResult Edit(int id)
        {
            var stay = stayRepo.GetOne(id);
            return doEdit(stay);
        }

        /// <summary>
        /// ใช้สำหรับ reload กรณีที่ user กรอก code ซ้ำ เพราะไม่สามารถเรียก function Edit เดิม (ใน Url.Action) ได้ (ของเดิมมี id อยู่แล้ว)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ReEdit(int id)
        {
            return RedirectToAction("Edit", new { id });
        }

        private ActionResult doEdit(Staying90Day stay)
        {
            ToolbarMenuHelpers.SetToolBar(ViewData,
                new { Save = "btnSave", New = "Insert", GiveUp = "btnDelete", Close = "Index2" }, "Stay");
            
            //ส่งผ่านตัวแปรเพื่อทำ combobox
            //makeReferenceViewData();

            if (stay != null)
                return View(stay);
            else
            {
                TempData.AddError(Resources.Messages.NotFoundData);
                return RedirectToAction("Index2");
            }
        }

        #region Server CRUD Section...
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Insert(Staying90Day stay ,bool wantClose)
        {
            //Staying90Day stay = new Staying90Day();
            this.stayRepo.CurrentUserName = HttpContext.User.Identity.Name;
            try
            {
                stayRepo.DoNewRecord(stay);
                UpdateModel(stay);
                stayRepo.DoSave(stay, true);
                TempData.AddInfo(Resources.Messages.SaveSuccess);
                if (!wantClose)
                    return RedirectToAction("Edit", new { id = stay.Id });
                return RedirectToAction("Index2");
            }
            catch (RulesException ex)
            {
                ex.CopyTo(ModelState);
                TempData.AddWarning(Resources.Messages.SaveError + ex.ExMessage());
            }
            catch (Exception ex)
            {
                TempData.AddWarning(Resources.Messages.SaveError + ex.ExMessage());
            }

            return doInsert(stay);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, bool wantClose)
        {
            var stay = stayRepo.GetOne(id);
            this.stayRepo.CurrentUserName = HttpContext.User.Identity.Name;
            try
            {
                UpdateModel(stay);
                stayRepo.DoSave(stay, false);
                TempData.AddInfo(Resources.Messages.SaveSuccess);
                if (!wantClose)
                    return RedirectToAction("Edit", new { id = id });
                return RedirectToAction("Index2");
            }
            catch (RulesException ex)
            {
                ex.CopyTo(ModelState);
                TempData.AddWarning(Resources.Messages.SaveError + ex.ExMessage());
            }
            catch (Exception ex)
            {
                TempData.AddWarning(Resources.Messages.SaveError + ex.ExMessage());
            }

            return doEdit(stay);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Staying90Day stay = stayRepo.GetOne(id);
            if (stay != null)
            {
                try
                {
                    //Delete the record                
                    stayRepo.DoDelete(stay);
                    new RecordDeletedEvent("Staying90Day", stay.Id, 0, stay.Code, stay.Alien.Name.FullName, null).Raise();
                    TempData.AddInfo(Resources.Messages.DeleteSuccess+" ("+stay.Code+"-"+stay.Alien.Name.FullName+")");
                }
                catch (Exception ex)
                {
                    TempData.AddError(ex.ExMessage());
                    return RedirectToAction("Edit", new { id = id });
                }
            }
            else 
                TempData.AddError(Resources.Messages.NotFoundData);

            return RedirectToAction("Index2");
        }
        #endregion
    }
}
