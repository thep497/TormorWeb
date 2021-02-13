using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tormor.DomainModel;
using NNS.MVCHelpers;
using System.Globalization;
using NNS.Config;
using NNS.ModelHelpers;
using NNS.GeneralHelpers;
using NNS.CustomEvents;

namespace Tormor.Web.Areas.ConveyanceArea.Controllers
{
    [Authorize]
    public class AddRemoveCrewController : Controller
    {
        private IAddRemoveCrewRepository addRemoveCrewRepo;

        public AddRemoveCrewController(IAddRemoveCrewRepository addRemoveCrewRepository)
        {
            this.addRemoveCrewRepo = addRemoveCrewRepository;
        }

        //
        // GET: /Conveyance/AddRemoveCrew/
        public ActionResult IncreaseCrew()
        {
            return RedirectToAction("Index", new { addRemoveType = 1 });
        }

        public ActionResult DecreaseCrew()
        {
            return RedirectToAction("Index", new { addRemoveType = 2 });
        }

        public ActionResult Index(int addRemoveType, int? dtpSelectRange, DateTime? dtpFromDate, DateTime? dtpToDate)
        {
            ToolbarMenuHelpers.SetToolBar(ViewData,
                new
                {
                    New = "Insert",
                    Date = "Index"
                },
                "AddRemoveCrew",
                new { addRemoveType },
                null,
                ref dtpSelectRange,
                ref dtpFromDate, ref dtpToDate);

            ViewData["AddRemoveType"] = addRemoveType;
            return View(addRemoveCrewRepo.FindAll(addRemoveType, dtpFromDate, dtpToDate).ToList());
        }

        public ActionResult Index2(int addRemoveType, int? dtpSelectRange, DateTime? dtpFromDate, DateTime? dtpToDate, bool? backToSearch = null)
        {
            if (backToSearch ?? false)
            {
                return RedirectToAction("Index", "Search", new { area = "Search" });
            }
            return RedirectToAction("Index", "AddRemoveCrew",
                new { area = "ConveyanceArea", addRemoveType, dtpSelectRange, dtpFromDate, dtpToDate });
        }

        public ActionResult Insert(int addRemoveType)
        {
            return doInsert(new AddRemoveCrew(addRemoveType), addRemoveType);
        }

        private ActionResult doInsert(AddRemoveCrew addRemoveCrew, int addRemoveType)
        {
            ToolbarMenuHelpers.SetToolBar(ViewData,
                new { Save = "btnSave", New = "Insert", Close = "Index2" }, "AddRemoveCrew",new { addRemoveType });

            //makeReferenceViewData();
            ViewData["AddRemoveType"] = addRemoveType;
            return View(addRemoveCrew);
        }

        public ActionResult Edit(int id, int? addRemoveType = null, bool? backToSearch=null)
        {
            int lAddRemoveType;
            var addRemoveCrew = addRemoveCrewRepo.GetOne(id, addRemoveType);
            if ((addRemoveType == null) && (addRemoveCrew != null))
                lAddRemoveType = addRemoveCrew.AddRemoveType;
            else
                lAddRemoveType = (addRemoveType ?? 1);
            return doEdit(addRemoveCrew, lAddRemoveType, backToSearch);
        }

        /// <summary>
        /// ใช้สำหรับ reload กรณีที่ user กรอก code ซ้ำ เพราะไม่สามารถเรียก function Edit เดิม (ใน Url.Action) ได้ (ของเดิมมี id อยู่แล้ว)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ReEdit(int id, int addRemoveType)
        {
            return RedirectToAction("Edit", new { id, addRemoveType });
        }

        private ActionResult doEdit(AddRemoveCrew addRemoveCrew, int addRemoveType, bool? backToSearch = null)
        {
            ToolbarMenuHelpers.SetToolBar(ViewData,
                new { Save = "btnSave", New = "Insert", GiveUp = "btnDelete", Close = "Index2" }, "AddRemoveCrew",new { addRemoveType, backToSearch });

            ViewData["AddRemoveType"] = addRemoveType;
            if (addRemoveCrew != null)
                return View(addRemoveCrew);
            else
            {
                TempData.AddError(Resources.Messages.NotFoundData);
                return RedirectToAction("Index2", new { addRemoveType });
            }
        }

        #region Server CRUD Section...
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Insert(AddRemoveCrew addRemoveCrew, int addRemoveType, bool wantClose)
        {
            this.addRemoveCrewRepo.CurrentUserName = HttpContext.User.Identity.Name;
            try
            {
                addRemoveCrewRepo.DoNewRecord(addRemoveCrew, addRemoveType);
                UpdateModel(addRemoveCrew);
                addRemoveCrewRepo.DoSave(addRemoveCrew, true, addRemoveType);
                TempData.AddInfo(Resources.Messages.SaveSuccess);
                if (!wantClose)
                    return RedirectToAction("Edit", new { id = addRemoveCrew.Id, addRemoveType });
                return RedirectToAction("Index2", new { addRemoveType });
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

            return doInsert(addRemoveCrew, addRemoveType);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, bool wantClose, int addRemoveType)
        {
            var addRemoveCrew = addRemoveCrewRepo.GetOne(id, addRemoveType);
            this.addRemoveCrewRepo.CurrentUserName = HttpContext.User.Identity.Name;
            try
            {
                UpdateModel(addRemoveCrew);
                addRemoveCrewRepo.DoSave(addRemoveCrew, false, addRemoveType);
                TempData.AddInfo(Resources.Messages.SaveSuccess);
                if (!wantClose)
                    return RedirectToAction("Edit", new { id, addRemoveType });
                return RedirectToAction("Index2", new { addRemoveType });
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

            return doEdit(addRemoveCrew, addRemoveType);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int addRemoveType)
        {
            AddRemoveCrew addRemoveCrew = addRemoveCrewRepo.GetOne(id, addRemoveType);
            if (addRemoveCrew != null)
            {
                try
                {
                    //Delete the record                
                    addRemoveCrewRepo.DoDelete(addRemoveCrew);
                    new RecordDeletedEvent("AddRemoveCrew", addRemoveCrew.Id, 0, addRemoveCrew.Code, addRemoveCrew.Alien.Name.FullName, null).Raise();
                    TempData.AddInfo(Resources.Messages.DeleteSuccess + " (" + addRemoveCrew.Code + "-" + addRemoveCrew.Alien.Name.FullName + ")");
                }
                catch (Exception ex)
                {
                    TempData.AddError(ex.ExMessage());
                    return RedirectToAction("Edit", new { id, addRemoveType });
                }
            }
            else
                TempData.AddError(Resources.Messages.NotFoundData);

            return RedirectToAction("Index2", new { addRemoveType });
        }
        #endregion

        #region Make References Combobox
        [HttpPost]
        public ActionResult _GetAddRemoveCrewCodeDetail(int id, string code, string xdate, int extdata)
        {
            int addRemoveType = extdata;
            AddRemoveCrew addRemoveCrew = null;
            try
            {
                DateTime requestdate = DateTime.ParseExact(xdate, Globals.DateFormat, CultureInfo.InvariantCulture);
                addRemoveCrew = addRemoveCrewRepo.GetOneByCode(code, requestdate, addRemoveType, id);
                if (addRemoveCrew != null)
                {
                    return new JsonResult
                    {
                        Data = new
                        {
                            dupcode = true,
                            code = code,
                            rdate = addRemoveCrew.RequestDate.ToString(Globals.DateFormat),
                            id = addRemoveCrew.Id,
                            name = addRemoveCrew.Alien.Name.FullName
                        }
                    };
                }
            }
            catch (FormatException)
            {
            }

            return new JsonResult { Data = new { dupcode = false } };
        }
        #endregion

    }

}
