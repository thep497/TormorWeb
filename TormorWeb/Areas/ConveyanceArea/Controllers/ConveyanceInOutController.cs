using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NNS.CustomEvents;
using NNS.GeneralHelpers;
using NNS.ModelHelpers;
using NNS.MVCHelpers;
using Telerik.Web.Mvc.Extensions;
using Tormor.DomainModel;
using Tormor.Web.Models;

namespace Tormor.Web.Areas.ConveyanceArea.Controllers
{
    [Authorize]
    public class ConveyanceInOutController : Controller
    {
        private IConveyanceInOutRepository convInOutRepo;
        private IReferenceRepository refRepo;
        private ISearchRepository searchRepo;

        public ConveyanceInOutController(IConveyanceInOutRepository convInOutRepository, IReferenceRepository referenceRepository, ISearchRepository searchRepository)
        {
            this.convInOutRepo = convInOutRepository;
            this.refRepo = referenceRepository;
            this.searchRepo = searchRepository;
        }

        //
        // GET: /ConveyanceInOut/ConveyanceInOut/
        public ActionResult Index()
        {
            if (searchRepo.ConveyanceSearch == null)
                searchRepo.ConveyanceSearch = new ConveyanceSearchInfo();

            int? dtpSelectRange = searchRepo.ConveyanceSearch.dtpSelectRange;
            DateTime? dtpFromDate = searchRepo.ConveyanceSearch.dtpFromDate;
            DateTime? dtpToDate = searchRepo.ConveyanceSearch.dtpToDate;

            ToolbarMenuHelpers.SetToolBar(ViewData, new { New = "Insert", Date = "Index" }, "ConveyanceInOut",
                null, null,  ref dtpSelectRange, ref dtpFromDate, ref dtpToDate);

            //ไม่ต้องใส่ค่ากลับให้ dtpSelectRange เพราะในนั้นมัน set เป็น 0
            searchRepo.ConveyanceSearch.dtpFromDate = dtpFromDate;
            searchRepo.ConveyanceSearch.dtpToDate = dtpToDate;

            var searchData = new ConveyanceSearchViewModel(searchRepo.ConveyanceSearch,
                                                           convInOutRepo.FindAll(searchRepo.ConveyanceSearch).ToList());
            return View(searchData);
        }

        [HttpPost]
        public ActionResult DoSearch(ConveyanceSearchInfo convsearchinfo)
        {
            searchRepo.ConveyanceSearch = convsearchinfo;
            return RedirectToAction("Index");
        }

        public ActionResult Index2(bool? backToSearch=null)
        {
            if (backToSearch ?? false)
            {
                return RedirectToAction("Index", "Search", new { area = "Search" });
            }
            return RedirectToAction("Index", "ConveyanceInOut", new { area = "ConveyanceArea" });
        }

        public ActionResult Insert()
        {
            return doInsert(new ConveyanceInOut());
        }

        private ActionResult doInsert(ConveyanceInOut convInOut)
        {
            ToolbarMenuHelpers.SetToolBar(ViewData,
                new { Save = "btnSave", New = "Insert", Close = "Index2" }, "ConveyanceInOut");

            //makeReferenceViewData();
            convInOut.Code = convInOutRepo.GetNewCode();
            return View(convInOut);
        }

        public ActionResult Edit(int id, bool? backToSearch = null)
        {
            var convInOut = convInOutRepo.GetOne(id);
            return doEdit(convInOut,backToSearch);
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

        private ActionResult doEdit(ConveyanceInOut convInOut, bool? backToSearch = null)
        {
            ToolbarMenuHelpers.SetToolBar(ViewData,
                new { Save = "btnSave", New = "Insert", GiveUp = "btnDelete", Close = "Index2" }, "ConveyanceInOut", new { backToSearch });
            
            //ส่งผ่านตัวแปรเพื่อทำ combobox
            //makeReferenceViewData();

            if (convInOut != null)
                return View(convInOut);
            else
            {
                TempData.AddError(Resources.Messages.NotFoundData);
                return RedirectToAction("Index2");
            }
        }

        #region Server CRUD Section...
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Insert(ConveyanceInOut convInOut ,bool wantClose)
        {
            //ConveyanceInOut convInOut = new ConveyanceInOut();
            this.convInOutRepo.CurrentUserName = HttpContext.User.Identity.Name;
            try
            {
                convInOutRepo.DoNewRecord(convInOut);
                UpdateModel(convInOut);
                convInOutRepo.DoSave(convInOut, true);
                TempData.AddInfo(Resources.Messages.SaveSuccess);
                if (!wantClose)
                    return RedirectToAction("Edit", new { id = convInOut.Id });
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

            return doInsert(convInOut);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, bool wantClose)
        {
            var convInOut = convInOutRepo.GetOne(id);
            this.convInOutRepo.CurrentUserName = HttpContext.User.Identity.Name;
            try
            {
                UpdateModel(convInOut);
                convInOutRepo.DoSave(convInOut, false);
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

            return doEdit(convInOut);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            ConveyanceInOut convInOut = convInOutRepo.GetOne(id);
            if (convInOut != null)
            {
                try
                {
                    //Delete the record                
                    convInOutRepo.DoDelete(convInOut);
                    new RecordDeletedEvent("ConveyanceInOut", convInOut.Id, 0, convInOut.Code, convInOut.Conveyance.Name, null).Raise();
                    TempData.AddInfo(Resources.Messages.DeleteSuccess + " (" + convInOut.Code + "-" + convInOut.Conveyance.Name + ")");
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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult _getPortInThai(string text, string value)
        {
            if (text.HasValue())
                text = text.ToLower();
            else
                text = "";

            var checkedDate = DateTime.Today.AddYears(-1);
            var refs = convInOutRepo.FindAll().Where(r => r.UpdateInfo.UpdatedDate >= checkedDate); // กรองเอาเฉพาะพวกที่กรอกภายใน 1 ปีเท่านั้น
            var ref2 = from r in refs.Where(r => r.PortInTo.PortName.ToLower().Contains(text) && r.PortInTo.PortName != null)
                       select new
                       {
                           name = r.PortInTo.PortName
                       };
            var ref3 = from r in refs.Where(r => r.PortOutFrom.PortName.ToLower().Contains(text) && r.PortOutFrom.PortName != null)
                       select new
                       {
                           name = r.PortOutFrom.PortName
                       };
            var result = (from r in ref2.Concat(ref3)
                          group r by r.name into n
                          orderby n.Key
                          select new
                          {
                              code = n.Key,
                              name = n.Key
                          }).ToList();

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

    }
}
