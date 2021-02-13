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
// *******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NNS.MVCHelpers;
using Tormor.DomainModel;
using Tormor.Web.Models;
using NNS.GeneralHelpers;
using NNS.Config;
using System.IO;
using Tormor.Web.Reports.Helpers;
using Tormor.Web.Reports.Models;
using NNS.ModelHelpers;
using Telerik.Web.Mvc.Extensions;

namespace Tormor.Web.Areas.Search.Controllers
{
    [Authorize]
    //[AreaSiteMap]
    public class SearchController : Controller
    {
        private readonly IAlienTransactionRepository aTranRepo;
        private readonly IReferenceRepository refRepo;
        private readonly ISearchRepository searchRepo;

        protected SearchController()
        {
        }
        public SearchController(IAlienTransactionRepository aTranRepository, 
            IReferenceRepository referenceRepository, ISearchRepository searchRepository) 
            : this()
        {
            this.aTranRepo = aTranRepository;
            this.refRepo = referenceRepository;
            this.searchRepo = searchRepository;
        }
         
        //
        // GET: /Search/Search/

        public ActionResult Index()
        {
            if (searchRepo.AlienSearch == null)
                searchRepo.AlienSearch = new AlienSearchInfo(Globals.WantVisa, Globals.WantReEntry, Globals.WantEndorse, Globals.WantStay, Globals.WantShip);

            int? dtpSelectRange = searchRepo.AlienSearch.dtpSelectRange;
            DateTime? dtpFromDate = searchRepo.AlienSearch.dtpFromDate;
            DateTime? dtpToDate = searchRepo.AlienSearch.dtpToDate;

            ToolbarMenuHelpers.SetToolBar(ViewData,
                new
                {
                    Print = "AlienListReport",
                    Print1 = "AlienListReport",
                    Print2 = "AlienListReportXLS",
                    Print3 = "AlienDetailReport",
                    Date = "Index",
                    New = "#", //ใส่กรณีที่มี submenu แต่ไม่ต้องการให้มี action กระทำการใด ๆ
                    New1 = "Visa/Visa/Insert",
                    New2 = "Visa/ReEntry/Insert",
                    New3 = "Visa/Endorse/Insert",
                    New4 = "Visa/Stay/Insert"
                },
                "Search",
                null,
                new
                {
                    Print1 = "พิมพ์รายการติดต่อบุคคลต่างด้าว",
                    Print2 = "พิมพ์รายการติดต่อบุคคลต่างด้าว (Excel)",
                    Print3 = "พิมพ์รายละเอียดบุคคลต่างด้าว",
                    New1 = "เพิ่มรายการขอต่ออายุ",
                    New2 = "เพิ่มรายการ Re-Entry",
                    New3 = "เพิ่มรายการสลักหลังถิ่นที่อยู่",
                    New4 = "เพิ่มรายการรายงานตัว 90 วัน"
                }, ref dtpSelectRange, ref dtpFromDate, ref dtpToDate);

            //ไม่ต้องใส่ค่ากลับให้ dtpSelectRange เพราะในนั้นมัน set เป็น 0
            searchRepo.AlienSearch.dtpFromDate = dtpFromDate;
            searchRepo.AlienSearch.dtpToDate = dtpToDate;

            var searchData = new AlienSearchViewModel(searchRepo.AlienSearch,
                                                      aTranRepo.FindAll(searchRepo.AlienSearch).ToList());
            return View(searchData);
        }

        public ActionResult Index2()
        {
            return View();
        }

        public ActionResult Edit(string ttype, int id)
        {
            string controllerName, areaName;
            switch (ttype)
            {
                case "1":
                    controllerName = "Visa";
                    areaName = "Visa";
                    break;
                case "2":
                    controllerName = "ReEntry";
                    areaName = "Visa";
                    break;
                case "3":
                    controllerName = "Endorse";
                    areaName = "Visa";
                    break;
                case "4":
                    controllerName = "Stay";
                    areaName = "Visa";
                    break;
                case "5":
                case "6":
                    controllerName = "ConveyanceInOut";
                    areaName = "ConveyanceArea";
                    break;
                case "7":
                case "8":
                    controllerName = "AddRemoveCrew";
                    areaName = "ConveyanceArea";
                    break;
                default:
                    controllerName = "Visa";
                    areaName = "Visa";
                    break;
            }
            return RedirectToAction("Edit", controllerName, new { id = id, area = areaName, backToSearch = true });
        }

        [HttpPost]
        public ActionResult DoSearch(AlienSearchInfo aliensearchinfo)
        {
            searchRepo.AlienSearch = aliensearchinfo;
            return RedirectToAction("Index");
        }

        /// <summary>
        /// แสดงรายงานรายการตนต่างด้าวตามเงื่อนไขที่แสดงในหน้าจอ
        /// </summary>
        /// <returns></returns>
        public ActionResult AlienListReport(bool isExcel = false)
        {
            if (searchRepo.AlienSearch != null)
            {
                string mimeType;
                var repData = aTranRepo.FindAll(searchRepo.AlienSearch).Select(c => new AlienTransReport(c,Globals.DateFormat));
                var dict = new Dictionary<string, string>();
                dict.Add("fromDate", searchRepo.AlienSearch.dtpFromDate.ToString(Globals.DateFormat));
                dict.Add("toDate", searchRepo.AlienSearch.dtpToDate.ToString(Globals.DateFormat));
                dict.Add("userCode", HttpContext.User.Identity.Name);

                byte[] stream;
                if (!isExcel)
                    stream = this.DevReportToPDF("AlienTransList", "AlienTransList", out mimeType, dict, repData.ToList());
                else
                    stream = this.DevReportToExcel("AlienTransList", "AlienTransList", out mimeType, dict, repData.ToList());
                return File(stream, mimeType);
            }
            return new EmptyResult();
        }

        public ActionResult AlienListReportXLS()
        {
            return RedirectToAction("AlienListReport", new { isExcel = true});
        }

        /// <summary>
        /// แสดง Report รายละเอียดคนต่างด้าว
        /// </summary>
        /// <returns></returns>
        public ActionResult AlienDetailReport()
        {
            if (searchRepo.AlienSearch != null)
            {
                string mimeType;
                var repData = aTranRepo.FindAll(searchRepo.AlienSearch).Select(c => new AlienTransReport(c, Globals.DateFormat,true));
                var dict = new Dictionary<string, string>();
                dict.Add("fromDate", searchRepo.AlienSearch.dtpFromDate.ToString(Globals.DateFormat));
                dict.Add("toDate", searchRepo.AlienSearch.dtpToDate.ToString(Globals.DateFormat));
                dict.Add("userCode", HttpContext.User.Identity.Name);

                var stream = this.DevReportToPDF("AlienTransDetail", "AlienTransDetail", out mimeType, dict, repData.ToList());

                return File(stream, mimeType);
            }
            return new EmptyResult();
        }

    }
}
