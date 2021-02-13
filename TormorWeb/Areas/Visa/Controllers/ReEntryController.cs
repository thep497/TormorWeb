using System;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NNS.Config;
using NNS.CustomEvents;
using NNS.GeneralHelpers;
using NNS.ModelHelpers;
using NNS.MVCHelpers;
using Telerik.Web.Mvc.Extensions;
using Tormor.DomainModel;

namespace Tormor.Web.Areas.Visa.Controllers
{
    [Authorize]
    public class ReEntryController : Controller
    {
        private IReEntryRepository reentryRepo;
        private IVisaRepository visaRepo;
        private IReferenceRepository refRepo;

        public ReEntryController(IReEntryRepository reentryRepository, IVisaRepository visaRepository, 
            IReferenceRepository referenceRepository)
        {
            this.reentryRepo = reentryRepository;
            this.refRepo = referenceRepository;
            this.visaRepo = visaRepository;
        }

        //
        // GET: /ReEntry/

        public ActionResult Index(int? dtpSelectRange, DateTime? dtpFromDate, DateTime? dtpToDate)
        {
            ToolbarMenuHelpers.SetToolBar(ViewData,
                new
                {
                    New = "Insert",
                    Date = "Index"
                },
                "ReEntry",
                null,
                null,
                ref dtpSelectRange,
                ref dtpFromDate, ref dtpToDate);

            return View(reentryRepo.FindAll(dtpFromDate, dtpToDate).ToList());
        }

        public ActionResult Index2(int? dtpSelectRange, DateTime? dtpFromDate, DateTime? dtpToDate)
        {
            return RedirectToAction("Index", "Search", new { area = "Search" });
        }

        public ActionResult Insert()
        {
            return doInsert(new ReEntry());
        }

        private ActionResult doInsert(ReEntry reentry)
        {
            ToolbarMenuHelpers.SetToolBar(ViewData,
                new { Save = "btnSave", New = "Insert", Close = "Index2" }, "ReEntry");

            //makeReferenceViewData();
            return View(reentry);
        }

        public ActionResult Edit(int id)
        {
            var reentry = reentryRepo.GetOne(id);
            return doEdit(reentry);
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

        #region Make References Combobox
        [HttpPost]
        public ActionResult _GetSMMoney(string text)
        {
            zz_Reference result;
            if (text.HasValue())
            {
                string refCode = refRepo.GetOneByName(10, text).Code;
                result = refRepo.FindAll(11).FirstOrDefault(c => c.RefCode == refCode);
            }
            else
                result = refRepo.FindAll(11).FirstOrDefault();

            var result_str = "";
            if (result != null)
                result_str = result.RefName;

            return new JsonResult
            {
                Data = result_str
            };
        }

        [HttpPost]
        public ActionResult _GetReEntryCodeDetail(int id, string code,string xdate)
        {
            ReEntry reentry = null;
            try
            {
                DateTime requestdate = DateTime.ParseExact(xdate, Globals.DateFormat, CultureInfo.InvariantCulture);
                reentry = reentryRepo.GetOneByCode(code, requestdate, id);
                if (reentry != null)
                {
                    return new JsonResult
                    {
                        Data = new
                        { 
                            dupcode = true,
                            code = code,
                            rdate = reentry.RequestDate.ToString(Globals.DateFormat),
                            id = reentry.Id,
                            name = reentry.Alien.Name.FullName
                        }
                    };
                }
            }
            catch (FormatException)
            {
            }

            return new JsonResult { Data = new { dupcode = false } };
        }

        [HttpPost]
        public ActionResult _GetLastPermitDate(int alienid, string rdate, string pdate)
        {
            DateTime? result = null;
            try
            {
                DateTime requestDate = DateTime.ParseExact(rdate, Globals.DateFormat, CultureInfo.InvariantCulture);
                DateTime permitToDate = DateTime.ParseExact(pdate, Globals.DateFormat, CultureInfo.InvariantCulture);
                //หาข้อมูล PermitToDate จาก visa ล่าสุด
                var res = (from p in visaRepo.FindAll(null, null)
                           where (p.RequestDate <= requestDate) &&
                           (p.AlienId == alienid)
                           orderby p.RequestDate descending
                           select p.PermitToDate).FirstOrDefault();
//                result = res ?? permitToDate;
                result = res;
            }
            catch (FormatException)
            {
            }

            return new JsonResult { Data = new { permittodate = result } };
        }

        #endregion

        private ActionResult doEdit(ReEntry reentry)
        {
            ToolbarMenuHelpers.SetToolBar(ViewData,
                new { Save = "btnSave", New = "Insert", GiveUp = "btnDelete", Close = "Index2" }, "ReEntry");
            
            //ส่งผ่านตัวแปรเพื่อทำ combobox
            //makeReferenceViewData();

            if (reentry != null)
                return View(reentry);
            else
            {
                TempData.AddError(Resources.Messages.NotFoundData);
                return RedirectToAction("Index2");
            }
        }

        #region Server CRUD Section...
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Insert(ReEntry reentry ,bool wantClose)
        {
            //ReEntry reentry = new ReEntry();
            this.reentryRepo.CurrentUserName = HttpContext.User.Identity.Name;
            try
            {
                reentryRepo.DoNewRecord(reentry);
                UpdateModel(reentry);
                reentryRepo.DoSave(reentry, true);
                TempData.AddInfo(Resources.Messages.SaveSuccess);
                if (!wantClose)
                    return RedirectToAction("Edit", new { id = reentry.Id });
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

            return doInsert(reentry);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, bool wantClose)
        {
            var reentry = reentryRepo.GetOne(id);
            this.reentryRepo.CurrentUserName = HttpContext.User.Identity.Name;
            try
            {
                UpdateModel(reentry);
                reentryRepo.DoSave(reentry, false);
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

            return doEdit(reentry);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            ReEntry reentry = reentryRepo.GetOne(id);
            if (reentry != null)
            {
                try
                {
                    //Delete the record                
                    reentryRepo.DoDelete(reentry);
                    new RecordDeletedEvent("ReEntry", reentry.Id, 0, reentry.Code, reentry.Alien.Name.FullName, null).Raise();
                    TempData.AddInfo(Resources.Messages.DeleteSuccess+" ("+reentry.Code+"-"+reentry.Alien.Name.FullName+")");
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
