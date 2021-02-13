// *******************************************************************
//1. ชื่อโปรแกรม 	: NeoSKIMO
//2. ชื่อระบบ 		: WebUI
//3. ชื่อ Unit 	: AlienController
//4. ผู้เขียนโปรแกรม	: อมรเทพ
//5. วันที่สร้าง	    : 201012xx
//6. จุดประสงค์ของ Unit นี้ : Controller จัดการเกี่ยวกับการกระทำของ Alien
// *******************************************************************
// แก้ไขครั้งที่ : 1
// ประวัติการแก้ไข
// ครั้งที่ 0 : เริ่มต้นสร้าง unit ใหม่
// ครั้งที่ 1 : //th20110409 แก้การ Save ให้บันทึก field เพิ่ม 2 field (ID/Seaman) PD18-540102 Req 2
// *******************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NNS.Config;
using NNS.GeneralHelpers;
using NNS.ModelHelpers;
using NNS.MVCHelpers;
using NNS.Web.Infrastructure;
using Telerik.Web.Mvc;
using Telerik.Web.Mvc.Extensions;
using Tormor.DomainModel;
using Tormor.Web.Models;

namespace Tormor.Web.Areas.AlienArea.Controllers
{
    [Authorize]
    public class AlienController : Controller
    {
        private readonly IAlienRepository alienRepo;

        public AlienController(IAlienRepository productRepository)
        {
            this.alienRepo = productRepository;
        }

        //
        // GET: /Foreigner/

        private IEnumerable<AlienLite> setAlienViewModel()
        {
            return (from o in alienRepo.FindAll().ToList()
                    select new AlienLite(o));
        }
        public ActionResult Index()
        {
            return View(setAlienViewModel());
        }

        public ActionResult Edit(int id)
        {
            var alien = alienRepo.GetOne(id);
            return View(alien);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ShowPhoto(int id)
        {
            var alien = alienRepo.GetOne(id);
            if (alien != null)
            {
                if ((alien.Photo != null) && !string.IsNullOrEmpty(alien.Photo.ContentType))
                {
                    var result = new ImageResult(alien.Photo.FPicture, alien.Photo.ContentType);
                    return result;
                }
            }
            var dir = Server.MapPath("~/Content/Images");
            var path = Path.Combine(dir, "noimage.gif");
            return base.File(path, "image/gif");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PhotoUpload(int id)
        {
            var alien = alienRepo.GetOne(id);
            if (alien != null)
            {
                var file = Request.Files["OriginalLocation"];

                if ((file.ContentLength > 0) && (file.ContentLength < 1048576)) //ขนาดรูปต้องไม่เกิน 1MB
                {
                    alien.Photo.ContentType = file.ContentType;

                    Int32 length = file.ContentLength;
                    //This may seem odd, but the fun part is that if
                    //  I didn't have a temp image to read into, I would
                    //  get memory issues for some reason.  Something to do
                    //  with reading straight into the object's ActualImage property.
                    byte[] tempImage = new byte[length];
                    file.InputStream.Read(tempImage, 0, length);
                    alien.Photo.FPicture = tempImage;

                    this.alienRepo.CurrentUserName = HttpContext.User.Identity.Name;
                    try
                    {
                        alienRepo.DoSave(alien, false);
                    }
                    catch (Exception ex)
                    {
                        TempData.AddWarning(ex.Message);
                    }
                    return new FileUploadJsonResult { Data = new { message = string.Format("{0} uploaded.", System.IO.Path.GetFileName(file.FileName)) } };
                }
            }
            return new FileUploadJsonResult { Data = new { message = "Cannot upload!!!" } };
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult _getAllNationality(string text, string value)
        {
            var checkedDate = DateTime.Today.AddYears(-1);
            var refs = alienRepo.FindAll().Where(r => r.UpdateInfo.UpdatedDate >= checkedDate); // กรองเอาเฉพาะพวกที่กรอกภายใน 1 ปีเท่านั้น
            if (text.HasValue())
            {
                text = text.ToLower();
                refs = refs.Where(r => r.Nationality.ToLower().Contains(text));
            }
            var result = (from r in refs
                          group r by r.Nationality into n
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

        /// <summary>
        /// th20110804 คำนวณอายุจากวันเกิด
        /// </summary>
        /// <param name="sBirthDate"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult _calculateAge(string sBirthDate)
        {
            var birthDate = sBirthDate.ToDate() ?? DateTime.Today;
            var age = birthDate.CalcAgeYear();
            return new JsonResult
            {
                Data = new
                {
                    status = Globals.ModalDetailUpdateOK,
                    age = age
                }
            };
        }

        /// <summary>
        /// th20110804 คำนวณปีเกิดจากอายุ โดย set ให้เป็นวันที่ 1 เดือน 1
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult _calculateDateOfBirth(int age, string sRefDate = null)
        {
            var birthDate = age.CalcDateOfBirth(sRefDate.ToDate());
            return new JsonResult
            {
                Data = new
                {
                    status = Globals.ModalDetailUpdateOK,
                    birthdate = birthDate.ToString(Globals.DateFormat)
                }
            };
        }

        #region AJAX CRUD Section...
        public ActionResult _GetPhotoUploadView(int id)
        {
            if (id > 0) 
            {
                var alien = alienRepo.GetOne(id);
                return PartialView("PhotoUpload", alien);
            }
            return Content(ControllerHelper.ModalContent("กรุณากดปุ่ม Save ด้านบนของเอกสารหลักก่อนค่ะ"));
        }
        
        [GridAction]
        public ActionResult _SelectAjaxEditing()
        {
            return View(new GridModel(setAlienViewModel()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _UpdateAjaxEditing(int id)
        {
            var alien = alienRepo.GetOne(id);
            this.alienRepo.CurrentUserName = HttpContext.User.Identity.Name;
            try
            {
                UpdateModel(alien);
                alienRepo.DoSave(alien, false);
            }
            catch (RulesException ex)
            {
                ex.CopyTo(ModelState);
            }
            return View(new GridModel(alienRepo.FindAll().ToList()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertAjaxEditing()
        {
            //Create a new instance of the EditableProduct class.            
            var alien = new Alien();
            this.alienRepo.CurrentUserName = HttpContext.User.Identity.Name;
            alienRepo.DoNewRecord(alien);

            //Perform model binding (fill the alien properties and validate it).            
            try
            {
                UpdateModel(alien);
                //The model is valid - insert the alien.                
                alienRepo.DoSave(alien, true);
            }
            catch (RulesException ex)
            {
                ex.CopyTo(ModelState);
            }

            //Rebind the grid            
            return View(new GridModel(alienRepo.FindAll().ToList()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteAjaxEditing(int id)
        {
            //Find a customer with ProductID equal to the id action parameter            
            Alien alien = alienRepo.GetOne(id);
            this.alienRepo.CurrentUserName = HttpContext.User.Identity.Name;
            if (alien != null)
            {
                //Delete the record                
                alienRepo.DoDelete(alien);
            }
            //Rebind the grid            
            return View(new GridModel(alienRepo.FindAll().ToList()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult _AlienSearch(string alien_Passport, string alien_Name, bool isInCrewPage)
        {
            ViewData["AlienSearch_Passport"] = alien_Passport;
            ViewData["AlienSearch_Name"] = alien_Name;
            ViewData["InCrewPage"] = isInCrewPage; //th20110409

            var alienJSON = new AlienViewModel();
            int alienCount = 0;
            int alienId = 0;

            try
            {
                var alienList = alienRepo.Search(alien_Passport, alien_Name).ToList();
                if (alienList.Count() > 0)
                {
                    alienCount = alienList.Count();
                    var alien = alienList.FirstOrDefault();
                    if ((alien != null) && (alienCount == 1))
                    {
                        alienJSON.Alien.CopyFrom(alien);
                        alienId = alien.Id;
                    }
                }
            }
            catch (Exception ex)
            {
                TempData.AddWarning(ex.Message);
            }

            return new JsonResult
            {
                Data = new
                {
                    aliencount = alienCount,
                    alien_id = alienId,
                    partialview = this.RenderPartialViewToString("AlienEditDetail", alienJSON)
                }
            };
        }
        #endregion
    }
}
