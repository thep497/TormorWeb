// *******************************************************************
//1. ชื่อโปรแกรม 	: NeoSKIMO
//2. ชื่อระบบ 		: WebUI
//3. ชื่อ Unit 	: ConveyanceController
//4. ผู้เขียนโปรแกรม	: อมรเทพ
//5. วันที่สร้าง	    : 201012xx
//6. จุดประสงค์ของ Unit นี้ : Controller สำหรับจัดการ Conveyances
// *******************************************************************
// แก้ไขครั้งที่ : 1
// ประวัติการแก้ไข
// ครั้งที่ 0 : เริ่มต้นสร้าง unit ใหม่
// ครั้งที่ 1 : th20110408 เพิ่ม ajax function สำหรับสร้าง Combobox ชื่อเรือ PD18-540102 Req 7.4
// *******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tormor.DomainModel;
using Telerik.Web.Mvc;
using Telerik.Web.Mvc.Extensions;
using NNS.ModelHelpers;
using NNS.MVCHelpers;
using NNS.Web.Infrastructure;
using System.IO;
using Tormor.Web.Models;

namespace Tormor.Web.Areas.ConveyanceArea.Controllers
{
    [Authorize]
    public class ConveyanceController : Controller
    {
        private readonly IConveyanceRepository convRepo;

        public ConveyanceController(IConveyanceRepository convRepository)
        {
            this.convRepo = convRepository;
        }

        //
        // GET: /Foreigner/

        private IEnumerable<Conveyance> setConveyanceViewModel()
        {
            return (from o in convRepo.FindAll().ToList()
                    select o);
        }
        public ActionResult Index()
        {
            return View(setConveyanceViewModel());
        }

        public ActionResult Edit(int id)
        {
            Conveyance alien = convRepo.GetOne(id);
            return View(alien);
        }

        #region AJAX CRUD Section...
        [GridAction]
        public ActionResult _SelectAjaxEditing()
        {
            return View(new GridModel(setConveyanceViewModel()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult _ConveyanceSearch(string conv_Owner, string conv_Name)
        {
            ViewData["ConveyanceSearch_OwnerName"] = conv_Owner;
            ViewData["ConveyanceSearch_Name"] = conv_Name;

            var convJSON = new ConveyanceViewModel();
            int convCount = 0;
            int convId = 0;

            try
            {
                IEnumerable<Conveyance> convList = convRepo.Search(conv_Owner,conv_Name).ToList();
                if (convList.Count() > 0)
                {
                    convCount = convList.Count();
                    Conveyance conv = convList.FirstOrDefault();
                    if ((conv != null) && (convCount == 1))
                    {
                        convJSON.Conveyance.CopyFrom(conv); 
                        convId = conv.Id;
                    }
                }
            }
            catch (FormatException)
            {
            }

            return new JsonResult
            {
                Data = new
                {
                    convcount = convCount,
                    conv_id = convId,
                    partialview = this.RenderPartialViewToString("ConveyanceEditDetail", convJSON)
                }
            };
        }

        /// <summary>
        /// th20110408
        /// สร้าง Combobox แสดงชื่อเรือ
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult _getAllConveyanceName(string text, string value)
        {
            var checkedDate = DateTime.Today.AddYears(-1);
            var refs = convRepo.FindAll().Where(r => r.UpdateInfo.UpdatedDate >= checkedDate); // กรองเอาเฉพาะพวกที่กรอกภายใน 1 ปีเท่านั้น
            if (text.HasValue())
            {
                text = text.ToLower();
                refs = refs.Where(r => r.Name.ToLower().Contains(text));
            }
            var result = (from r in refs
                          group r by r.Name into n
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
        #endregion
    }
}
