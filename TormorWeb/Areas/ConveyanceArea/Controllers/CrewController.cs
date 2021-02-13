// *******************************************************************
//1. ชื่อโปรแกรม 	: NeoSKIMO
//2. ชื่อระบบ 		: WebUI
//3. ชื่อ Unit 	: SearchController
//4. ผู้เขียนโปรแกรม	: อมรเทพ
//5. วันที่สร้าง	    : 201012xx
//6. จุดประสงค์ของ Unit นี้ : Controller สำหรับจัดการการค้นหา AlienTransaction
// *******************************************************************
// แก้ไขครั้งที่ : 0
// ประวัติการแก้ไข
// ครั้งที่ 0 : เริ่มต้นสร้าง unit ใหม่
// ครั้งที่ 1 : //th20110411 เพิ่มคนเพิ่มลด ใน Tab คนประจำเรือ PD18-540102 Req 8
// *******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tormor.DomainModel;
using Telerik.Web.Mvc;
using Telerik.Web.Mvc.Extensions;
using Tormor.Web.Models;
using NNS.MVCHelpers;
using NNS.Config;
using NNS.ModelHelpers;
using NNS.CustomEvents;

namespace Tormor.Web.Areas.ConveyanceArea.Controllers
{
    [Authorize]
    public class CrewController : Controller
    {
        private IConveyanceInOutRepository convInOutRepo;
        private ICrewRepository crewRepo;
        private IReferenceRepository refRepo;

        //constructor
        public CrewController(ICrewRepository crewRepository, IConveyanceInOutRepository convInOutRepository, IReferenceRepository referenceRepository)
        {
            this.crewRepo = crewRepository;
            this.convInOutRepo = convInOutRepository;
            this.refRepo = referenceRepository;
        }

        #region Detail (Crew) Ajax CRUD
        /// <summary>
        /// Ajax Get Crew
        /// </summary>
        /// <param name="conveyanceInOutId">id ของตัวแม่ (conveyanceInOutId)</param>
        /// <returns></returns>
        [GridAction]
        public ActionResult _GetCrews(int conveyanceInOutId, bool isCrew)
        {
            //สองตัวนี้ไม่ได้ใช้ ส่งไปให้ครบเท่านั้น
            string cName = "";
            var ioDate = DateTime.Today;

            //th20110411 vvv เพิ่ม addRemoveCrews เพื่อดึงไป show ในหน้าคนเรือด้วย PD18-540102 Req 8
            IList<AddRemoveCrew> addRemoveCrews = null;
            if (isCrew)
            {
                var convInout = convInOutRepo.GetOne(conveyanceInOutId);
                if (convInout != null)
                {
                    if (convInout.InOutType == ModelConst.CONVINOUT_OUT) //เอาเฉพาะกรณีออกเท่านั้น
                        addRemoveCrews = convInout.DiffCrew;
                }
            }
            //th20440411 ^^^
            var esvm = new CrewViewModel(conveyanceInOutId, isCrew, cName, ioDate, 
                crewRepo.FindAll(conveyanceInOutId, isCrew).ToList(),addRemoveCrews); //th20110411
            return View(new GridModel(esvm.vmCrews));
        }

        /// <summary>
        /// th201012xx 
        /// ทำการ Insert ลูกเรือ (แสดง Partial View)
        /// </summary>
        /// <param name="conveyanceInOutid"></param>
        /// <param name="isCrew"></param>
        /// <param name="hasCrew"></param>
        /// <param name="inCrew"></param>
        /// <returns></returns>
        public ActionResult _InsertCrew(int conveyanceInOutid, bool isCrew, bool hasCrew = false, Crew inCrew = null)
        {
            if (conveyanceInOutid <= 0) // ยังไม่ได้ Save Master
            {
                return Content(ControllerHelper.ModalContent("กรุณากดปุ่ม Save ด้านบนของเอกสารหลักก่อนค่ะ"));
            }
            ViewData["isCreate"] = true;
            ViewData["isCrew"] = isCrew;
            Crew crew;
            if (hasCrew && (inCrew != null))
                crew = inCrew;
            else
            {
                ModelState.Clear();
                crew = new Crew(conveyanceInOutid, isCrew);
            }
            return PartialView("CrewEdit", crew);
        }

        /// <summary>
        /// th201012xx 
        /// ทำการ Edit ลูกเรือ (แสดง Partial View)
        /// </summary>
        /// <param name="conveyanceInOutid"></param>
        /// <param name="isCrew"></param>
        /// <param name="crewId"></param>
        /// <param name="hasCrew"></param>
        /// <param name="inCrew"></param>
        /// <returns></returns>
        public ActionResult _EditCrew(int conveyanceInOutid, bool isCrew, int crewId, bool hasCrew=false, Crew inCrew = null)
        {
            ViewData["isCreate"] = false;
            ViewData["isCrew"] = isCrew;
            Crew crew;
            if (hasCrew && (inCrew != null))
                crew = inCrew;
            else
            {
                ModelState.Clear();
                crew = crewRepo.GetOne(conveyanceInOutid, isCrew, crewId);
            }
            if (crew != null)
                return PartialView("CrewEdit", crew);
            return Content(ControllerHelper.ModalContent("Cannot find the detail..."));
        }

        /// <summary>
        /// th201012xx 
        /// ทำการ บันทึก ลูกเรือ (เรียก repo มา save)
        /// </summary>
        /// <param name="isCreate"></param>
        /// <param name="conveyanceInOutId"></param>
        /// <param name="isCrew"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult _SaveCrew(bool isCreate, int conveyanceInOutId, bool isCrew,int id = 0)
        {
            Crew crew;
            if (!isCreate && id > 0)
            {
                crew = crewRepo.GetOne(conveyanceInOutId, isCrew, id);
            }
            else
            {
                crew = new Crew(conveyanceInOutId,isCrew);
            }
            this.crewRepo.CurrentUserName = HttpContext.User.Identity.Name;
            try
            {
                if (isCreate)
                    crewRepo.DoNewRecord(crew);
                UpdateModel(crew);
                crewRepo.DoSave(crew, isCreate);

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
                return _InsertCrew(conveyanceInOutId, isCrew, true, crew);
            return _EditCrew(conveyanceInOutId, isCrew, id, true, crew);
        }

        /// <summary>
        /// th201012xx 
        /// ทำการ ลบ ลูกเรือ (เรียก repo เพื่อ delete)
        /// </summary>
        /// <param name="conveyanceInOutId"></param>
        /// <param name="isCrew"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult _DeleteCrew(int conveyanceInOutId, bool isCrew, int id)
        {
            Crew crew = crewRepo.GetOne(conveyanceInOutId, isCrew, id);
            if (crew != null)
            {
                try
                {
                    //Delete the record                
                    crewRepo.DoDelete(crew);
                    new RecordDeletedEvent("Crew", crew.Id, conveyanceInOutId, crew.Code, null, null).Raise();
                }
                catch 
                {
                    return RedirectToAction("_EditCrew", new { conveyanceInOutId, id});
                }
            }

            return Content(Globals.ModalDetailUpdateOK);
        }

        #endregion

    }
}
