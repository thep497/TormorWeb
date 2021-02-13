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
using System.Collections.Generic;
using Telerik.Web.Mvc;
using Tormor.Web.Models;

namespace Tormor.Web.Areas.Visa.Controllers
{
    [Authorize]
    public class EndorseController : Controller
    {
        private IEndorseRepository endorseRepo;

        public EndorseController(IEndorseRepository endorseRepository)
        {
            this.endorseRepo = endorseRepository;
        }

        //
        // GET: /Endorse/

        public ActionResult Index(int? dtpSelectRange, DateTime? dtpFromDate, DateTime? dtpToDate)
        {
            ToolbarMenuHelpers.SetToolBar(ViewData,
                new
                {
                    New = "Insert",
                    Date = "Index"
                },
                "Endorse",
                null,
                null,
                ref dtpSelectRange,
                ref dtpFromDate, ref dtpToDate);

            return View(endorseRepo.FindAll(dtpFromDate, dtpToDate).ToList());
        }

        public ActionResult Index2(int? dtpSelectRange, DateTime? dtpFromDate, DateTime? dtpToDate)
        {
            return RedirectToAction("Index", "Search", new { area = "Search" });
        }

        public ActionResult Insert()
        {
            return doInsert(new Endorse());
        }

        private ActionResult doInsert(Endorse endorse)
        {
            ToolbarMenuHelpers.SetToolBar(ViewData,
                new { Save = "btnSave", New = "Insert", Close = "Index2" }, "Endorse");

            //makeReferenceViewData();
            return View(endorse);
        }

        public ActionResult Edit(int id)
        {
            var endorse = endorseRepo.GetOne(id);
            return doEdit(endorse);
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

        private ActionResult doEdit(Endorse endorse)
        {
            ToolbarMenuHelpers.SetToolBar(ViewData,
                new { Save = "btnSave", New = "Insert", GiveUp = "btnDelete", Close = "Index2" }, "Endorse");

            //ส่งผ่านตัวแปรเพื่อทำ combobox
            //makeReferenceViewData();

            if (endorse != null)
                return View(endorse);
            else
            {
                TempData.AddError(Resources.Messages.NotFoundData);
                return RedirectToAction("Index2");
            }
        }

        [HttpPost]
        public ActionResult _GetEndorseCodeDetail(int id, string code,string xdate)
        {
            Endorse endorse = null;
            try
            {
                DateTime requestdate = DateTime.ParseExact(xdate, Globals.DateFormat, CultureInfo.InvariantCulture);
                endorse = endorseRepo.GetOneByCode(code, requestdate, id);
                if (endorse != null)
                {
                    return new JsonResult
                    {
                        Data = new
                        { 
                            dupcode = true,
                            code = code,
                            rdate = endorse.RequestDate.ToString(Globals.DateFormat),
                            id = endorse.Id,
                            name = endorse.Alien.Name.FullName
                        }
                    };
                }
            }
            catch (FormatException)
            {
            }

            return new JsonResult { Data = new { dupcode = false } };
        }

        #region Server CRUD Section...
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Insert(Endorse endorse ,bool wantClose)
        {
            //Endorse endorse = new Endorse();
            this.endorseRepo.CurrentUserName = HttpContext.User.Identity.Name;
            try
            {
                endorseRepo.DoNewRecord(endorse);
                UpdateModel(endorse);
                endorseRepo.DoSave(endorse, true);
                TempData.AddInfo(Resources.Messages.SaveSuccess);
                if (!wantClose)
                    return RedirectToAction("Edit", new { id = endorse.Id });
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

            return doInsert(endorse);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, bool wantClose)
        {
            var endorse = endorseRepo.GetOne(id);
            this.endorseRepo.CurrentUserName = HttpContext.User.Identity.Name;
            try
            {
                UpdateModel(endorse);
                endorseRepo.DoSave(endorse, false);
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

            return doEdit(endorse);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Endorse endorse = endorseRepo.GetOne(id);
            if (endorse != null)
            {
                try
                {
                    //Delete the record                
                    endorseRepo.DoDelete(endorse);
                    new RecordDeletedEvent("Endorse", endorse.Id, 0, endorse.Code, endorse.Alien.Name.FullName, null).Raise();
                    TempData.AddInfo(Resources.Messages.DeleteSuccess+" ("+endorse.Code+"-"+endorse.Alien.Name.FullName+")");
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
