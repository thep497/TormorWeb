// *******************************************************************
//1. ชื่อโปรแกรม 	: NeoSKIMO
//2. ชื่อระบบ 		: Web Model
//3. ชื่อ Unit 	: SSSearchInfo
//4. ผู้เขียนโปรแกรม	: อมรเทพ
//5. วันที่สร้าง	    : 201012xx
//6. จุดประสงค์ของ Unit นี้ : Model สร้าง Session เก็บข้อมูลการค้นหา (ช่วงวันที่)
// *******************************************************************
// แก้ไขครั้งที่ : 0
// ประวัติการแก้ไข
// ครั้งที่ 0 : เริ่มต้นสร้าง unit ใหม่
// *******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tormor.DomainModel;

namespace Tormor.Web.Models
{
    public static class SSSearchInfo
    {
        private static string getSessionName(string controllerName)
        {
            return String.Format("_SearchInfo{0}", controllerName.ToLower());
        }
        public static void SetSearchInfo<T>(string controllerName, T value)
        {
            HttpContext.Current.Session[getSessionName(controllerName)] = value;
        }

        public static SearchInfoBase GetSearchInfoBase(string controllerName)
        {
            if (HttpContext.Current.Session[getSessionName(controllerName)] == null)
                HttpContext.Current.Session[getSessionName(controllerName)] = new SearchInfoBase();
            return (SearchInfoBase)HttpContext.Current.Session[getSessionName(controllerName)];
        }
    }
}