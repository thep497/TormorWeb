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
    public class VisaController : Controller
    {
        private IVisaRepository visaRepo;
        private IReferenceRepository refRepo;

        public VisaController(IVisaRepository visaRepository,IReferenceRepository referenceRepository)
        {
            this.visaRepo = visaRepository;
            this.refRepo = referenceRepository;
        }

        //
        // GET: /Visa/

        public ActionResult Index(int? dtpSelectRange, DateTime? dtpFromDate, DateTime? dtpToDate)
        {
            ToolbarMenuHelpers.SetToolBar(ViewData,
                new
                {
                    //Save = "Save", //กรณีปุ่ม Save/GiveUp ให้ link ไปที่ id ของปุ่ม save จริง ๆ ของฟอร์ม
                    New = "Insert",
                    //GiveUp = "Delete",
                    //Print = "Print",
                    //Print1 = "Print",
                    //Other = "Other",
                    //Other1 = "Other",
                    Date = "Index"
                },
                "Visa",
                null,
                null, //new { Other1 = "อื่น ๆ ลองดู", }, 
                ref dtpSelectRange,
                ref dtpFromDate, ref dtpToDate);

            return View(visaRepo.FindAll(dtpFromDate, dtpToDate).ToList());
        }

        public ActionResult Index2(int? dtpSelectRange, DateTime? dtpFromDate, DateTime? dtpToDate)
        {
            return RedirectToAction("Index", "Search", new { area = "Search" });
        }

        public ActionResult Insert()
        {
            return doInsert(new VisaDetail());
        }

        private ActionResult doInsert(VisaDetail visa)
        {
            ToolbarMenuHelpers.SetToolBar(ViewData,
                new { Save = "btnSave", New = "Insert", Close = "Index2" }, "Visa");

            //makeReferenceViewData();
            return View(visa);
        }

        public ActionResult Edit(int id)
        {
            var visa = visaRepo.GetOne(id);
            return doEdit(visa);
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
        /// <summary>
        /// สำหรับการเอาค่า Ref จาก StayPeriod ไปใส่ใน StayType
        /// </summary>
        /// <param name="text">ข้อมูลระยะเวลา (ลูก) เช่น 7 วัน/1 ปี</param>
        /// <returns>JSon String บอกค่าของ RefName (แม่) เช่น ระยะสั้น</returns>
        [HttpPost]
        public ActionResult _GetStayType(string text)
        {
            string result;
            if (text.HasValue())
            {
                string refRefName = refRepo.GetOneByName(2, text).RefRefName;
                result = refRepo.FindAll(1).Where(c => c.RefName == refRefName).FirstOrDefault().RefName;
            }
            else
                result = "";
            return new JsonResult { Data = result };
        }

        [HttpPost]
        public ActionResult _GetStayReasonDetail(string text)
        {
            IQueryable<zz_Reference> result;
            if (text.HasValue())
            {
                string refCode = refRepo.GetOneByName(3, text).Code;
                result = refRepo.FindAll(4).Where(c => c.RefCode == refCode);
            }
            else
                result = refRepo.FindAll(4);
            return new JsonResult
            {
                Data = result.Select(r => r.RefName)
            };
        }

        [HttpPost]
        public ActionResult _GetVisaCodeDetail(int id, string code,string xdate)
        {
            VisaDetail visa = null;
            try
            {
                DateTime requestdate = DateTime.ParseExact(xdate, Globals.DateFormat, CultureInfo.InvariantCulture);
                visa = visaRepo.GetOneByCode(code, requestdate, id);
                if (visa != null)
                {
                    return new JsonResult
                    {
                        Data = new
                        { 
                            dupcode = true,
                            code = code,
                            rdate = visa.RequestDate.ToString(Globals.DateFormat),
                            id = visa.Id,
                            name = visa.Alien.Name.FullName
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

        private ActionResult doEdit(VisaDetail visa)
        {
            ToolbarMenuHelpers.SetToolBar(ViewData,
                new { Save = "btnSave", New = "Insert", GiveUp = "btnDelete", Close = "Index2" }, "Visa");
            
            //ส่งผ่านตัวแปรเพื่อทำ combobox
            //makeReferenceViewData();

            if (visa != null)
                return View(visa);
            else
            {
                TempData.AddError(Resources.Messages.NotFoundData);
                return RedirectToAction("Index2");
            }
        }

        #region Server CRUD Section...
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Insert(VisaDetail visa ,bool wantClose)
        {
            //VisaDetail visa = new VisaDetail();
            this.visaRepo.CurrentUserName = HttpContext.User.Identity.Name;
            try
            {
                visaRepo.DoNewRecord(visa);
                UpdateModel(visa);
                visaRepo.DoSave(visa, true);
                TempData.AddInfo(Resources.Messages.SaveSuccess);
                if (!wantClose)
                    return RedirectToAction("Edit", new { id = visa.Id });
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

            return doInsert(visa);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, bool wantClose)
        {
            var visa = visaRepo.GetOne(id);
            this.visaRepo.CurrentUserName = HttpContext.User.Identity.Name;
            try
            {
                UpdateModel(visa);
                visaRepo.DoSave(visa, false);
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

            return doEdit(visa);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            VisaDetail visa = visaRepo.GetOne(id);
            if (visa != null)
            {
                try
                {
                    //Delete the record                
                    visaRepo.DoDelete(visa);
                    new RecordDeletedEvent("VisaDetail", visa.Id, 0, visa.Code, visa.Alien.Name.FullName, null).Raise();
                    TempData.AddInfo(Resources.Messages.DeleteSuccess+" ("+visa.Code+"-"+visa.Alien.Name.FullName+")");
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
