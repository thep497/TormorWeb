using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NNS.MVCHelpers;
using Tormor.DomainModel;
using Tormor.Web.Models;
using Telerik.Web.Mvc;
using Telerik.Web.Mvc.Extensions;
using NNS.ModelHelpers;
using NNS.CustomEvents;
using NNS.GeneralHelpers;
using System.Web.Routing;

namespace NNS.Web.Areas.Reference.Controllers
{
    [Authorize]
    [AreaSiteMap]
    public class ReferenceController : Controller
    {
        private IReferenceRepository refRepo;

        public ReferenceController(IReferenceRepository referenceRepository)
        {
            this.refRepo = referenceRepository;
        }

        //
        // GET: /Reference/Reference/

        [Authorize(Roles = "Administrator, GODS")]
        public ActionResult Index(int? refTypeId)
        {
            if (refTypeId == null)
                return View("Blank");

            //the grid called... 2?Grid-mode=insert
            ToolbarMenuHelpers.SetToolBar(ViewData, new { New = "Index" }, "Reference",
                new RouteValueDictionary { { "Grid-mode", "insert" }, { "refTypeId", refTypeId } }, null, true);

            ReferenceViewModel refVM = new ReferenceViewModel(refRepo, refTypeId ?? 0);
            populateReferences(refTypeId ?? 0);
            return View("Index",refVM);
        }

        #region For use with combobox/autocomplete section...
        private void populateReferences(int refTypeId)
        {
            var refRefTypeId = refRepo.GetRefRefTypeId(refTypeId);

            if (refRefTypeId == null)
                ViewData["cb_references"] = null;
            ViewData["cb_references"] = refRepo.FindAll(refRefTypeId ?? 0).ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult _getReferenceAutoCompleteAjax(int refTypeId, string refRefName, string text)
        {
            IEnumerable<zz_Reference> refs = refRepo.FindAll(refTypeId).ToList();
            if (refRefName.HasValue() && (refRefName != ""))
            {
                refs = refs.Where(r => r.RefRefName == refRefName);
            }
            if (text.HasValue())
            {
                text = text.ToLower();
                refs = refs.Where(r => r.RefName.ToLower().Contains(text));
            }
            return new JsonResult { Data = refs.Select(r => r.RefName).ToList() };
        }

        /// <summary>
        /// แสดง Dropdown / ComboBox แบบที่มีเฉพาะ Name
        /// </summary>
        /// <param name="refTypeId">ประเภท Ref</param>
        /// <param name="text">ตัวค้นหาของ Name</param>
        /// <param name="value">Name เดิมที่ส่งเข้ามา</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult _getReferenceComboDropDownAjax(int refTypeId, string text, string value, bool wantPleaseSelect=true)
        {
            var refs = refRepo.FindAll(refTypeId);
            if (text.HasValue())
            {
                text = text.ToLower();
                refs = refs.Where(r => r.RefName.ToLower().Contains(text));
            }
            var result = (from r in refs
                          select new
                          {
                              code = r.RefName,
                              name = r.RefName
                          }).ToList();
            if (wantPleaseSelect)
                result.Insert(0, new { code = "", name = "Please Select" });
            
            //ถ้า value ไม่อยู่ใน list ให้แทรกเข้าไปด้วย
            if (value.HasValue())
            {
                if (result.Where(r => r.name == value).Count() <= 0)
                {
                    result.Add(new { code = value, name = value });
                }
            }
            return new JsonResult
            {
                Data = new SelectList(result, "code", "name", value)
            };

        }

        /// <summary>
        /// แสดง Dropdown / ComboBox แบบมี Code/Name
        /// </summary>
        /// <param name="refTypeId">ประเภท Ref</param>
        /// <param name="text">ตัวค้นหาของ Name</param>
        /// <param name="value">Code เดิมที่ส่งเข้ามา</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult _getReferenceComboDropDown_CodeName_Ajax(int refTypeId, string text, string value, bool wantPleaseSelect = true)
        {
            var refs = refRepo.FindAll(refTypeId);
            if (text.HasValue())
            {
                text = text.ToLower();
                refs = refs.Where(r => r.RefName.ToLower().Contains(text));
            }
            var result = (from r in refs
                          select new
                          {
                              code = r.Code,
                              name = r.RefName
                          }).ToList();
            if (wantPleaseSelect)
                result.Insert(0, new { code = "0", name = "Please Select" });

            //ถ้า value ไม่อยู่ใน list ให้แทรกเข้าไปด้วย
            if (value.HasValue())
            {
                if (result.Where(r => r.code == value).Count() <= 0)
                {
                    result.Add(new { code = value, name = text });
                }
            }
            return new JsonResult
            {
                Data = new SelectList(result, "code", "name", value)
            };

        }
        #endregion

        #region Server CRUD Section...
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update(int refTypeId, int id)
        {
            zz_Reference reference = refRepo.GetOne(id);
            this.refRepo.CurrentUserName = HttpContext.User.Identity.Name;
            try
            {
                UpdateModel(reference);
                refRepo.DoSave(reference, false);

                //RouteValueDictionary routeValues = this.GridRouteValues();
                //routeValues.Add("refTypeId", refTypeId);
                //return RedirectToAction("Index", routeValues);

                //return RedirectToAction("Index", new { refTypeId = refTypeId });
            }
            catch (RulesException ex)
            {
                ex.CopyTo(ModelState);
            }

            return RedirectToAction("Index", new { refTypeId = refTypeId});
            //return Index(refTypeId);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Insert(int refTypeId, string Code)
        {
            //Create a new instance of the zz_Reference class.            
            zz_Reference reference = new zz_Reference();
            this.refRepo.CurrentUserName = HttpContext.User.Identity.Name;

            //Perform model binding (fill the reference properties and validate it).            
            try
            {
                refRepo.DoNewRecord(refTypeId, Code, reference);
                UpdateModel(reference, string.Empty, null, new string[] { "Code" }); //ไม่ต้อง Update Field Code เพราะใน DoNewRecord ทำแล้ว และใน Save ก็มีเช็คซ้ำ
                //The model is valid - insert the reference.                
                refRepo.DoSave(reference, true);

                //return RedirectToAction("Index", new { refTypeId = refTypeId });
            }
            catch (RulesException ex)
            {
                ex.CopyTo(ModelState);
            }
            return RedirectToAction("Index", new { refTypeId = refTypeId });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int refTypeId, int id)
        {
            //Find a customer with ProductID equal to the id action parameter            
            zz_Reference reference = refRepo.GetOne(id);
            this.refRepo.CurrentUserName = HttpContext.User.Identity.Name;
            if (reference != null)
            {
                try
                {
                    //Delete the record                
                    refRepo.DoDelete(reference);
                    new RecordDeletedEvent("Reference", reference.Id, reference.RefTypeId, reference.Code, reference.RefName, null).Raise();
                }
                catch (ApplicationException ex)
                {
                    TempData.AddError(ex.ExMessage());
                }
            }
            return RedirectToAction("Index", new { refTypeId = refTypeId });
        }
        #endregion

    }
}
